using System;
using System.Collections.Generic;
using GovernancePortal.Core.Meetings;

namespace GovernancePortal.Service.ClientModels.Meetings
{
    public class MeetingDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChairPersonId { get; set; }
        public string SecretaryId { get; set; }
        
        public MeetingFrequency Frequency { get; set; }
        public MeetingMedium Medium { get; set; }
        public MeetingType Type { get; set; }
        public int Duration { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class CreateMeetingPOST : MeetingDTO
    {
        public List<string> AttendeesId { get; set; }
        public List<MeetingAgendaItem> Items { get; set; }
    }
    
    public class UpdateMeetingPOST : MeetingDTO
    {
        public List<string> AttendeesId { get; set; }
        public List<MeetingAgendaItem> Items { get; set; }
    }

    public class UpdateMeetingGet : MeetingDTO
    {
        public string Id { get; set; }
    }

    public class AddPastMeetingPOST : MeetingDTO
    {
        public MeetingAttendance Attendance { get; set; }
        public Minutes Minutes { get; set; }
    }
    
    public class AddPastMinutesPOST : MeetingDTO
    {
        public Minutes Minutes { get; set; }
    }

    public class MeetingListGET : MeetingDTO
    {
        public string AttendanceId { get; set; }
        public string MinutesId { get; set; }
    }
}