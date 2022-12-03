using System.Collections.Generic;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Resolutions;

public class Voting : BaseModel, ICompanyModel
{
    public Voting()
    {
        Voters = new List<VotingUser>();
    }
    public string Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public bool IsAnonymous { get; set; }
    public bool IsVotingEnded { get; set; }
    public List<VotingUser> Voters { get; set; }
}

public class VotingUser
{
    public VotingUser()
    {
        Stance = VotingStance.Abstain;
    }
    public string Id { get; set; }
    public string UserId { get; set; }
    public string VotingId { get; set; }
    public  VotingStance Stance { get; set; }
    public string StanceReason { get; set; }
}

public enum VotingStance
{
    Abstain,
    For,
    Against
}