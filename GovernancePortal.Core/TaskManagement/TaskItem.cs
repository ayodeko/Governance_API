using GovernancePortal.Core.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Core.TaskManagement
{
    public class TaskItem
    {
		public TaskItem()
		{
			Id = Guid.NewGuid().ToString();
			Attachments = new List<TaskAttachment>();
			IsActive = true;
			DateCreated = DateTime.Now;
		}
		public string Id { get; set; }
		public string TaskId { get; set; }
		public string Title { get; set; }
		public bool DocumentUpload { get; set; }
		public bool IsActive { get; set; }
		public TaskItemStatus Status { get; set; }
		public List<TaskAttachment> Attachments { get; set; }
		public DateTime? DateCreated { get; set; }
	}
}
