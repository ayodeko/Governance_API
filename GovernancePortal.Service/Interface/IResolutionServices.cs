using System.Threading.Tasks;
using GovernancePortal.Service.ClientModels.General;

namespace GovernancePortal.Service.Interface;

public interface IResolutionServices
{
    Task<Response> CreateVoting(CreateVotingPOST createVotingPOST);
    Task<Response> ChangeIsAnonymous(string resolutionId, bool isAnonymous);
    Task<Response> Vote(string resolutionId, string userId, VotePOST votePost);
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
    
}
public class PollVotePOST
{
    
}

public class CreatePollingPOST
{
    
}