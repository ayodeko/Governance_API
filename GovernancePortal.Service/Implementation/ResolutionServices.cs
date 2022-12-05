using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Data;
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
    public ResolutionServices(IHttpContextAccessor context, IUnitOfWork unit, IResolutionMaps resolutionMaps)
    {
        _context = context;
        _unit = unit;
        _resolutionMaps = resolutionMaps;
    }
    Person GetLoggedInUser()
    {
        var companyId = _context.HttpContext?.Request.Headers["CompanyId"];
        return new Person()
        {
            Id = "18312549-7133-41cb-8fd2-e76e1d088bb6",
            Name = "User1",
            CompanyId = companyId ?? "CompanyId",
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

    public async Task<Response> ChangeIsAnonymousAsync(string resolutionId, bool isAnonymous)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = await _unit.Votings.GetVotingAsync(resolutionId, person.CompanyId);
        retrievedVoting.IsAnonymous = isAnonymous;
        var response = new Response()
        {
            Data = retrievedVoting,
            Exception = null,
            Message = "Voting successfully created",
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
        if (retrievedVoting == null || !retrievedVoting.Any())
            throw new NotFoundException($"No resolution found in company with Id: {person.CompanyId}");
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

    public async Task<Response> PollVote(string resolutionId, string userId, PollVotePOST votePost)
    {
        var person = GetLoggedInUser();
        var retrievedVoting = await _unit.Polls.GetPoll_PollVotersAsync(resolutionId, person.CompanyId);
        if (retrievedVoting == null || retrievedVoting.ModelStatus == ModelStatus.Deleted)
            throw new NotFoundException($"Resolution with ID: {resolutionId} not found");
        var pollVoter = retrievedVoting?.PollUsers?.FirstOrDefault(x => x.UserId == userId);
        if (pollVoter == null)
            throw new NotFoundException($"Voter with UserID: {userId} not found");
        pollVoter = _resolutionMaps.InMap(votePost, pollVoter);
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
        var retrievedVoting = _unit.Votings.GetVoting_VotersList(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords).ToList();
        if (retrievedVoting == null || !retrievedVoting.Any())
            throw new NotFoundException($"No resolution found in company with Id: {person.CompanyId}");
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