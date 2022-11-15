using System;
using System.Threading.Tasks;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;

namespace GovernancePortal.Service.Interface;

public interface IMeetingService
{
    Task<Response> CreateMeeting(CreateMeetingPOST createMeetingPOST);
    Task<Response> UpdateAttendingUsers(string meetingId, UpdateAttendingUsersPOST updateAttendingUsersPost);
    Task<Response> UpdateAgendaItems(string meetingId, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPOST);
    Task<Response> UpdateMeetingPack(string meetingId, UpdateMeetingPackPOST updateMeetingPackPOST);
    Task<Response> UpdateMinutes(string meetingId, UpdateMeetingMinutesPOST updateMinutesPOST);
    Task<Response> UpdateNotice(string meetingId, UpdateMeetingNoticePOST updateMinutesPOST);
    
    
    Task<Response> GetMeetingUpdateData(string meetingId);
    Task<Response> GetMeetingMinutesUpdateData(string meetingId);
    Task<Response> GetMeetingAttendeesUpdateData(string meetingId);
    Task<Response> GetMeetingAgendaItemsUpdateData(string meetingId);
    Task<Response> GetMeetingPackUpdateData(string meetingId);
    Task<Response> GetMeetingNoticeUpdateData(string meetingId);
    
    
    Task<Pagination<MeetingListGET>> GetAllMeetingList(PageQuery pageQuery);
    Task<Pagination<MeetingListGET>> GetUserMeetingList(PageQuery pageQuery);
    Task<Response> SearchMeetings(string meetingSearchString);
    Task<Response> SearchMeetingsByDate(DateTime meetingDateTime);
    Task<Response> AddAttendees(string meetingId, AddAttendeesPOST updateMeetingAttendeesPost);
}