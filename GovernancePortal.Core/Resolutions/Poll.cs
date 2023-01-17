using System;
using System.Collections.Generic;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Resolutions;

public class Poll : BaseModel, ICompanyModel
{
    public Poll()
    {
        Id = Guid.NewGuid().ToString();
        PollItems = new List<PollItem>();
        PollUsers = new List<PollUser>();
        PastPollItems = new List<PastPollItem>();
    }
    public string Id { get; set; }
    public string Title { get; set; }
    public bool isUnlimitedSelection { get; set; }
    public bool IsAnonymousVote { get; set; }
    public int MaximumSelection { get; set; }
    public DateTime DateTIme { get; set; }
    public List<PollItem> PollItems { get; set; }
    public List<PollUser> PollUsers { get; set; }
    public List<PastPollItem> PastPollItems { get; set; }
    public int PastPollParticipantAmount { get; set; }
    public bool IsPastPoll { get; set; }
    public ResolutionStatus ResolutionStatus { get; set; }
}

public enum ResolutionStatus
{
    Default,
    Progress,
    Completed
}
public class PollItem
{
    public PollItem()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string PollId { get; set; }
    public string Title { get; set; }
}

public class PollUser
{
    public PollUser()
    {
        Id = Guid.NewGuid().ToString();
        PollVotes = new List<PollItemVote>();
    }
    public string Id { get; set; }
    public string UserId { get; set; }
    public string PollId { get; set; }
    public List<PollItemVote> PollVotes { get; set; }
}

public class PastPollItem
{
    public PastPollItem()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string Title { get; set; }
    public int Percentage { get; set; }
}

public class PollItemVote
{
    public PollItemVote()
    {
        Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }
    public string PollItemId { get; set; }
    public string UserId { get; set; }
}