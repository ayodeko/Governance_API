using System.Collections.Generic;
using System.Threading.Tasks;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.ClientModels.TaskManagement;

namespace GovernancePortal.Service.Interface
{
    public interface IMeetingService
    {
        Task<MeetingModel> CreateMeeting(Person user, CreateMeetingPOST meetingDto);
        Task<Pagination<MeetingListGET>> GetAllMeetings(string companyId, PageQuery pageQuery);
        Task<MeetingModel> UpdateMeeting(string meetingId, Person user, UpdateMeetingPOST meetingDto);
        Task<MeetingModel> AddPastMeeting(Person user, AddPastMeetingPOST meetingDto);
        Task<MeetingModel> AddPastMinutes(Person user, AddPastMinutesPOST meetingDto);
    }
}