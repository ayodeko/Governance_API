using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Core.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;
using GovernancePortal.Core.General;
using System.Threading.Tasks;
using GovernancePortal.Service.ClientModels.General;

namespace GovernancePortal.Service.Interface
{
    public interface ITaskService
    {
        Task<Pagination<TaskListGET>> GetTaskList(PageQuery pageQuery);
        Task<Pagination<TaskListGET>> GetNotStartedTasks(PageQuery pageQuery);
        Task<Pagination<TaskListGET>> GetOngoingTasks(PageQuery pageQuery);
        Task<Pagination<TaskListGET>> GetCompletedTasks(PageQuery pageQuery);
        Task<Pagination<TaskListGET>> GetDueTasks(PageQuery pageQuery);
        Task<Pagination<TaskListGET>> GetUserTasks(PageQuery pageQuery);
        Task<Response> GetTaskData(string taskId);
        Task<Response> CreateTask( TaskPOST task);
        Task<Response> UpdateTask(TaskPOST task, string taskId);

        
        Task<Response> CompleteTaskItem(CompleteTaskDTO task, string taskId);
        
        
        Task<Response> AddTaskItemDocument(AddDocumentToTaskItemDTO task, string taskId);





    }
}
