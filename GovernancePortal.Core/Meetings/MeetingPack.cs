using System;
using System.Collections.Generic;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Meetings
{
    public class MeetingPack
    {
        public string Id { get; set; }
        public string MeetingId { get; set; }
        public bool Published { get; set; }
        public bool Downloadable { get; set; }
        public List<MeetingPackItem> MeetingPackItems { get; set; }
    }
    
    public class MeetingPackItem
    {
        public string MeetingPackId { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string PresenterUserId { get; set; }
        public List<string> CoCreatorUserIds { get; set; }
        public List<string> RestrictedUserIds { get; set; }
        public List<string> InterestTagUserIds { get; set; }
        public Attachment Attachment { get; set; }
        public DateTime Duration { get; set; }
        public ActionRequired ActionRequired { get; set; }
    }

    public enum ActionRequired
    {
        Others
    }
}