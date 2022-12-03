using System.Threading;
using System.Threading.Tasks;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Service.ClientModels.General;

namespace GovernancePortal.Service.Interface;

public interface IResolutionServices
{
    Task<Response> CreateVotingAsync(CreateVotingPOST createVotingPOST, CancellationToken cancellationToken);
    Task<Response> ChangeIsAnonymousAsync(string resolutionId, bool isAnonymous);
    Task<Response> VoteAsync(string resolutionId, string userId, VotePOST votePost);
    Task<Response> GetVotingDetails(string resolutionId);
    Task<Response> GetVotingList(PageQuery pageQuery);
    
    
    
    Task<Response> CreatePolling(CreatePollingPOST createPollingPOST);
    Task<Response> PollVote(string resolutionId, string userId, PollVotePOST votePost);
    Task<Response> GetPollingDetails(string resolutionId);
    Task<Response> GetPollingList(PageQuery pageQuery);
}

public class CreateVotingPOST
{
    
}
public class VotePOST
{
    public  VotingStance Stance { get; set; }
    public string StanceReason { get; set; }
}
public class PollVotePOST
{
    
}

public class CreatePollingPOST
{
    
}