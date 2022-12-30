using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Service.ClientModels.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GovernancePortal.Core.General;

namespace GovernancePortal.Service.Mappings.IMaps
{
    public interface ITaskMaps
    {
        TaskModel InMap(UserModel user, TaskPOST item, Core.TaskManagement.TaskModel existingTask = null);
        TaskItem InMap( AddDocumentToTaskItemDTO source, TaskItem destination);
        List<TaskListGET> OutMap(List<TaskModel> source, List<TaskListGET> destination);
        TaskGET OutMap(TaskModel source, TaskGET destination);

    }
}
