using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Core.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;
using GovernancePortal.Core.General;
using System.Threading.Tasks;

namespace GovernancePortal.Service.Interface
{
    public interface ITaskService
    {
        Task<TaskModel> CreateTask(Person user, TaskPOST task);
    }
}
