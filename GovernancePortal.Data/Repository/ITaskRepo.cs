using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Core.TaskManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = GovernancePortal.Core.General.TaskStatus;

namespace GovernancePortal.Data.Repository
{
    public interface ITaskRepo : IGenericRepo<TaskModel>
    {
        List<TaskModel> GetTaskList(string companyId, TaskStatus? status, string userId, string searchString, int pageNumber, int pageSize, out int totalRecords);
        List<TaskModel> GetTaskListByStatus(TaskStatus status, string companyId, int pageNumber, int pageSize, out int totalRecords);

        List<TaskModel> GetTaskListBySearch(string companyId, TaskStatus? status, string userId, string searchString, int pageNumber, int pageSize, out int totalRecords);
        List<TaskModel> GetNotStartedTasks(string companyId, int pageNumber, int pageSize, out int totalRecords);
        List<TaskModel> GetOngoingTasks(string companyId, int pageNumber, int pageSize, out int totalRecords);
        List<TaskModel> GetCompletedTasks(string companyId, int pageNumber, int pageSize, out int totalRecords);
        List<TaskModel> GetTaskListByUserId(string personId, string companyId, int pageNumber, int pageSize, out int totalRecords);
        List<TaskModel> GetUserTaskListByStatus(TaskStatus status, string personId, string companyId, int pageNumber, int pageSize, out int totalRecords);

        Task<TaskModel> GetTaskData(string taskId, string companyId);
        List<TaskModel> GetDueTasks(string companyId, int pageNumber, int pageSize, out int totalRecords);
    }
}
