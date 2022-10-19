using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string CompanyId { get; set; }
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
        public string MeetingAgendaItemId { get; set; }
        public string MeetingId { get; set; }
        public string CompanyId { get; set; }
        public string Description { get; set; }
        public string PresenterUserId { get; set; }
        public List<MeetingPackItemUser> CoCreators { get; set; }
        public List<MeetingPackItemUser> RestrictedUsers { get; set; }
        public List<MeetingPackItemUser> InterestTagUsers { get; set; }
        public Attachment Attachment { get; set; }
        public DateTime Duration { get; set; }
        public ActionRequired ActionRequired { get; set; }
    }

    public enum ActionRequired
    {
        Others
    }

    public class MeetingPackItemUser
    {
        public MeetingPackItemUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string AttendingUserId { get; set; }
        public AttendingUser AttendingUser { get; set; }
        public string CoCreatorId { get; set; }
        public string RestrictedUserId { get; set; }
        public string InterestTagUserId { get; set; }
    }
}