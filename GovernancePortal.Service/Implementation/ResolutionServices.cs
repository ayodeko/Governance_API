using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Data;
using GovernancePortal.EF.Repository;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using Microsoft.AspNetCore.Http;

namespace GovernancePortal.Service.Implementation;

public class ResolutionServices : IResolutionServices
{
    private IHttpContextAccessor _context;
    private IUnitOfWork _unit;
    private IResolutionMaps _resolutionMaps;
    private IBridgeRepo _bridgeRepo;
    private IValidator<VotingUser> _votingUserValidator;
    public ResolutionServices(IHttpContextAccessor context, IUnitOfWork unit, IResolutionMaps resolutionMaps, IBridgeRepo bridgeRepo, IValidator<VotingUser> votingUserValidator)
    {
        _context = context;
        _unit = unit;
        _resolutionMaps = resolutionMaps;
        _bridgeRepo = bridgeRepo;
        _votingUserValidator = votingUserValidator;
    }
    Person GetLoggedInUser()
    {
        //var companyIdStringValues = _context.HttpContext?.Request.Headers["CompanyId"];
        return new Person()
        {
            Id = "18312549-7133-41cb-8fd2-e76e1d088bb6",
            Name = "User1",
            CompanyId = "Company1",
            UserType = UserType.StandaloneUser
        };
    }
    public async Task<Response> CreateVotingAsync(CreateVotingPOST createVotingPOST, CancellationToken cancellationToken)
    {
        var person = GetLoggedInUser();
        var voting = _resolutionMaps.InMap(createVotingPOST);
        await _unit.Votings.Add(voting, person);
        _unit.SaveToDB();
        var response = new Response()
        {
            Data = voting,
            Exception = null,
            Message = "Voting successfully created",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.Created.ToString()
        };
        return response;
    }

    public async Task<Response> ChangeIsAnonymousAsync(string resolutionId, IsAllowAnonymousPOST isAnonymous)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = await _unit.Votings.FindById(resolutionId, person.CompanyId);
        if (retrievedVoting == null || retrievedVoting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"No resolution found  with Id: {resolutionId}");
        retrievedVoting.IsAnonymous = isAnonymous.IsAllowAnonymous;
        _unit.SaveToDB();
        var response = new Response()
        {
            Data = retrievedVoting,
            Exception = null,
            Message = "Successful",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return response;
    }

    public async Task<Response> VoteAsync(string resolutionId, string userId, VotePOST votePost)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = await _unit.Votings.GetVoting_VotersAsync(resolutionId, person.CompanyId);
        if (retrievedVoting == null || retrievedVoting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Resolution with ID: {resolutionId} not found");
        var voter = retrievedVoting?.Voters?.FirstOrDefault(x => x.UserId == userId);
        if (voter == null || voter.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Voter with UserID: {userId} not found");
        voter.Stance = votePost.Stance;
        voter.StanceReason = votePost.StanceReason;
        await _votingUserValidator.ValidateAndThrowAsync(voter);
        _unit.SaveToDB();

        var response = new Response()
        {
            Data = retrievedVoting,
            Exception = null,
            Message = "Voting successful",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return response;
    }

    public async Task<Response> GetVotingDetails(string resolutionId)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = await _unit.Votings.GetVoting_VotersAsync(resolutionId, person.CompanyId);
        if (retrievedVoting == null || retrievedVoting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Resolution with ID: {resolutionId} not found");
        var outVoting = _resolutionMaps.OutMap(retrievedVoting);
        var response = new Response()
        {
            Data = retrievedVoting,
            Exception = null,
            Message = "Retrieved successfully",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return response;
    }

    public Task<Response> GetVotingList(PageQuery pageQuery)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = _unit.Votings.GetVoting_VotersList(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords).ToList();
        var response = new Response()
        {
            Data = retrievedVoting,
            Exception = null,
            Message = "Retrieved successfully",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return Task.FromResult(response);
    }
    public async Task<Response> LinkMeetingToVoting(string resolutionId, LinkedMeetingIdPOST meetingId)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = await _unit.Votings.FindById(resolutionId, person.CompanyId);
        if (retrievedVoting == null || retrievedVoting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Resolution with ID: {resolutionId} not found");
        var getMeeting = await _unit.Meetings.GetMeeting(meetingId.LinkedMeetingId, person.CompanyId);
        if (getMeeting == null || getMeeting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Meeting with ID: {meetingId.LinkedMeetingId} not found");
        await _bridgeRepo.AddMeeting_Resolution(meetingId.LinkedMeetingId, resolutionId, person.CompanyId);
        _unit.SaveToDB();
        var response = new Response()
        {
            Data = retrievedVoting,
            Exception = null,
            Message = "Meeting Successfully linked",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return response;
    }
    public async Task<Response> LinkMeetingToPoll(string resolutionId, LinkedMeetingIdPOST meetingId)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = await _unit.Polls.FindById(resolutionId, person.CompanyId);
        if (retrievedVoting == null || retrievedVoting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Resolution with ID: {resolutionId} not found");
        var getMeeting = await _unit.Meetings.GetMeeting(meetingId.LinkedMeetingId, person.CompanyId);
        if (getMeeting == null || getMeeting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Meeting with ID: {meetingId.LinkedMeetingId} not found");
        await _bridgeRepo.AddMeeting_Resolution(meetingId.LinkedMeetingId, resolutionId, person.CompanyId);
        _unit.SaveToDB();
        var response = new Response()
        {
            Data = retrievedVoting,
            Exception = null,
            Message = "Meeting Successfully linked",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return response;
    }
    
    public async Task<Response> GetLinkedMeetingByVotingId(string resolutionId)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = await _unit.Votings.FindById(resolutionId, person.CompanyId);
        if (retrievedVoting == null || retrievedVoting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Resolution with ID: {resolutionId} not found");

        var bridge = await _bridgeRepo.RetrieveMeetingByResolutionId(resolutionId, person.CompanyId);
        if (bridge == null)
            throw new NotFoundException(
                $"No relationship between resolution Id : {resolutionId} and any other meeting found");
        var response = new Response()
        {
            Data = new LinkedMeetingIdPOST(bridge.MeetingId),
            Exception = null,
            Message = "Successful",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return response;
    }
    
    public async Task<Response> GetLinkedMeetingByPollId(string resolutionId)
    {
        var person = GetLoggedInUser();
        var retrievedPolling = await _unit.Votings.FindById(resolutionId, person.CompanyId);
        if (retrievedPolling == null || retrievedPolling.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Resolution with ID: {resolutionId} not found");

        var bridge = await _bridgeRepo.RetrieveMeetingByResolutionId(resolutionId, person.CompanyId);
        if (bridge == null)
            throw new NotFoundException(
                $"Not relationship between resolution Id : {resolutionId} and any other meeting found");
        var response = new Response()
        {
            Data = new LinkedMeetingIdPOST(bridge.MeetingId),
            Exception = null,
            Message = "Successful",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return response;
    }

    public async Task<Response> CreatePolling(CreatePollingPOST createPollingPOST)
    {
        var person = GetLoggedInUser();
        var voting = _resolutionMaps.InMap(createPollingPOST);
        await _unit.Polls.Add(voting, person);
        _unit.SaveToDB();
        var response = new Response()
        {
            Data = voting,
            Exception = null,
            Message = "Voting successfully created",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.Created.ToString()
        };
        return response;
    }

    public async Task<Response> CreatePastPoll(CreatePastPollPOST createPastPollPOST)
    {
        var person = GetLoggedInUser();
        var poll = _resolutionMaps.InMap(createPastPollPOST);
        poll.IsPastPoll = true;
        await _unit.Polls.Add(poll, person);
        _unit.SaveToDB();
        var response = new Response()
        {
            Data = poll,
            Exception = null,
            Message = "Voting successfully created",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.Created.ToString()
        };
        return response;
    }

    public async Task<Response> PollVote(string resolutionId, string userId, PollVotePOST votePost)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = await _unit.Polls.GetPoll_PollVotersAsync(resolutionId, person.CompanyId);
        if (retrievedVoting == null || retrievedVoting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Resolution with ID: {resolutionId} not found");
        var pollVoter = retrievedVoting?.PollUsers?.FirstOrDefault(x => x.UserId == userId);
        if (pollVoter == null)
            throw new NotFoundException($"Voter with UserID: {userId} not found");
        var pollVoteList = pollVoter.PollVotes ?? new List<PollItemVote>();
        pollVoter.PollVotes = _resolutionMaps.InMap(votePost, pollVoteList);
        _unit.SaveToDB();

        var response = new Response()
        {
            Data = retrievedVoting,
            Exception = null,
            Message = "Voting successful",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return response;
    }

    public async Task<Response> GetPollingDetails(string resolutionId)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = await _unit.Polls.GetPoll_PollVotersAsync(resolutionId, person.CompanyId);
        if (retrievedVoting == null || retrievedVoting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Resolution with ID: {resolutionId} not found");
        var response = new Response()
        {
            Data = retrievedVoting,
            Exception = null,
            Message = "Retrieved successfully",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return response;
    }

    public Task<Response> GetPollingList(PageQuery pageQuery)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = _unit.Polls.GetPoll_PollVotersList(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords).ToList();
        var response = new Response()
        {
            Data = retrievedVoting,
            Exception = null,
            Message = "Retrieved successfully",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
        };
        return Task.FromResult(response);
    }
}