using GovernancePortal.Core.General;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskStatus = GovernancePortal.Core.General.TaskStatus;

namespace GovernancePortal.Core.TaskManagement
{
    public class TaskModel : BaseModel, ICompanyModel
	{
		public TaskModel()
		{
			Id = Guid.NewGuid().ToString();
			Items = new List<TaskItem>();
			Participants = new List<TaskParticipant>();
		}
		public string Id { get; set; }
		public string CompanyId { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime? DateModified { get; set; }
		public string MeetingId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public List<TaskItem> Items { get; set; }
		public List<TaskParticipant> Participants { get; set; }
		public DateTime? TimeDue { get; set; }
		public TaskStatus Status { get; set; }
	}
}
