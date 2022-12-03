using GovernancePortal.Core.Resolutions;
using GovernancePortal.Service.Interface;

namespace GovernancePortal.Service.Mappings.IMaps;

public interface IVotingMaps
{
    Voting InMap(CreateVotingPOST createVotingPost, Voting voting);
}