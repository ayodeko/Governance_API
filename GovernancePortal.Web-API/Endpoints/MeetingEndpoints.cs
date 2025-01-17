﻿using System.Globalization;
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
using GovernancePortal.Service.ClientModels.Meetings.Minute;
using GovernancePortal.Service.Implementation;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using GovernancePortal.Service.Mappings.Maps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GovernancePortal.Web_API.Endpoints;

public static class MeetingEndpoints
{
    [Authorize]
    public static WebApplication MapSectionedMeetingEndpoints(this WebApplication app)
    {

        #region Create, Update Meeting

        
        app.MapPost("api/Meeting/Create",
            ([FromServices] IMeetingService meetingService, CreateMeetingPOST createMeetingPost) =>
                meetingService.CreateMeeting(createMeetingPost)).RequireAuthorization();
                
        app.MapPost("api/Meeting/{meetingId}/Update", ([FromServices] IMeetingService meetingService,
                string meetingId, UpdateMeetingPOST updateMeetingPost) =>
            meetingService.UpdateMeetingDetails(meetingId, updateMeetingPost)).RequireAuthorization();
                
        app.MapDelete("api/Meeting/{meetingId}/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.DeleteMeetingDetails(meetingId)).RequireAuthorization();
        
        app.MapPost("api/Meeting/{meetingId}/AddAttendees", ([FromServices] IMeetingService meetingService,
                string meetingId, AddAttendeesPOST updateMeetingAttendeesPost) =>
            meetingService.AddAttendees(meetingId, updateMeetingAttendeesPost)).RequireAuthorization();
        
        app.MapPost("api/Meeting/{meetingId}/Attendees/Update", ([FromServices] IMeetingService meetingService,
                string meetingId, UpdateAttendingUsersPOST updateMeetingAttendeesPost) =>
            meetingService.UpdateAttendingUsers(meetingId, updateMeetingAttendeesPost)).RequireAuthorization();
        
        app.MapPost("api/Meeting/{meetingId}/AgendaItems/Update", ([FromServices] IMeetingService meetingService,
               string meetingId, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST) =>
            meetingService.UpdateAgendaItems(meetingId, updateMeetingAgendaItemPOST)).RequireAuthorization();
        
        app.MapPost("api/Meeting/{meetingId}/AgendaItems/FullUpdate", ([FromServices] IMeetingService meetingService,
               string meetingId, FullUpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST) =>
            meetingService.FullUpdateAgendaItems(meetingId, updateMeetingAgendaItemPOST)).RequireAuthorization();
        
        app.MapPost("api/Meeting/{meetingId}/Notice/Update", ([FromServices] IMeetingService meetingService,
                string meetingId, UpdateMeetingNoticePOST updateMeetingMinutesPOST) =>
            meetingService.UpdateNotice(meetingId, updateMeetingMinutesPOST)).RequireAuthorization();
        #endregion
        
        #region Get Update Data for meeting
        app.MapGet("api/Meeting/{meetingId}/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingUpdateData(meetingId)).RequireAuthorization();
        
        
        app.MapGet("api/Meeting/{meetingId}/Attendees/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingAttendeesUpdateData(meetingId)).RequireAuthorization();
        
        app.MapGet("api/Meeting/{meetingId}/AgendaItems/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingAgendaItemsUpdateData(meetingId)).RequireAuthorization();
        
        app.MapGet("api/Meeting/{meetingId}/AgendaItems/FullUpdate", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingAgendaItemsFullUpdateData(meetingId)).RequireAuthorization();
        
        app.MapGet("api/Meeting/{meetingId}/Notice/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingNoticeUpdateData(meetingId)).RequireAuthorization();
        
        app.MapPost("api/Meeting/{meetingId}/SendMailToAllAttendees", ([FromServices] IMeetingService meetingService,
                string meetingId, MailDetails mailDetails, CancellationToken token) =>
            meetingService.SendMailToAllAttendees(meetingId, mailDetails, token));
        #endregion

        #region  Retrieve Meeting Details, Meetings
        app.MapGet("api/Meeting/List",
            ([FromServices] IMeetingService meetingService, int? meetingType, string? userId, string? searchString, DateTime? dateTime, PageQuery pageQuery) =>
                meetingService.GetAllMeetingList(meetingType, userId, searchString, dateTime, pageQuery)).RequireAuthorization();
        app.MapGet("api/Meeting/UserMeetings", ([FromServices] IMeetingService meetingService, int? meetingType, PageQuery pageQuery) =>
            meetingService.GetUserMeetingList(pageQuery, meetingType)).RequireAuthorization();
        app.MapGet("api/Meeting/SearchMeetings", ([FromServices] IMeetingService meetingService, int? meetingType, string? userId, string searchMeetingsString, PageQuery pageQuery) =>
            meetingService.SearchMeetings(searchMeetingsString, meetingType, userId)).RequireAuthorization();
        app.MapGet("api/Meeting/SearchMeetingsByDate", ([FromServices] IMeetingService meetingService, int? meetingType, string? userId, DateTime dateTime, PageQuery pageQuery) =>
            meetingService.SearchMeetingsByDate(dateTime, meetingType, userId, pageQuery)).RequireAuthorization();

        #endregion

        #region Meeting Pack
        /*
                app.MapGet("api/Meeting/{meetingId}/MeetingPack", ([FromServices] IMeetingService meetingService,
                        string meetingId) =>
                    meetingService.GetMeetingPack(meetingId));
                app.MapPost("api/Meeting/{meetingId}/MeetingPack/Update", ([FromServices] IMeetingService meetingService,
                        string meetingId, UpdateMeetingPackPOST updateMeetingPackPOST) =>
                    meetingService.UpdateMeetingPack(meetingId, updateMeetingPackPOST));

                app.MapGet("api/Meeting/{meetingId}/MeetingPack/Update", ([FromServices] IMeetingService meetingService,
                        string meetingId) =>
                    meetingService.GetMeetingPackUpdateData(meetingId));
                    */
        #endregion

        #region Minute

        app.MapGet("api/Meeting/{meetingId}/Minutes", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingMinutes(meetingId)).RequireAuthorization();
        app.MapGet("api/Meeting/{meetingId}/Minutes/Update", ([FromServices] IMeetingService meetingService,
                string meetingId) =>
            meetingService.GetMeetingMinutesUpdateData(meetingId));
        app.MapPost("api/Meeting/{meetingId}/Minutes/Update", ([FromServices] IMeetingService meetingService,
              string meetingId, List<UpdateMeetingMinutesPOST> updateMeetingMinutesPOST) =>
          meetingService.UpdateMinutes(meetingId, updateMeetingMinutesPOST)).RequireAuthorization();
        app.MapPost("api/Meeting/{meetingId}/AddMinutes", ([FromServices] IMeetingService meetingService,
                string meetingId, AddMinutePOST data) =>
            meetingService.AddMinutes(meetingId, data)).RequireAuthorization();
        app.MapPost("api/Meeting/{meetingId}/UploadMinutes", ([FromServices] IMeetingService meetingService,
               string meetingId, UploadMinutePOST data) =>
           meetingService.UploadMinutes(meetingId, data)).RequireAuthorization();
        app.MapGet("api/Meeting/{meetingId}/RetrieveUploadedMinutes", ([FromServices] IMeetingService meetingService,
               string meetingId) =>
           meetingService.GetUploadedMinutes(meetingId)).RequireAuthorization();
        app.MapPost("api/Meeting/{meetingId}/UpdateMinutesStatus", ([FromServices] IMeetingService meetingService,
               string meetingId, int minutesStatus) =>
           meetingService.UpdateMinutesStatus(meetingId, minutesStatus )).RequireAuthorization();

        #endregion

        #region Attendance

        app.MapPost("api/Meeting/{meetingId}/Attendance/GenerateCode", ([FromServices] IAttendanceServices meetingService,
                string meetingId, CancellationToken token) =>
            meetingService.GenerateAttendanceCode(meetingId, token)).RequireAuthorization();
        
        app.MapGet("api/Meeting/{meetingId}/Attendance/RetrieveGeneratedCode", ([FromServices] IAttendanceServices meetingService,
                string meetingId, CancellationToken token) =>
            meetingService.RetrieveGeneratedAttendanceCode(meetingId, token)).RequireAuthorization();
        
        app.MapPost("api/Meeting/{meetingId}/Attendance/SendCodeToAll", ([FromServices] IAttendanceServices meetingService,
                string meetingId, CancellationToken token) =>
            meetingService.SendAttendanceCodeToAll(meetingId, token)).RequireAuthorization();
        
        app.MapPost("api/Meeting/{meetingId}/Attendance/SendCodeToUser/{userId}", ([FromServices] IAttendanceServices meetingService,
                string meetingId, string userId, CancellationToken token) =>
            meetingService.SendAttendanceCodeToUser(meetingId, userId, token)).RequireAuthorization(); 
        
        app.MapPost("api/Meeting/{meetingId}/Attendance/RemindAll", ([FromServices] IAttendanceServices meetingService,
                string meetingId, CancellationToken token) =>
            meetingService.NotifyAllToMarkAttendance(meetingId, token)).RequireAuthorization();
        
        app.MapPost("api/Meeting/{meetingId}/Attendance/RemindUser/{userId}", ([FromServices] IAttendanceServices meetingService,
                string meetingId, string userId, CancellationToken token) =>
            meetingService.NotifyUserToMarkAttendance(meetingId, userId, token)).RequireAuthorization();
        
        app.MapPost("api/Meeting/{meetingId}/Attendance/MarkAttendance/{userId}", ([FromServices] IAttendanceServices meetingService,
                string meetingId, string userId, string attendanceCode, CancellationToken token) =>
            meetingService.MarkAttendance(meetingId, userId, attendanceCode, token)).RequireAuthorization();
        
        app.MapGet("api/Meeting/{meetingId}/Attendance/RetrieveAttendanceDetails", ([FromServices] IAttendanceServices meetingService,
                string meetingId, CancellationToken token) =>
            meetingService.GetAttendanceDetails(meetingId, token)).RequireAuthorization();

        #endregion

        #region Resolution

        
        app.MapGet("api/Meeting/{meetingId}/GetResolutionIds", ([FromServices] IMeetingService meetingServices, string meetingId) =>
            meetingServices.GetResolutionIds(meetingId));
        app.MapGet("api/Meeting/{meetingId}/GetPollsByMeetingId", ([FromServices] IMeetingService meetingServices, string meetingId) =>
            meetingServices.GetPollsByMeetingId(meetingId));
        app.MapGet("api/Meeting/{meetingId}/GetVotesByMeetingId", ([FromServices] IMeetingService meetingServices, string meetingId) =>
            meetingServices.GetVotingByMeetingId(meetingId));

        #endregion

        #region TASK

        app.MapPost("api/Resolution/{meetingId}/LinkToMeetingToTask",
            ([FromServices] IMeetingService meetingServices, string meetingId, string taskId) =>
                meetingServices.LinkMeetingToTask(meetingId, taskId)).RequireAuthorization();
        
        app.MapGet("api/Resolution/{meetingId}/RetrieveLinkedTaskByMeetingId",
            ([FromServices] IMeetingService meetingServices, string meetingId) =>
                meetingServices.RetrieveTaskByMeetingId(meetingId)).RequireAuthorization();

        #endregion


        #region Stub Endpoints

        app.MapGet("api/Stub/GetCurrentUserDetails", StaticLogics.DummyGetCurrentEnterpriseUser);

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
        
        builder.Services.AddScoped<IResolutionServices, ResolutionServices>();
        builder.Services.AddScoped<IResolutionMaps, ResolutionMaps>();
        builder.Services.AddScoped<IBridgeRepo, BridgeRepo>();
        
        builder.Services.AddScoped<ITaskService, TaskService>();
        builder.Services.AddScoped<ITaskMaps, TaskMaps>();

        builder.Services.AddScoped<IUtilityService, UtilityService>();
        return builder;
    }

}


