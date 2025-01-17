﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GovernancePortal.Core.Meetings;

namespace GovernancePortal.Data.Repository;


public interface IMeetingRepo : IGenericRepo<Meeting>
{
    Task<Meeting> GetMeeting(string meetingId, string companyId);
    Task<AttendingUser> GetAttendingUsers(string meetingId, string companyId, CancellationToken token = default);
    Task<Meeting> GetMeeting_AllDependencies(string meetingId, string companyId);
    Task<Meeting> GetMeeting_Attendees(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AgendaItems(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AgendaItems_Relationships(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AgendaItems_Attendees_Notice(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AgendaItems_Attendees_Minutes(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AgendaItems_MeetingPack(string meetingId, string companyId);
    Task<Meeting> GetMeeting_MeetingPack(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AgendaItems_Attendees_MeetingPack(string meetingId, string companyId);

    
    IEnumerable<Meeting> GetMeetingListByMeetingType(MeetingType meetingType, string companyId, int pageNumber, int pageSize,
        out int totalRecords);
    IEnumerable<Meeting> GetMeetingListByMeetingTypeAndUserId(MeetingType meetingType, string userId, string companyId, int pageNumber, int pageSize,
        out int totalRecords);
    Task<Meeting> GetMeeting_Minutes(string meetingId, string companyId);
    Task<Meeting> GetMeeting_Minute(string meetingId, string companyId);
    IEnumerable<Meeting> GetMeetingListByUserId(string userId, MeetingType? meetingType, string companyId, int pageNumber, int pageSize,
        out int totalRecords);
    IEnumerable<Meeting> GetMeetingList(string companyId, MeetingType? meetingType, string userId, string searchString, DateTime? DateTime, int pageNumber, int pageSize,
        out int totalRecords);
    IEnumerable<Meeting> FindBySearchString(string searchString, string companyId);
    IEnumerable<Meeting> FindBySearchStringAndMeetingType(string searchString, MeetingType? meetingType, string userId, string companyId, int pageNumber, int pageSize,
        out int totalRecords);
    IEnumerable<Meeting> FindMeetingByDate(DateTime dateTime, MeetingType? meetingType, string userId, string companyId, int pageNumber, int pageSize,
        out int totalRecords);
    IEnumerable<MeetingAgendaItem> GetAgendaItems_With_MeetingHolder(string meetingId, string companyId);
}
