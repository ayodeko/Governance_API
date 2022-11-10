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
    public interface IMeetingService_depr
    {
        Task<Response> CreateMeeting(CreateMeetingPOST meetingDto);
        Task<Pagination<MeetingListGet>> GetAllMeetings(PageQuery pageQuery);
        Task<Response> UpdateMeeting(string meetingId, UpdateMeetingPOST meetingDto);
        Task<Response> AddPastMeeting(AddPastMeetingPOST meetingDto);
        Task<Response> AddPastMinutes(AddPastMinutesPOST meetingDto);
        Task<Response> AddPastAttendance(AddPastAttendancePOST meetingDto);
    }
}