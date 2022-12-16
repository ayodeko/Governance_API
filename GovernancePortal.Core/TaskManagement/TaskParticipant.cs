using GovernancePortal.Core.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Core.TaskManagement
{
    public class TaskParticipant
    {
        public TaskParticipant()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string ImageId { get; set; }
        public string CompanyId { get; set; }
        public string TaskId { get; set; }
    }
}
