using System.Globalization;
using System.Net;
using AutoMapper;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Data;
using GovernancePortal.Data.Repository;
using GovernancePortal.EF;
using GovernancePortal.EF.Repository;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.Implementation;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using GovernancePortal.Service.Mappings.Maps;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GovernancePortal.Web_API.Endpoints;

public static class CreateMeetingEndpoints
{
    public static WebApplication MapSectionedMeetingEndpoints(this WebApplication app)
    {

        #region Create, Update Meeting
        
        app.MapPost("api/Meeting/Create",
            ([FromServices] IMeetingService meetingService, CreateMeetingPOST createMeetingPost) =>
                meetingService.CreateMeeting(createMeetingPost));
        
        app.MapPost("api/Meeting/{meetingId}/AddAttendees", ([FromServices] IMeetingService meetingService,
                string meetingId, AddAttendeesPOST updateMeetingAttendeesPost) =>
            meetingService.AddAttendees(meetingId, updateMeetingAttendeesPost));
        
        app.MapPost("api/Meeting/{meetingId}/Attendees/Update", ([FromServices] IMeetingService meetingService,
                string meetingId, UpdateAttendingUsersPOST updateMeetingAttendeesPost) =>
            meetingService.UpdateAttendingUsers(meetingId, updateMeetingAttendeesPost));
        
        app.MapPost("api/Meeting/{meetingId}/AgendaItems/Update", ([FromServices] IMeetingService meetingService,
               string meetingId, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST) =>
            meetingService.UpdateAgendaItems(meetingId, updateMeetingAgendaItemPOST));
        
        app.MapPost("api/Meeting/{meetingId}/MeetingPack/Update", ([FromServices] IMeetingService meetingService,
                string meetingId, UpdateMeetingPackPOST updateMeetingPackPOST) =>
            meetingService.UpdateMeetingPack(meetingId, updateMeetingPackPOST));
        
        app.MapPost("api/Meeting/{meetingId}/Minutes/Update", ([FromServices] IMeetingService meetingService,
                string meetingId, UpdateMeetingMinutesPOST updateMeetingMinutesPOST) =>
            meetingService.UpdateMinutes(meetingId, updateMeetingMinutesPOST));
        
        app.MapPost("api/Meeting/{meetingId}/Notice/Update", ([FromServices] IMeetingService meetingService,
                string meetingId, UpdateMeetingNoticePOST updateMeetingMinutesPOST) =>
            meetingService.UpdateNotice(meetingId, updateMeetingMinutesPOST));
        #endregion
        
        
        
        #region Get Update Data for meeting
        app.MapGet("api/Meeting/{meetingId}/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingUpdateData(meetingId));
        app.MapGet("api/Meeting/{meetingId}/Minutes/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingMinutesUpdateData(meetingId));
        
        app.MapGet("api/Meeting/{meetingId}/Attendees/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingAttendeesUpdateData(meetingId));
        
        app.MapGet("api/Meeting/{meetingId}/AgendaItems/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingAgendaItemsUpdateData(meetingId));
        
        app.MapGet("api/Meeting/{meetingId}/MeetingPack/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingPackUpdateData(meetingId));
        
        app.MapGet("api/Meeting/{meetingId}/Notice/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingNoticeUpdateData(meetingId));
        
        #endregion

        #region  Retrieve Meeting Details, Meetings
        app.MapGet("api/Meeting/List",
            ([FromServices] IMeetingService meetingService, PageQuery pageQuery) =>
                meetingService.GetAllMeetingList(pageQuery));
        app.MapGet("api/Meeting/UserMeetings", ([FromServices] IMeetingService meetingService, PageQuery pageQuery) =>
            meetingService.GetUserMeetingList(pageQuery));
        app.MapGet("api/Meeting/SearchMeetings", ([FromServices] IMeetingService meetingService, string searchMeetingsString) =>
            meetingService.SearchMeetings(searchMeetingsString));
        app.MapGet("api/Meeting/SearchMeetingsByDate", ([FromServices] IMeetingService meetingService, DateTime dateTime) =>
            meetingService.SearchMeetingsByDate(dateTime));

        #endregion

        #region Get Meeting Relations

        app.MapGet("api/Meeting/{meetingId}/MeetingPack", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingPack(meetingId));
        #endregion






        #region Attendance

        app.MapPost("api/Meeting/{meetingId}/Attendance/GenerateCode", ([FromServices] IAttendanceServices meetingService,
                string meetingId, CancellationToken token) =>
            meetingService.GenerateAttendanceCode(meetingId, token));
        
        app.MapGet("api/Meeting/{meetingId}/Attendance/RetrieveGeneratedCode", ([FromServices] IAttendanceServices meetingService,
                string meetingId, CancellationToken token) =>
            meetingService.RetrieveGeneratedAttendanceCode(meetingId, token));
        
        app.MapPost("api/Meeting/{meetingId}/Attendance/SendCodeToAll", ([FromServices] IAttendanceServices meetingService,
                string meetingId, CancellationToken token) =>
            meetingService.SendAttendanceCodeToAll(meetingId, token));
        
        app.MapPost("api/Meeting/{meetingId}/Attendance/SendCodeToUser/{userId}", ([FromServices] IAttendanceServices meetingService,
                string meetingId, string userId, CancellationToken token) =>
            meetingService.SendAttendanceCodeToUser(meetingId, userId, token)); 
        
        app.MapPost("api/Meeting/{meetingId}/Attendance/RemindAll", ([FromServices] IAttendanceServices meetingService,
                string meetingId, CancellationToken token) =>
            meetingService.NotifyAllToMarkAttendance(meetingId, token));
        
        app.MapPost("api/Meeting/{meetingId}/Attendance/RemindUser/{userId}", ([FromServices] IAttendanceServices meetingService,
                string meetingId, string userId, CancellationToken token) =>
            meetingService.NotifyUserToMarkAttendance(meetingId, userId, token));
        
        app.MapPost("api/Meeting/{meetingId}/Attendance/MarkAttendance/{userId}", ([FromServices] IAttendanceServices meetingService,
                string meetingId, string userId, string attendanceCode, CancellationToken token) =>
            meetingService.MarkAttendance(meetingId, userId, attendanceCode, token));
        
        app.MapGet("api/Meeting/{meetingId}/Attendance/RetrieveAttendanceDetails", ([FromServices] IAttendanceServices meetingService,
                string meetingId, CancellationToken token) =>
            meetingService.GetAttendanceDetails(meetingId, token));

        #endregion
        
        
        
        
        
        
        
        
        return app;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    public static WebApplicationBuilder RegisterMeetingServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMeetingService, MeetingServices>();
        builder.Services.AddScoped<IAttendanceServices, AttendanceService>();
        builder.Services.AddScoped<IBusinessLogic, BusinessLogicService>();
        builder.Services.AddScoped<IMeetingMaps, MeetingMaps>();
        builder.Services.AddScoped<ILogger, StubLogger>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        return builder;
    }

}


