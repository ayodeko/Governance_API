using GovernancePortal.Core.General;
using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Service.ClientModels.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Service.ClientModels.TaskManagement
{
    public abstract class TaskDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? TimeDue { get; set; }
        public bool isMeetingRelated { get; set; }
        public string meetingId { get; set; }

    }



    public class TaskPOST : TaskDTO
    {
        public TaskPOST()
        {
            Items = new List<TaskItemPOST>();
            Participants = new List<TaskPersonPOST>();
        }
        public List<TaskItemPOST> Items { get; set; }
        public List<TaskPersonPOST> Participants { get; set; }
    }

    public class TaskGET : TaskDTO
    {
        public TaskGET()
        {
            Items = new List<TaskItemGET>();
            Participants = new List<TaskPersonGET>();
        }
        public List<TaskItemGET> Items { get; set; }
        public List<TaskPersonGET> Participants { get; set; }
        public Core.General.TaskStatus Status { get; set; }

    }
    public class CompleteTaskDTO
    {
        public string TaskId { get; set; }
        public string TaskItemId { get; set; }
        public string CompletedBy { get; set; }
    }
    public class AddDocumentToTaskItemDTO
    {
        public string TaskId { get; set; }
        public string TaskItemId { get; set; }
        public List<AttachmentPostDTO> Documents { get; set; }
    }
}