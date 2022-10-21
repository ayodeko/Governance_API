using System;
using System.Collections.Generic;

namespace GovernancePortal.Core.Meetings
{
    public class MeetingAgendaItem
    {
        public MeetingAgendaItem()
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