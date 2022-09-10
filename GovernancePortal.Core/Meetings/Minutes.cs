namespace GovernancePortal.Core.Meetings
{
    public class Minutes
    {
        public string MeetingId { get; set; }
        public string AgendaItemId { get; set; }
        public string MinuteText { get; set; }
        public string PresenterUserId { get; set; }
        public string SignerUserId { get; set; }
    }
}