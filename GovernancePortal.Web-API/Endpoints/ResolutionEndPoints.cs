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

        app.MapPost("api/Resolution/{resolutionId}/ChangeIsAnonymous",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, IsAllowAnonymousPOST isAnonymous) =>
                resolutionServices.ChangeIsAnonymousAsync(resolutionId, isAnonymous));
        
        app.MapPost("api/Resolution/{resolutionId}/Vote/{userId}",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, string userId, VotePOST votePost) =>
                resolutionServices.VoteAsync(resolutionId, userId, votePost));
        
        app.MapGet("api/Resolution/{resolutionId}/GetVotingDetails",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.GetVotingDetails(resolutionId));
        
        app.MapGet("api/Resolution/VotingList",
            ([FromServices] IResolutionServices resolutionServices, PageQuery pageQuery) =>
                resolutionServices.GetVotingList(pageQuery));

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
            ([FromServices] IResolutionServices resolutionServices, PageQuery pageQuery) =>
                resolutionServices.GetPollingList(pageQuery));
        
        
        app.MapGet("api/Meeting/SearchVotingByTitle", ([FromServices] IResolutionServices resolutionServices, string searchMeetingsString, PageQuery pageQuery) =>
            resolutionServices.SearchVotingByTitle(searchMeetingsString, pageQuery));
        
        app.MapGet("api/Meeting/SearchPollByTitle", ([FromServices] IResolutionServices resolutionServices, string searchMeetingsString) =>
            resolutionServices.SearchPollByTitle(searchMeetingsString));
        
        return app;
    }
}