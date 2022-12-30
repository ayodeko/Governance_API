    using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.ClientModels.Meetings.Minute;

namespace GovernancePortal.Service.Interface;

public interface IMeetingService
{
    Task<Response> CreateMeeting(CreateMeetingPOST createMeetingPOST);
    Task<Response> UpdateMeetingDetails(string meetingId, UpdateMeetingPOST createMeetingPOST);
    Task<Response> UpdateAttendingUsers(string meetingId, UpdateAttendingUsersPOST updateAttendingUsersPost);
    Task<Response> UpdateAgendaItems(string meetingId, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST);
    Task<Response> FullUpdateAgendaItems(string meetingId, FullUpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST);
    Task<Response> UpdateMeetingPack(string meetingId, UpdateMeetingPackPOST updateMeetingPackPOST);
    Task<Response> UpdateMinutes(string meetingId, UpdateMeetingMinutesPOST updateMinutesPOST);
    Task<Response> UpdateNotice(string meetingId, UpdateMeetingNoticePOST updateMinutesPOST);
    
    
   
    Task<Response> GetMeetingAttendeesUpdateData(string meetingId);
    Task<Response> GetMeetingAgendaItemsUpdateData(string meetingId);
    Task<Response> GetMeetingAgendaItemsFullUpdateData(string meetingId);
    Task<Response> GetMeetingPackUpdateData(string meetingId);
    Task<Response> GetMeetingNoticeUpdateData(string meetingId);
    
    
    Task<Pagination<MeetingListGET>> GetAllMeetingList(int meetingType, PageQuery pageQuery);
    Task<Response> GetMeetingUpdateData(string meetingId);
    Task<Pagination<MeetingListGET>> GetAllMeetingList(PageQuery pageQuery);
    Task<Pagination<MeetingListGET>> GetAllMeetingList(int? meetingType, PageQuery pageQuery);
    Task<Pagination<MeetingListGET>> GetUserMeetingList(PageQuery pageQuery, int? meetingType);
    Task<Response> SearchMeetings(string meetingSearchString, int? meetingType);
    Task<Response> SearchMeetingsByDate(DateTime meetingDateTime, int? meetingType);
    Task<Response> AddAttendees(string meetingId, AddAttendeesPOST updateMeetingAttendeesPost);
    Task<Response> GetMeetingPack(string meetingId);
    Task<Response> GetMeetingDetails(string meetingId);

    //minute
    Task<Response> AddMinutes(string meetingId, AddMinutePOST data);
    Task<Response> UploadMinutes(string meetingId, UploadMinutePOST data);
    Task<Response> GetMeetingMinutes(string meetingId);
    Task<Response> GetMeetingMinutesUpdateData(string meetingId);
    Task<Response> GetResolutionIds(string meetingId);

    Task<Response> GetPollsByMeetingId(string meetingId);
    Task<Response> GetVotingByMeetingId(string meetingId);
}