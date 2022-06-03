using GovernancePortal.Core.General;
using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Data;
using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace GovernancePortal.Service.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly ITaskMaps _taskMaps;
        private readonly IUnitOfWork _unit;
        public TaskService(ITaskMaps tasksMaps, IUnitOfWork unit)
        {
            _taskMaps = tasksMaps;
            _unit = unit;
        }

        public async Task<TaskModel> CreateTask(Person user, TaskPOST task)
        {
            var newTask = _taskMaps.InMap(user.CompanyId, task);
            newTask.Status = Core.General.TaskStatus.NotStarted;
            await _unit.Tasks.Add(newTask, user);
            _unit.SaveToDB();
            return newTask;
        }
    }
}
