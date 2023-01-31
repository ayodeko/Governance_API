using System;
using System.Collections.Generic;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Resolutions;

public class Voting : BaseModel, ICompanyModel
{
    public Voting()
    {
        Id = Guid.NewGuid().ToString();
        Voters = new List<VotingUser>();
    }
    public string Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public bool IsAnonymous { get; set; }
    public bool IsPast { get; set; }
    public bool IsLinkedToMeeting { get; set; }
    public ResolutionStatus ResolutionStatus { get; set; }
    public DateTime DateTime { get; set; }
    public List<VotingUser> Voters { get; set; }
}

public class VotingUser : BaseModel
{
    public VotingUser()
    {
        Id = Guid.NewGuid().ToString();
        Stance = VotingStance.Abstain;
        HasVoted = false;
    }
    public string Id { get; set; }
    public string UserId { get; set; }
    public string VotingId { get; set; }
    public  VotingStance Stance { get; set; }
    public bool HasVoted { get; set; }
    public string StanceReason { get; set; }
}

public enum VotingStance
{
    Abstain = 0,
    For = 1,
    Against = 2
}