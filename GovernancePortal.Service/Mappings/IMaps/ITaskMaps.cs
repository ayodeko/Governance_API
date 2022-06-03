using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Service.ClientModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Service.Mappings.IMaps
{
    public interface ITaskMaps
    {
        TaskModel InMap(string companyId, TaskPOST item, TaskModel existingTask = null);
    }
}
