using System;
using System.Collections.Generic;
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

public class UpdateMeetingPackItemPOST
{
    public string AgendaItemId { get; set; }
    public string MeetingId { get; set; }
    public string Description { get; set; }
    public string PresenterUserId { get; set; }
    public List<string> CoCreators { get; set; }
    public List<string> RestrictedUsers { get; set; }
    public List<string> InterestTagUsers { get; set; }
    public DateTime Duration { get; set; }
}

public class UpdateMeetingAttendeesPOST
{
    public string MeetingId { get; set; }
    public List<AttendingUserPOST> Attendees { get; set; }
}
public class UpdateMeetingAgendaItemPOST
{
    public string MeetingId { get; set; }
    public List<AgendaItemPOST> Items { get; set; }
}
public class UpdateMeetingAgendaItemGET
{
    public string MeetingId { get; set; }
    public List<AgendaItemPOST> Items { get; set; }
}
public class UpdateMeetingMinutesPOST
{
}










public class MeetingBaseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsAttendanceTaken { get; set; }
    public bool IsMinutesPublished { get; set; }
    public string CompanyId { get; set; }
    public string ChairPersonId { get; set; }
    public string SecretaryId { get; set; }
        
    public MeetingFrequency Frequency { get; set; }
    public MeetingMedium Medium { get; set; }
    public MeetingType Type { get; set; }
    public MeetingStatus Status { get; set; }
    public int Duration { get; set; }
    public DateTime DateTime { get; set; }
}

public class MeetingPOST : MeetingBaseDto
{
    
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
    public MeetingFrequency Frequency { get; set; }
    public MeetingMedium Medium { get; set; }
    public MeetingType Type { get; set; }
    public int Duration { get; set; }
    public DateTime DateTime { get; set; }
}

public class UpdateMeetingAttendingUserGET
{
    public string MeetingId { get; set; }
    private List<AttendingUserPOST> Attendees { get; set; }
}
public class AttendingUserPOST
{
    public new string Id { get; set; }
    public string UserId { get; set; }
    public string MeetingId { get; set; }
    public string MeetingAttendanceId { get; set; }
    public bool IsPresent { get; set; }
    public bool IsGuest { get; set; }
    public string Name { get; set; }
    public AttendeePosition AttendeePosition { get; set; }
}
public class AgendaItemPOST
{
    public new string Id { get; set; }
    public string MeetingId { get; set; }
    public int Number { get; set; }
    public string Title { get; set; }
    public List<AgendaItemPOST> SubItems { get; set; }
}
public class SubAgendaItemPOST
{
    public string MeetingId { get; set; }
    public List<SubAgendaItemPOST> SubItems { get; set; }
}