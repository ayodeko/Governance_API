using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Core.General
{
    public class Company
    {
        public Company()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string DomainMail { get; set; }
    }
}
