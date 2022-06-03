using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Service.ClientModels.TaskManagement
{
    public class TaskPersonDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class TaskPersonPOST : TaskPersonDTO
    {

    }

    public class TaskPersonGET : TaskPersonDTO
    {

    }
}
