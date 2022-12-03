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
    private IVotingMaps _votingMaps;
    public ResolutionServices(IHttpContextAccessor context, IUnitOfWork unit, IVotingMaps votingMaps)
    {
        _context = context;
        _unit = unit;
        _votingMaps = votingMaps;
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
        var voting = _votingMaps.InMap(createVotingPOST, new Voting());
        await _unit.Votings.Add(voting, person);
        _unit.SaveToDB();
        var response = new Response()
        {
            Data = voting,
            Exception = null,
            Message = "Voting successfully created",
            IsSuccessful = true,
            StatusCode = HttpStatusCode.OK.ToString()
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

    public Task<Response> GetVotingDetails(string resolutionId)
    {
        throw new System.NotImplementedException();
    }

    public Task<Response> GetVotingList(PageQuery pageQuery)
    {
        throw new System.NotImplementedException();
    }

    public Task<Response> CreatePolling(CreatePollingPOST createPollingPOST)
    {
        throw new System.NotImplementedException();
    }

    public Task<Response> PollVote(string resolutionId, string userId, PollVotePOST votePost)
    {
        throw new System.NotImplementedException();
    }

    public Task<Response> GetPollingDetails(string resolutionId)
    {
        throw new System.NotImplementedException();
    }

    public Task<Response> GetPollingList(PageQuery pageQuery)
    {
        throw new System.NotImplementedException();
    }
}