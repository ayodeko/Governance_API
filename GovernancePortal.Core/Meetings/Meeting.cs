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
            Attendance = new MeetingAttendance();
            Minutes = new List<Minute>();
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
        public bool IsMeetingPackPublished { get; set; }
        public bool IsAttendanceSaved { get; set; }
        public bool IsPast { get; set; }
        public MeetingFrequency Frequency { get; set; }
        public MeetingMedium Medium { get; set; }
        public MeetingType Type { get; set; }
        public MeetingStatus Status { get; set; }
        public MinutesStatus MinutesStatus { get; set; }
        public NoticesStatus NoticesStatus { get; set; }
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public List<AttendingUser> Attendees { get; set; }
        public List<MeetingAgendaItem> Items { get; set; }
        public List<MeetingPackItem> Packs { get; set; }
        public List<Minute> Minutes { get; set; }
        public StandAloneMinute StandAloneMinute { get; set; }
        public MeetingAttendance Attendance { get; set; }
        public NoticeMeeting Notice { get; set; }
        public string MeetingPackId { get; set; }
        public string AttendanceGeneratedCode { get; set; }
        public bool isMinutesUploaded { get; set; }
    }

    public enum MeetingType
    {
        Board = 0,
        Management = 1,
        Others = 2
    }

    public enum MinutesStatus
    {
        Default = 0,
        Taken = 1,
        SentForApproval = 2,
        Approved = 3
    }
    public enum NoticesStatus
    {
        Default = 0,
        Drafts = 1,
        Sent = 2
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
        Edited = 0, Ended = 3
    }
    
}