using System.Collections.Generic;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.Meetings;

namespace GovernancePortal.Service.Mappings.IMaps;

public interface IMeetingMaps
{
    Meeting InMap(MeetingPOST createMeetingPost, Meeting meeting);
    Meeting InMap(UpdateMeetingAttendeesPOST updateMeetingAttendeesPost, Meeting meeting);
    Meeting InMap(UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost, Meeting meeting);
    Meeting InMap(UpdateMeetingPackPOST updateMeetingPackPost, Meeting existingMeeting);
    Meeting InMap(UpdateMeetingMinutesPOST updateMinutesPost, Meeting existingMeeting);
    MeetingListGET OutMap(Meeting existingMeeting, MeetingListGET meetingList);
    List<MeetingListGET> OutMap(List<Meeting> existingMeeting);

    UpdateMeetingAttendingUserGET OutMap(Meeting existingMeeting,
        UpdateMeetingAttendingUserGET updateMeetingAttendingUserGet);

    UpdateMeetingAgendaItemGET OutMap(Meeting existingMeeting, UpdateMeetingAgendaItemGET updateMeetingAgendaItemGet);
    List<UpdateMeetingPackItemGET> OutMap(Meeting existingMeeting, List<UpdateMeetingPackItemGET> updateMeetingAgendaItemGet);
    UpdateMeetingGET OutMap(Meeting existingMeeting);
    UpdateMeetingNoticeGET OutMap(MeetingNotice existingMeetingNotice, UpdateMeetingNoticeGET updateMeetingAttendingUserGet);
}
