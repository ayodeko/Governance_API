using GovernancePortal.Core.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Core.TaskManagement
{
    public class TaskPerson : Person
    {
        public TaskPerson()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string TaskId { get; set; }
    }
}
