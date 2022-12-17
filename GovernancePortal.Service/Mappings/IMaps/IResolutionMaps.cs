using GovernancePortal.Core.Resolutions;
using GovernancePortal.Service.Interface;

namespace GovernancePortal.Service.Mappings.IMaps;

public interface IResolutionMaps
{
    Voting InMap(CreateVotingPOST createVotingPost);
    VotingUser InMap(VotePOST createVotingPost, VotingUser voting);
    VotingDetailsGET OutMap(Voting voting);
    
    
    Poll InMap(CreatePollingPOST createVotingPost);
    PollUser InMap(PollVotePOST createVotingPost, PollUser pollVoter);
    Poll InMap(CreatePastPollPOST createPastPollPost);
}