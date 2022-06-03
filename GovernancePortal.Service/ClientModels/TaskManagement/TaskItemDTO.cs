using GovernancePortal.Core.General;
using GovernancePortal.Service.ClientModels.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Service.ClientModels.TaskManagement
{
    public class TaskItemDTO
    {
        public TaskItemDTO()
        {
            //Attachments = new List<AttachmentDTO>();
        }
        public string Id { get; set; }
        public string Title { get; set; }
        public bool DocumentUpload { get; set; }
        public bool IsActive { get; set; }
        public TaskItemStatus Status { get; set; }
        //public List<AttachmentDTO> Attachments { get; set; }
    }

    public class TaskItemPOST : TaskItemDTO
    {

    }

    public class TaskItemGET : TaskItemDTO
    {

    }
}
