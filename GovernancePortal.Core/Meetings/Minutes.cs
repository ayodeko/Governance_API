using System;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Meetings
{
    public class Minute
    {
        public Minute()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string MeetingId { get; set; }
        public string CompanyId { get; set; }
        public MeetingAgendaItem AgendaItem { get; set; }
        public string AgendaItemId { get; set; }
        public string MinuteText { get; set; }
        public bool IsApproved { get; set; }
        public Attachment Attachment { get; set; }
    }
}