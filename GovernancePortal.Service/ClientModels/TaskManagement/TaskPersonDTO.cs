using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Service.ClientModels.TaskManagement
{
    public class TaskPersonDTO
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string ImageId { get; set; }
    }

    public class TaskPersonPOST : TaskPersonDTO
    {

    }

    public class TaskPersonGET : TaskPersonDTO
    {

    }
}
