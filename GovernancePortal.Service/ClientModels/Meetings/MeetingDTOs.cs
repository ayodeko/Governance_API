using System;
using System.Collections.Generic;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;

namespace GovernancePortal.Service.ClientModels.Meetings;

public class MeetingDTOs
{
    
}


public class UpdateMeetingPackPOST
{
    public string MeetingId { get; set; }
    public List<UpdateMeetingPackItemPOST> Packs { get; set; }
}

public class s
{
    public string MeetingAgendaItemId { get; set; }
    public string MeetingId { get; set; }
    public string Description { get; set; }
    public string PresenterUserId { get; set; }
    public List<string> CoCreators { get; set; }
    public List<string> RestrictedUsers { get; set; }
    public List<string> InterestTagUsers { get; set; }
    public int Duration { get; set; }
}

public class UpdateMeetingPackItemPOST
{
    public string Id { get; set; }
    public string MeetingAgendaItemId { get; set; }
    public string Title { get; set; }
    public string MeetingId { get; set; }
    public string Description { get; set; }
    public string PresenterUserId { get; set; }
    public List<MeetingPackUserPOST> CoCreators { get; set; }
    public List<MeetingPackUserPOST> RestrictedUsers { get; set; }
    public List<MeetingPackUserPOST> InterestTagUsers { get; set; }
    public int Duration { get; set; }
}

public class MeetingPackUserPOST
{
    public string Id { get; set; }
    public string UserId { get; set; }
}

public class UpdateMeetingAgendaItemPOST
{
    public List<AgendaItemPOST> Items { get; set; }
}

public class FullUpdateMeetingAgendaItemPOST
{
    public List<FullAgendaItemPOST> Items { get; set; }
}

public class UpdateMeetingMinutesPOST
{
}

public class BaseAgendaItemGET
{
    public string AgendaItemId { get; set; }
    public string Title { get; set; }
}

public class UpdateMeetingNoticePOST
{
    public string Id { get; set; }
    public List<AttendingUserPOST> Attendees { get; set; }
    public List<BaseAgendaItemGET> AgendaItems { get; set; }
    public string Salutation { get; set; }
    public DateTime MeetingDate { get; set; }
    public string Venue { get; set; }
    public string NoticeText { get; set; }
    public string AgendaText { get; set; }
    public string Mandate { get; set; }
    public string Signatory { get; set; }
    public Attachment Signature { get; set; }
    public DateTime NoticeDate { get; set; }
    public DateTime SendNoticeDate { get; set; }
}

public record MailDetails(string subject, string body);








public class MeetingBaseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsAttendanceTaken { get; set; }
    public bool IsMinutesPublished { get; set; }
    public bool IsMeetingPackDownloadable { get; set; }
    public bool IsMeetingPackPublished { get; set; }
    public bool IsPast { get; set; }
    public string ChairPersonId { get; set; }
    public string SecretaryId { get; set; }
        
    public MeetingFrequency Frequency { get; set; }
    public MeetingMedium Medium { get; set; }
    public MeetingType Type { get; set; }
    public MeetingStatus Status { get; set; }
    public int Duration { get; set; }
    public DateTime DateTime { get; set; }
    public string CreatedBy { get; set; }
}

public class CreateMeetingPOST
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Venue { get; set; }
    public string Link { get; set; }
        
    public MeetingFrequency Frequency { get; set; }
    public MeetingMedium Medium { get; set; }
    public MeetingType Type { get; set; }
    public int Duration { get; set; }
    public DateTime DateTime { get; set; }
    
    public bool IsPast { get; set; }
}

public class MeetingPOST : MeetingBaseDto
{
    
}

public class MeetingGET : MeetingBaseDto
{
    public List<AttendingUserPOST> Attendees { get; set; }
    public List<FullAgendaItemPOST> Items { get; set; }
    
}

public class MeetingListGET : MeetingBaseDto
{
    public string Id { get; set; }
    public List<AttendingUserPOST> Attendees { get; set; }
}

public class UpdateMeetingGET
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsMeetingPackDownloadable { get; set; }
    public bool IsMeetingPackPublished { get; set; }
    public bool IsAttendanceTaken { get; set; }
    public bool IsAttendanceSaved { get; set; }
    public bool IsPast { get; set; }
    public string Venue { get; set; }
    public string Link { get; set; }
    public string SecretaryUserId { get; set; }
    public string ChairPersonUserId { get; set; }
    public MeetingFrequency Frequency { get; set; }
    public MeetingMedium Medium { get; set; }
    public MeetingType Type { get; set; }
    public int Duration { get; set; }
    public DateTime DateTime { get; set; }
    public string CreatedBy { get; set; }
}

public class UpdateMeetingPOST
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsMeetingPackDownloadable { get; set; }
    public bool IsMeetingPackPublished { get; set; }
    public bool IsAttendanceTaken { get; set; }
    public bool IsAttendanceSaved { get; set; }
    public bool IsPast { get; set; }
    public string Venue { get; set; }
    public string Link { get; set; }
    public string SecretaryUserId { get; set; }
    public string ChairPersonUserId { get; set; }
    public MeetingFrequency Frequency { get; set; }
    public MeetingMedium Medium { get; set; }
    public MeetingType Type { get; set; }
    public int Duration { get; set; }
    public DateTime DateTime { get; set; }
}

public class UpdateAttendingUsersPOST
{
    public string ChairPersonUserId { get; set; }
    public string SecretaryUserId { get; set; }
    public List<AttendingUserPOST> Attendees { get; set; }
}
public class AttendingUserPOST
{
    public new string Id { get; set; }
    public string UserId { get; set; }
    public string MeetingAttendanceId { get; set; }
    public bool IsPresent { get; set; }
    public bool IsGuest { get; set; }
    public string Name { get; set; }
    public AttendeePosition AttendeePosition { get; set; }
}

public class AddAttendeesPOST
{
    public string ChairPersonUserId { get; set; }
    public string SecretaryUserId { get; set; }
    public List<AddAttendeesListPOST> Attendees { get; set; }
}
public class AddAttendeesListPOST
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public AttendeePosition AttendeePosition { get; set; }
}

public class AgendaItemPOST
{
    public new string Id { get; set; }
    public new string ParentId { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public List<AgendaItemPOST> SubItems { get; set; }
}
public class FullAgendaItemPOST
{
    public new string Id { get; set; }
    public new string ParentId { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PresenterUserId { get; set; }
    public int Duration { get; set; }
    public ActionRequired ActionRequired { get; set; }
    public Attachment Attachment { get; set; }
    
    
    public List<MeetingPackUserPOST> CoCreators { get; set; }
    public List<MeetingPackUserPOST> RestrictedUsers { get; set; }
    public List<MeetingPackUserPOST> InterestTagUsers { get; set; }
    public List<SubAgendaItemPOST> SubItems { get; set; }
}
public class SubAgendaItemPOST
{
    public new string Id { get; set; }
    public new string ParentId { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string PresenterUserId { get; set; }
    public int Duration { get; set; }
    public ActionRequired ActionRequired { get; set; }
    public List<SubAgendaItemPOST> SubItems { get; set; }
}
