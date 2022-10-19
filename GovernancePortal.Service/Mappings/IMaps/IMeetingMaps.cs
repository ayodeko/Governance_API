﻿using System.Collections.Generic;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.Meetings;

namespace GovernancePortal.Service.Mappings.IMaps
{
    public interface IMeetingMaps
    {
        Meeting InMap(CreateMeetingPOST source, Meeting destination);
        Meeting InMap(UpdateMeetingPOST source, Meeting destination);
        Meeting InMap(AddPastMeetingPOST source, Meeting destination);
        Meeting InMap(AddPastMinutesPOST source, Meeting destination);
        Meeting InMap(AddPastAttendancePOST source, Meeting destination);
        List<MeetingListGET> OutMap(List<Meeting> source, List<MeetingListGET> destination);
        MeetingGET OutMap(Meeting source,  MeetingGET destination);
    }
}