using System;
using System.Collections.Generic;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Meetings
{
    public class MeetingAgendaItem : BaseModel
    {
        public MeetingAgendaItem()
        {
            Id = Guid.NewGuid().ToString();
            SubItems = new List<MeetingAgendaItem>();
            CoCreators = new List<MeetingPackItemUser>();
            RestrictedUsers = new List<MeetingPackItemUser>();
            InterestTagUsers = new List<MeetingPackItemUser>();
        }
        public string Id { get; set; }
        public string MeetingId { get; set; }
        public string MeetIdHolder { get; set; }
        public string AgendaItemId { get; set; }
        public string ParentId { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public bool IsNumbered { get; set; }
        public List<MeetingAgendaItem> SubItems { get; set; }
        
        
        
        //Meeting Pack Items
        
        public string Description { get; set; }
        public string PresenterUserId { get; set; }
        public List<MeetingPackItemUser> CoCreators { get; set; }
        public List<MeetingPackItemUser> RestrictedUsers { get; set; }
        public List<MeetingPackItemUser> InterestTagUsers { get; set; }
        public Attachment Attachment { get; set; }
        public int Duration { get; set; }
        public ActionRequired ActionRequired { get; set; }
    }
    public class MeetingAgendaSubItem
    {
        public MeetingAgendaSubItem()
        {
            Id = Guid.NewGuid().ToString();
            AgendaItemId = Id;
        }
        public string Id { get; set; }
        public string MeetingId { get; set; }
        public string AgendaItemId { get; set; }
        public string ParentId { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public bool IsNumbered { get; set; }
        public List<MeetingAgendaItem> SubItems { get; set; }
    }
}