using GovernancePortal.Core.General;
using GovernancePortal.Core.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Service.ClientModels.TaskManagement
{
    public abstract class TaskDTO
    {
        public string Id { get; set; }
        public string MeetingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Person CreatedBy { get; set; }
        public DateTime? TimeDue { get; set; }
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
            Items = new List<TaskItemPOST>();
            Participants = new List<TaskPersonPOST>();
        }
        public List<TaskItemPOST> Items { get; set; }
        public List<TaskPersonPOST> Participants { get; set; }
        public Core.General.TaskStatus Status { get; set; }

    }
}