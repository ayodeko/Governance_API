using System;
using System.Collections.Generic;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Meetings
{
    public class MeetingPack
    {
        public MeetingPack()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string MeetingId { get; set; }
        public bool Published { get; set; }
        public bool Downloadable { get; set; }
        public List<MeetingPackItem> MeetingPackItems { get; set; }
    }
    
    public class MeetingPackItem
    {
        public MeetingPackItem()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string MeetingPackId { get; set; }
        public string AgendaItemId { get; set; }
        public string Description { get; set; }
        public string PresenterUserId { get; set; }
        public List<AttendingUser> CoCreators { get; set; }
        public List<AttendingUser> RestrictedUsers { get; set; }
        public List<AttendingUser> InterestTagUsers { get; set; }
        public Attachment Attachment { get; set; }
        public DateTime Duration { get; set; }
        public ActionRequired ActionRequired { get; set; }
    }

    public enum ActionRequired
    {
        Others
    }
}