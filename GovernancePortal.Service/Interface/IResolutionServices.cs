using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovernancePortal.Core.Resolutions;
using GovernancePortal.Service.ClientModels.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;

namespace GovernancePortal.Service.Interface;

public interface IResolutionServices
{
    Task<Response> CreateVotingAsync(CreateVotingPOST createVotingPOST, CancellationToken cancellationToken);
    Task<Response> ChangeVoteIsAnonymousAsync(string resolutionId, IsAllowAnonymousPOST isAnonymous);
    Task<Response> VoteAsync(string resolutionId, string userId, VotePOST votePost);
    Task<Response> GetVotingDetails(string resolutionId);
    Task<Response> LinkMeetingToVoting(string resolutionId, LinkedMeetingIdPOST meetingId);
    Task<Response> LinkMeetingToPoll(string resolutionId, LinkedMeetingIdPOST meetingId);
    Task<Response> GetLinkedMeetingByVotingId(string resolutionId);
    Task<Response> GetLinkedMeetingByPollId(string resolutionId);
    Task<Response> GetVotingList(PageQuery pageQuery);
    Task<Response> SearchVotingByTitle(string searchMeetingsString, PageQuery pageQuery);
    
    
    
    Task<Response> CreatePolling(CreatePollingPOST createPollingPOST);
    Task<Response> PollVote(string resolutionId, string userId, PollVotePOST votePost);
    Task<Response> GetPollingDetails(string resolutionId);
    Task<Response> GetPollingList(PageQuery pageQuery);
    Task<Response> CreatePastPoll(CreatePastPollPOST createPastPollPost);
    Task<Response> SearchPollByTitle(string searchMeetingsString);
    Task<Response> ChangePollIsAnonymousAsync(string resolutionId, IsAllowAnonymousPOST isAnonymous);
}

public class VotingDetailsGET
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public bool IsAnonymous { get; set; }
    public bool IsVotingEnded { get; set; }
    public DateTime DateTime { get; set; }
    public List<VotingUser> Voters { get; set; }
}
public class CreateVotingPOST
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public bool IsAnonymous { get; set; }
    public DateTime DateTime { get; set; }
    public List<VotingUserPOST> Voters { get; set; }
}

public record IsAllowAnonymousPOST (bool IsAllowAnonymous);
public record LinkedMeetingIdPOST (string LinkedMeetingId);
public class VotingUserPOST
{
    public string UserId { get; set; }
    public string VotingId { get; set; }
    public  VotingStance Stance { get; set; }
    public string StanceReason { get; set; }
}
public class VotingUserGET
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string VotingId { get; set; }
    public  VotingStance Stance { get; set; }
    public string StanceReason { get; set; }
}
public class VotePOST
{
    public  VotingStance Stance { get; set; }
    public string StanceReason { get; set; }
}
public class PollVotePOST
{
    public string UserId { get; set; }
    public List<string> PollItemIds { get; set; }
}

public class CreatePollingPOST
{
    public string Title { get; set; }
    public bool isUnlimitedSelection { get; set; }
    public bool IsAnonymousVote { get; set; }
    public int MaximumSelection { get; set; }
    public DateTime DateTime { get; set; }
    public List<PollItemPOST> PollItems { get; set; }
    public List<PollUserPOST> PollUsers { get; set; }
}
public class CreatePastPollPOST
{
    public string Title { get; set; }
    public DateTime DateTime { get; set; }
    public int PastPollParticipantAmount { get; set; }
    public List<PastPollItemPOST> PastPollItems { get; set; }
}

public record PastPollItemPOST(string Title, int Percentage);

public class PollItemPOST
{
    public string Title { get; set; }
}
public class PollUserPOST
{
    public string UserId { get; set; }
    public string PollId { get; set; }
    public List<PollItemVote> PollVotes { get; set; }
}