using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Core.General
{
    public class Person
    {
		public Person()
		{
			Id = Guid.NewGuid().ToString();
		}
		public string Id { get; set; }
		public string Name { get; set; }
		public string CompanyId { get; set; }
	}
}
