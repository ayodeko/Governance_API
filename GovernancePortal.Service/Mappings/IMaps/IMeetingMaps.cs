using System.Collections.Generic;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.Meetings;
using GovernancePortal.Service.ClientModels.Meetings.Minute;

namespace GovernancePortal.Service.Mappings.IMaps;

public interface IMeetingMaps
{
    Meeting InMap(CreateMeetingPOST createMeetingPost, Meeting meeting);
    Meeting InMap(UpdateMeetingPOST createMeetingPost, Meeting meeting);
    Meeting InMap(UpdateAttendingUsersPOST updateAttendingUsersPost, Meeting meeting);
    Meeting InMap(UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost, List<MeetingAgendaItem> agendaItems, Meeting meeting);
    Meeting InMap(FullUpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost, List<MeetingAgendaItem> agendaItems, Meeting meeting);
    Meeting InMap(UpdateMeetingPackPOST updateMeetingPackPost, Meeting existingMeeting);
    Meeting InMap(UpdateMeetingMinutesPOST updateMinutesPost, Meeting existingMeeting);
    MeetingListGET OutMap(Meeting existingMeeting, MeetingListGET meetingList);
    List<MeetingListGET> OutMap(List<Meeting> existingMeeting);
    List<AttendingUser> InMap(List<AttendingUserPOST> updateMeetingAttendeesPost, Meeting meeting);

    UpdateAttendingUsersPOST OutMap(Meeting existingMeeting,
        UpdateAttendingUsersPOST updateAttendingUsersPost);

    UpdateMeetingAgendaItemPOST OutMap(Meeting existingMeeting, UpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost);
    FullUpdateMeetingAgendaItemPOST OutMap(Meeting existingMeeting, FullUpdateMeetingAgendaItemPOST updateMeetingAgendaItemPost);
    List<UpdateMeetingPackItemPOST> OutMap(Meeting existingMeeting, List<UpdateMeetingPackItemPOST> updateMeetingAgendaItemPOST);
    MeetingGET OutMap(Meeting existingMeeting, MeetingGET updateMeetingAttendingUserPost);
    UpdateMeetingGET OutMap(Meeting existingMeeting);
    UpdateMeetingNoticePOST OutMap(NoticeMeeting existingNoticeMeeting, UpdateMeetingNoticePOST updateMeetingAttendingUserPost);
    Meeting InMap(UpdateMeetingNoticePOST updateMeetingAgendaItemPost, Meeting existingMeeting);

    //minute
    Meeting InMap(AddMinutePOST source, Meeting destination);
    Meeting InMap(UploadMinutePOST source, Meeting destination);
    List<MinuteGET> OutMap(List<Minute> source, MinuteGET destination);

}
