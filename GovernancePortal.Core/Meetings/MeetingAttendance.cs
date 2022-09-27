using System.Collections.Generic;

namespace GovernancePortal.Core.Meetings
{
    public class MeetingAttendance
    {
        public string Id { get; set; }
        public string MeetingId { get; set; }
        public string GeneratedCode { get; set; }
        public List<AttendingUser> Attendees { get; set; }
    }

    public class AttendingUser
    {
        public string UserId { get; set; }
        public bool IsPresent { get; set; }
        public string Name { get; set; }
        public AttendeePosition AttendeePosition { get; set; }
    }

    public enum AttendeePosition
    {
        Moderator,
        Secretary,
        Participant,
        Guest
    }
}