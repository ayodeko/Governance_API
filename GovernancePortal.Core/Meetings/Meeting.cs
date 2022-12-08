using System;
using System.Collections.Generic;
using System.Linq;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Meetings
{
    public class Meeting : ICompanyModel
    {
        public Meeting()
        {
            Id = Guid.NewGuid().ToString();
            Attendees = new List<AttendingUser>();
            Items = new List<MeetingAgendaItem>();
            Packs = new List<MeetingPackItem>();
            Minutes = new Minutes();
            Attendance = new MeetingAttendance();
        }
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public ModelStatus ModelStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Venue { get; set; }
        public string Link { get; set; }
        public string ChairPersonUserId { get; set; }
        public string SecretaryUserId { get; set; }
        public bool IsAttendanceTaken { get; set; }
        public bool IsMinutesPublished { get; set; }
        public bool IsMeetingPackDownloadable { get; set; }

        public MeetingFrequency Frequency { get; set; }
        public MeetingMedium Medium { get; set; }
        public MeetingType Type { get; set; }
        public MeetingStatus Status { get; set; }
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public List<AttendingUser> Attendees { get; set; }
        public List<MeetingAgendaItem> Items { get; set; }
        public List<MeetingPackItem> Packs { get; set; }
        public Minutes Minutes { get; set; }
        public MeetingAttendance Attendance { get; set; }
        public NoticeMeeting Notice { get; set; }
        public string MeetingPackId { get; set; }
        public string AttendanceGeneratedCode { get; set; }
    }

    public enum MeetingType
    {
        Board = 0,
        Management = 1,
        Others = 2
    }

    public enum MeetingFrequency
    {
        Once = 0, Daily = 1, Weekly = 2, Monthly = 3
    }

    public enum MeetingMedium
    {
        Physical = 0, Virtual = 1, Hybrid = 2
    }
    public enum MeetingStatus
    {
        Edited = 0, NoticeSent = 1
    }
    
}