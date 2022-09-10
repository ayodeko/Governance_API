using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.Meetings;

namespace GovernancePortal.Service.Mappings.IMaps
{
    public interface IMeetingMaps
    {
        MeetingModel InMap(CreateMeetingPOST source, MeetingModel destination);
        MeetingModel InMap(UpdateMeetingPOST source, MeetingModel destination);
    }
}