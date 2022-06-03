using GovernancePortal.Core.General;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GovernancePortal.Core.TaskManagement
{
    public class TaskModel : ICompanyModel
	{
		public TaskModel()
		{
			Id = Guid.NewGuid().ToString();
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
		public List<TaskPerson> Participants { get; set; }
		public DateTime? TimeDue { get; set; }
		public General.TaskStatus Status { get; set; }
	}
}
