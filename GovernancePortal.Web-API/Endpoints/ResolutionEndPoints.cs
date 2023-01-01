using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GovernancePortal.Web_API.Endpoints;

public static class ResolutionEndPoints
{
    public static WebApplication MapResolutionEndpoints(this WebApplication app)
    {

        app.MapPost("api/Resolution/CreateVoting",
            ([FromServices] IResolutionServices resolutionServices, CreateVotingPOST createVotingPost, CancellationToken cancellationToken) =>
                resolutionServices.CreateVotingAsync(createVotingPost, cancellationToken));

        app.MapPost("api/Resolution/{resolutionId}/ChangeVoteIsAnonymous",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, IsAllowAnonymousPOST isAnonymous) =>
                resolutionServices.ChangeVoteIsAnonymousAsync(resolutionId, isAnonymous));
        
        app.MapPost("api/Resolution/{resolutionId}/Vote/{userId}",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, string userId, VotePOST votePost) =>
                resolutionServices.VoteAsync(resolutionId, userId, votePost));
        
        app.MapGet("api/Resolution/{resolutionId}/GetVotingDetails",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.GetVotingDetails(resolutionId));
        
        app.MapGet("api/Resolution/VotingList",
            ([FromServices] IResolutionServices resolutionServices,string? userId, string? searchString, DateTime? dateTime, PageQuery pageQuery) =>
                resolutionServices.GetVotingList(userId, searchString, dateTime, pageQuery));

        app.MapPost("api/Resolution/{resolutionId}/LinkToMeetingToVoting",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, LinkedMeetingIdPOST meetingIdPost) =>
                resolutionServices.LinkMeetingToVoting(resolutionId, meetingIdPost));
        
        app.MapGet("api/Resolution/{resolutionId}/RetrieveLinkedMeetingByVotingId",
            
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.GetLinkedMeetingByVotingId(resolutionId));

        app.MapPost("api/Resolution/{resolutionId}/LinkToMeetingToPoll",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, LinkedMeetingIdPOST meetingIdPost) =>
                resolutionServices.LinkMeetingToPoll(resolutionId, meetingIdPost));
        
        app.MapGet("api/Resolution/{resolutionId}/RetrieveLinkedMeetingByPollId",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.GetLinkedMeetingByPollId(resolutionId));

        app.MapPost("api/Resolution/CreatePolling",
            ([FromServices] IResolutionServices resolutionServices, CreatePollingPOST createVotingPost) =>
                resolutionServices.CreatePolling(createVotingPost));
        app.MapPost("api/Resolution/CreatePastPolling",
            ([FromServices] IResolutionServices resolutionServices, CreatePastPollPOST createPastPollPOST) =>
                resolutionServices.CreatePastPoll(createPastPollPOST));

        app.MapPost("api/Resolution/{resolutionId}/PollVote/{userId}",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, string userId, PollVotePOST votePost) =>
                resolutionServices.PollVote(resolutionId, userId, votePost));
        
        
        app.MapGet("api/Resolution/{resolutionId}/GetPollingDetails",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.GetPollingDetails(resolutionId));
        
        app.MapGet("api/Resolution/PollingList",
            ([FromServices] IResolutionServices resolutionServices, string? userId, string? searchString, DateTime? dateTime, PageQuery pageQuery) =>
                resolutionServices.GetPollingList(userId, searchString, dateTime, pageQuery));
        
        app.MapPost("api/Resolution/{resolutionId}/ChangePollIsAnonymous",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, IsAllowAnonymousPOST isAnonymous) =>
                resolutionServices.ChangePollIsAnonymousAsync(resolutionId, isAnonymous));
        
        
        app.MapGet("api/Resolution/SearchVotingByTitle", ([FromServices] IResolutionServices resolutionServices, string searchMeetingsString, PageQuery pageQuery) =>
            resolutionServices.SearchVotingByTitle(searchMeetingsString, pageQuery));
        
        app.MapGet("api/Resolution/SearchPollByTitle", ([FromServices] IResolutionServices resolutionServices, string searchMeetingsString) =>
            resolutionServices.SearchPollByTitle(searchMeetingsString));
        
        
        
        return app;
    }
}