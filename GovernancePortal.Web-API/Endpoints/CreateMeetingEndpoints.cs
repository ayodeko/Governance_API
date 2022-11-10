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
            ([FromServices] IMeetingService meetingService, MeetingPOST createMeetingPost) =>
                meetingService.CreateMeeting(createMeetingPost));
        
        app.MapPost("api/Meeting/{meetingId}/Attendees/Update", ([FromServices] IMeetingService meetingService,
                string meetingId, UpdateMeetingAttendeesPOST updateMeetingAttendeesPost) =>
            meetingService.UpdateAttendees(meetingId, updateMeetingAttendeesPost));
        
        app.MapPost("api/Meeting/{meetingId}/AgendaItems/Update", ([FromServices] IMeetingService meetingService,
               string meetingId, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST) =>
            meetingService.UpdateAgendaItems(meetingId, updateMeetingAgendaItemPOST));
        
        app.MapPost("api/Meeting/{meetingId}/MeetingPack/Update", ([FromServices] IMeetingService meetingService,
                string meetingId, UpdateMeetingPackPOST updateMeetingPackPOST) =>
            meetingService.UpdateMeetingPack(meetingId, updateMeetingPackPOST));
        
        app.MapPost("api/Meeting/{meetingId}/Minutes/Update", ([FromServices] IMeetingService meetingService,
                string meetingId, UpdateMeetingMinutesPOST updateMeetingMinutesPOST) =>
            meetingService.UpdateMinutes(meetingId, updateMeetingMinutesPOST));
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
        
        
        return app;
    }

    public static WebApplicationBuilder RegisterMeetingServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMeetingService, MeetingServices>();
        builder.Services.AddScoped<IMeetingMaps, MeetingMaps>();
        builder.Services.AddScoped<ILogger, StubLogger>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        return builder;
    }

}


