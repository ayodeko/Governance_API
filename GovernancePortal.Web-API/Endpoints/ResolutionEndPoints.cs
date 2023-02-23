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
                resolutionServices.CreateVotingAsync(createVotingPost, cancellationToken)).RequireAuthorization();

        app.MapPost("api/Resolution/{resolutionId}/ChangeVoteIsAnonymous",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, IsAllowAnonymousPOST isAnonymous) =>
                resolutionServices.ChangeVoteIsAnonymousAsync(resolutionId, isAnonymous)).RequireAuthorization();
        
        app.MapPost("api/Resolution/{resolutionId}/Vote/{userId}",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, string userId, VotePOST votePost) =>
                resolutionServices.VoteAsync(resolutionId, userId, votePost)).RequireAuthorization();
        
        app.MapGet("api/Resolution/{resolutionId}/GetVotingDetails",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.GetVotingDetails(resolutionId)).RequireAuthorization();
        
        app.MapGet("api/Resolution/VotingList",
            ([FromServices] IResolutionServices resolutionServices,string? userId, string? searchString, DateTime? dateTime, PageQuery pageQuery) =>
                resolutionServices.GetVotingList(userId, searchString, dateTime, pageQuery)).RequireAuthorization();

        app.MapPost("api/Resolution/{resolutionId}/LinkToMeetingToVoting",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, LinkedMeetingIdPOST meetingIdPost) =>
                resolutionServices.LinkMeetingToVoting(resolutionId, meetingIdPost)).RequireAuthorization();
        
        app.MapGet("api/Resolution/{resolutionId}/RetrieveLinkedMeetingByVotingId",
            
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.GetLinkedMeetingByVotingId(resolutionId)).RequireAuthorization();

        app.MapPost("api/Resolution/{resolutionId}/LinkToMeetingToPoll",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, LinkedMeetingIdPOST meetingIdPost) =>
                resolutionServices.LinkMeetingToPoll(resolutionId, meetingIdPost)).RequireAuthorization();
        
        app.MapGet("api/Resolution/{resolutionId}/RetrieveLinkedMeetingByPollId",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.GetLinkedMeetingByPollId(resolutionId)).RequireAuthorization();

        app.MapPost("api/Resolution/CreatePolling",
            ([FromServices] IResolutionServices resolutionServices, CreatePollingPOST createVotingPost) =>
                resolutionServices.CreatePolling(createVotingPost)).RequireAuthorization();
        app.MapPost("api/Resolution/CreatePastPolling",
            ([FromServices] IResolutionServices resolutionServices, CreatePastPollPOST createPastPollPOST) =>
                resolutionServices.CreatePastPoll(createPastPollPOST)).RequireAuthorization();

        app.MapPost("api/Resolution/{resolutionId}/PollVote/{userId}",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, string userId, PollVotePOST votePost) =>
                resolutionServices.PollVote(resolutionId, userId, votePost)).RequireAuthorization();
        
        
        app.MapGet("api/Resolution/{resolutionId}/GetPollingDetails",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.GetPollingDetails(resolutionId)).RequireAuthorization();
        app.MapGet("api/Resolution/{resolutionId}/Update",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.GetUpdatePollingDetails(resolutionId)).RequireAuthorization();
        app.MapPost("api/Resolution/{resolutionId}/Update",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, UpdatePollingPOST updatePollingPOST) =>
                resolutionServices.UpdatePollingDetails(resolutionId, updatePollingPOST)).RequireAuthorization();
        
        app.MapGet("api/Resolution/PollingList",
            ([FromServices] IResolutionServices resolutionServices, string? userId, string? searchString, DateTime? dateTime, PageQuery pageQuery) =>
                resolutionServices.GetPollingList(userId, searchString, dateTime, pageQuery)).RequireAuthorization();
        
        app.MapPost("api/Resolution/{resolutionId}/ChangePollIsAnonymous",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId, IsAllowAnonymousPOST isAnonymous) =>
                resolutionServices.ChangePollIsAnonymousAsync(resolutionId, isAnonymous)).RequireAuthorization();
        
        app.MapPost("api/Resolution/{resolutionId}/EndPoll",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.EndPoll(resolutionId)).RequireAuthorization();
        
        app.MapPost("api/Resolution/{resolutionId}/EndVote",
            ([FromServices] IResolutionServices resolutionServices, string resolutionId) =>
                resolutionServices.EndVote(resolutionId)).RequireAuthorization();
        
        
        app.MapGet("api/Resolution/SearchVotingByTitle", ([FromServices] IResolutionServices resolutionServices, string searchMeetingsString, PageQuery pageQuery) =>
            resolutionServices.SearchVotingByTitle(searchMeetingsString, pageQuery)).RequireAuthorization();
        
        app.MapGet("api/Resolution/SearchPollByTitle", ([FromServices] IResolutionServices resolutionServices, string searchMeetingsString) =>
            resolutionServices.SearchPollByTitle(searchMeetingsString)).RequireAuthorization();
        
        
        
        return app;
    }
}