using System.Threading.Tasks;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.ClientModels.TaskManagement;

namespace GovernancePortal.Service.Interface
{
    public interface IMeetingService
    {
        Task<MeetingModel> CreateMeeting(Person user, CreateMeetingPOST meetingDto);
        Task<MeetingModel> UpdateMeeting(string meetingId, Person user, UpdateMeetingPOST meetingDto);
    }
}