using GovernancePortal.Core.Meetings;
using GovernancePortal.Service.ClientModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GovernancePortal.Core.General;

namespace GovernancePortal.Service.ClientModels.Meetings.Minute
{
    public class MinuteDTO
    {
        public string Id { get; set; }
        public string MinuteText { get; set; }
        public string AgendaItemId { get; set; }
    }
    //add minute
    public class AddMinuteDTO
    {
        public string Id { get; set; }
        public string MinuteText { get; set; }
        public string AgendaItemId { get; set; }

    }

    public class AddMinutePOST
    {
        public List<AddMinuteDTO> items { get; set; }
    }
    public class AddMinuteGET: AddMinuteDTO
    {
        public BaseAgendaItemGET AgendaItem { get; set; }
    }

    //upload minute
    public class UploadMinuteDTO
    {
        public string Id { get; set; }
        public AttachmentPostDTO Attachment { get; set; }
    }
    public class UploadMinutePOST : UploadMinuteDTO
    {
    }
    public class UploadMinuteGET : UploadMinuteDTO
    {
        public BaseAgendaItemGET AgendaItem { get; set; }
    }

    public class MinuteGET
    {
        public string Id { get; set; }
        public string MinuteText { get; set; }
        public string AgendaItemId { get; set; }
        public FullAgendaItemPOST AgendaItem { get; set; }
        public Attachment Attachment { get; set; }

    }

}
