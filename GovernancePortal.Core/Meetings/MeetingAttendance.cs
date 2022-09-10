using System.Collections.Generic;

namespace GovernancePortal.Core.Meetings
{
    public class MeetingAttendance
    {
        public string Id { get; set; }
        public string MeetingId { get; set; }
        public List<AttendingUser> AttendingUsers { get; set; }
    }

    public class AttendingUser
    {
        public string UserId { get; set; }
        public bool Marked { get; set; }
    }
}