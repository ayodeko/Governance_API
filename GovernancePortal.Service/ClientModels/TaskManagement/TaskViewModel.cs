using GovernancePortal.Core.General;
using GovernancePortal.Core.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Service.ClientModels.TaskManagement
{
    public class TaskListGET
    {
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public List<TaskItem> Items { get; set; }
		public List<TaskParticipant> Participants { get; set; }
		public DateTime? TimeDue { get; set; }
	}
}
