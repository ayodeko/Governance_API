using System.Collections.Generic;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Service.Interface;

namespace GovernancePortal.Service.Mappings.IMaps;

public interface IResolutionMaps
{
    Voting InMap(CreateVotingPOST createVotingPost);
    VotingUser InMap(VotePOST createVotingPost, VotingUser voting);
    VotingDetailsGET OutMap(Voting voting);
    UpdatePollingPOST OutMap(Poll poll);
    
    
    Poll InMap(CreatePollingPOST createVotingPost);
    List<PollItemVote> InMap(PollVotePOST createVotingPost, List<PollItemVote> preexistingPollItemVotes);
    Poll InMap(CreatePastPollPOST createPastPollPost);
    Poll InMap(UpdatePollingPOST updatePollPost, Poll preexistingPollItemVotes);
}