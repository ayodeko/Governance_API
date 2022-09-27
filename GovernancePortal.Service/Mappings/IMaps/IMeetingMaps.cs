using System.Collections.Generic;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.Meetings;

namespace GovernancePortal.Service.Mappings.IMaps
{
    public interface IMeetingMaps
    {
        MeetingModel InMap(CreateMeetingPOST source, MeetingModel destination);
        MeetingModel InMap(UpdateMeetingPOST source, MeetingModel destination);
        MeetingModel InMap(AddPastMeetingPOST source, MeetingModel destination);
        MeetingModel InMap(AddPastMinutesPOST source, MeetingModel destination);
        MeetingModel InMap(AddPastAttendancePOST source, MeetingModel destination);
        List<MeetingListGET> OutMap(List<MeetingModel> source, List<MeetingListGET> destination);
        MeetingGET OutMap(MeetingModel source,  MeetingGET destination);
    }
}