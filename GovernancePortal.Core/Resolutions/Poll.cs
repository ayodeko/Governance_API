using System.Collections.Generic;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Resolutions;

public class Poll : BaseModel, ICompanyModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public bool isUnlimitedSelection { get; set; }
    public int MaximumSelection { get; set; }
    public List<PollItem> PollItems { get; set; }
    public List<PollUser> PollUsers { get; set; }
}

public class PollItem
{
    public string Id { get; set; }
    public string Title { get; set; }
}

public class PollUser
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string PollId { get; set; }
    public List<PollItemVote> PollVotes { get; set; }
}

public class PollItemVote
{
    public string Id { get; set; }
    public string PollItemId { get; set; }
    public string PollUserId { get; set; }
}