using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GovernancePortal.Web_API.Endpoints;

public static class MeetingEndpoints
{
    public static WebApplication MapMeetingEndpoints(this WebApplication app)
    {
        app.MapPost("api/Meeting/Create",
            ([FromServices] IMeetingService meetingService, CreateMeetingPOST createMeetingPost) =>
                meetingService.CreateMeeting(createMeetingPost));
        
        app.MapGet("api/Meeting/List",
            ([FromServices] IMeetingService meetingService, PageQuery pageQuery) =>
                meetingService.GetAllMeetings(pageQuery));
        
        app.MapPost("api/{meetingId}/Meeting/Update",
            ([FromServices] IMeetingService meetingService, string meetingId,  UpdateMeetingPOST updateMeetingPost) =>
                meetingService.UpdateMeeting(meetingId, updateMeetingPost));
        app.MapPost("api/Meeting/AddPastMeeting",
            ([FromServices] IMeetingService meetingService, AddPastMeetingPOST addPastMeetingPost) =>
                meetingService.AddPastMeeting(addPastMeetingPost));
        app.MapPost("api/Meeting/AddPastMinutes",
            ([FromServices] IMeetingService meetingService, AddPastMinutesPOST addPastMinutesPost) =>
                meetingService.AddPastMinutes(addPastMinutesPost));
        app.MapPost("api/Meeting/AddPastAttendance",
            ([FromServices] IMeetingService meetingService, AddPastAttendancePOST addPastAttendancePost) =>
                meetingService.AddPastAttendance(addPastAttendancePost));
        app.MapPost("api/Meeting/{Id}",
            ([FromServices] IMeetingService meetingService, string Id) =>
                meetingService.GetMeetingById(Id));
        
        
        
        
        return app;
    }
}