using System;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Meetings
{
    public class Minutes
    {
        public Minutes()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string MeetingId { get; set; }
        public string CompanyId { get; set; }
        public MeetingAgendaItem AgendaItem { get; set; }
        public string AgendaItemId { get; set; }
        public string MinuteText { get; set; }
        public string SignerUserId { get; set; }
        public Attachment Attachment { get; set; }
    }
}