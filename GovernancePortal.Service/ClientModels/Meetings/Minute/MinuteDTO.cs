using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Service.ClientModels.Meetings.Minute
{
    //add minute
    public class AddMinuteDTO
    {
        public string Id { get; set; }
        public string MinuteText { get; set; }
    }

    public class AddMinutePOST: AddMinuteDTO
    {
        public string AgendaItemId { get; set; }
    }
    public class AddMinuteGET: AddMinuteDTO
    {
        public BaseAgendaItemGET AgendaItem { get; set; }
    }

    //upload minute
    public class UploadMinuteDTO
    {
        public string Id { get; set; }
        public string MeetingId { get; set; }
        public AttachmentDTO Attachment { get; set; }
    }
    public class UploadMinutePOST : UploadMinuteDTO
    {
        public string AgendaItemId { get; set; }
    }
    public class UploadMinuteGET : UploadMinuteDTO
    {
        public BaseAgendaItemGET AgendaItem { get; set; }
    }

}
