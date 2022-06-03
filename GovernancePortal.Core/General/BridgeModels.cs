using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Core.General
{
    public class Task_Person
    {
        public Task_Person()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string CompanyId { get; set; }
        public string PersonId { get; set; }
        public string TaskId { get; set; }
    }
}
