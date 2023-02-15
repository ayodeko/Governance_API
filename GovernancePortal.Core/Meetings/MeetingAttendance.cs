using System;
using System.Collections.Generic;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Meetings
{
    public class MeetingAttendance : BaseModel
    {
        public MeetingAttendance()
        {
            Id = Guid.NewGuid().ToString();
        }
        public new string Id { get; set; }
        public string MeetingId { get; set; }
        public string GeneratedCode { get; set; }
        public List<AttendingUser> Attendees { get; set; }
    }

    public class AttendingUser : BaseModel
    {
        public AttendingUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        public new string Id { get; set; }
        public string UserId { get; set; }
        public string MeetingId { get; set; }
        public string MeetingAttendanceId { get; set; }
        public bool IsPresent { get; set; }
        public bool IsGuest { get; set; }
        public bool IsMinuteApproved { get; set; }
        public bool IsRestricted { get; set; }

        public string Name { get; set; }
        public AttendeePosition AttendeePosition { get; set; }
    }

    public enum AttendeePosition
    {
        MeetingOfficial = 0,
        Participant = 2,
        Guest = 3
    }
}