using System;
using System.Collections.Generic;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Meetings
{
    public class Meeting : ICompanyModel
    {
        public Meeting()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ChairPersonId { get; set; }
        public string SecretaryId { get; set; }
        
        public MeetingFrequency Frequency { get; set; }
        public MeetingMedium Medium { get; set; }
        public MeetingType Type { get; set; }
        public MeetingStatus Status { get; set; }
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public List<AttendingUser> Attendees { get; set; }
        public List<MeetingAgendaItem> Items { get; set; }
        public List<Guest> Guests { get; set; }
        public Minutes Minutes { get; set; }
        public MeetingAttendance Attendance { get; set; }
        public string MeetingPackId { get; set; }
    }

    public class Guest
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public enum MeetingType
    {
        Board,
        Management,
        Others
    }

    public enum MeetingFrequency
    {
        Daily, Weekly, Monthly
    }

    public enum MeetingMedium
    {
        Physical, Virtual, Hybrid
    }
    public enum MeetingStatus
    {
        Edited, NoticeSent
    }
    
}