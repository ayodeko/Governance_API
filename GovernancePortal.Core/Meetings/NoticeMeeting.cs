using System;
using GovernancePortal.Core.General;

namespace GovernancePortal.Core.Meetings;

public class NoticeMeeting : BaseModel
{
    public NoticeMeeting()
    {
        Id = Guid.NewGuid().ToString();
    }
    public new string Id { get; set; }
    public string MeetingId { get; set; }
    public string Salutation { get; set; }
    public string NoticeText { get; set; }
    public string AgendaText { get; set; }
    public string Mandate { get; set; }
    public string Signatory { get; set; }
    public Attachment Signature { get; set; }
    public DateTime NoticeDate { get; set; }
    public DateTime SendNoticeDate { get; set; }
}