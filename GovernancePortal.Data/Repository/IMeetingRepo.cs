using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.Meetings;

namespace GovernancePortal.Data.Repository;


public interface IMeetingRepo : IGenericRepo<Meeting>
{
    Task<Meeting> GetMeeting(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AllDependencies(string meetingId, string companyId);
    Task<Meeting> GetMeeting_Attendees(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AgendaItems(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AgendaItems_Attendees_Notice(string meetingId, string companyId);
    Task<Meeting> GetMeeting_AgendaItems_MeetingPack(string meetingId, string companyId);
    IEnumerable<Meeting> GetMeetingListByUserId(string userId, string companyId, int pageNumber, int pageSize,
        out int totalRecords);
    IEnumerable<Meeting> FindBySearchString(string searchString, string companyId);
    IEnumerable<Meeting> FindByDate(DateTime dateTime, string companyId);
    IEnumerable<MeetingAgendaItem> GetAgendaItems_With_MeetingHolder(string meetingId, string companyId);
}
