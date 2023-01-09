using Amazon.Auth.AccessControlPolicy;
using GovernancePortal.Core.General;
using GovernancePortal.Core.Meetings;
using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Data;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Service.Interface;
using GovernancePortal.Service.Mappings.IMaps;
using GovernancePortal.Service.Mappings.Maps;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = GovernancePortal.Core.General.TaskStatus;



namespace GovernancePortal.Service.Implementation
{
    public class TaskService : ITaskService
    {
        private IHttpContextAccessor _context;
        private readonly ITaskMaps _taskMaps;
        private readonly IUnitOfWork _unit;
        private ILogger _logger;
        private readonly IUtilityService _utilityService;

        public TaskService(IHttpContextAccessor context, ITaskMaps tasksMaps, IUnitOfWork unit, ILogger logger, IUtilityService utilityService)
        {
            _taskMaps = tasksMaps;
            _unit = unit;
            _context = context;
            _logger = logger;
            _utilityService = utilityService;
        }

        UserModel GetLoggedInUser()
        {
            var user = _utilityService.GetUser();
            return user;
        }

        public async Task<Pagination<TaskListGET>> GetTaskList(int? status, string userId, string searchString, PageQuery pageQuery)
        {
            var user = GetLoggedInUser();
            _logger.LogInformation($"Inside get tasks");


            //filter by task status, userId, search string
            var taskStatus = status != null
                ? (Enum.IsDefined(typeof(TaskStatus), status)
                    ? (TaskStatus)status : throw new Exception("Wrong status passed as query parameter"))
                : TaskStatus.NotStarted;
            var retrievedTasks = _unit.Tasks.GetTaskList(user.CompanyId, status == null ? null: taskStatus, userId, searchString, pageQuery.PageNumber,
            pageQuery.PageSize, out var totalRecords);
           

            if (retrievedTasks == null || !retrievedTasks.Any()) retrievedTasks = null;
            var taskList = _taskMaps.OutMap(retrievedTasks, new List<TaskListGET>());
            var response = new Pagination<TaskListGET>
            {
                Data = taskList,
                PageNumber = pageQuery.PageNumber,
                PageSize = pageQuery.PageSize,
                TotalRecords = totalRecords,
                Message = "Retrieved successfully",
                IsSuccessful = true,
                StatusCode = HttpStatusCode.OK.ToString()
            };
            _logger.LogInformation("get tasks successful: {response}", response);

            return response;
        }
        public async Task<Pagination<TaskListGET>> GetTaskListBySearch(int? status, string userId, string searchString, PageQuery pageQuery)
        {
            var person = GetLoggedInUser();
            _logger.LogInformation($"Inside search tasks");

            var taskStatus = status != null
            ? (Enum.IsDefined(typeof(TaskStatus), status)
                ? (TaskStatus)status : throw new Exception("Wrong status passed as query parameter"))
            : TaskStatus.NotStarted;

            var retrievedTasks = _unit.Tasks.GetTaskListBySearch(person.CompanyId, status == null ? null : taskStatus, userId, searchString, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
            if (retrievedTasks == null || !retrievedTasks.Any()) retrievedTasks = null;
            var taskList = _taskMaps.OutMap(retrievedTasks, new List<TaskListGET>());
            var response = new Pagination<TaskListGET>
            {
                Data = taskList,
                PageNumber = pageQuery.PageNumber,
                PageSize = pageQuery.PageSize,
                TotalRecords = totalRecords,
                Message = "Retrieved successfully",
                IsSuccessful = true,
                StatusCode = HttpStatusCode.OK.ToString()
            };
            _logger.LogInformation("search tasks successful: {response}", response);

            return response;
        }
  
        public async Task<Pagination<TaskListGET>> GetUserTasks(PageQuery pageQuery)
        {
            var person = GetLoggedInUser();
            _logger.LogInformation($"Inside get user tasks");
            var retrievedTasks = _unit.Tasks.GetTaskListByUserId(person.Id, person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
            if (retrievedTasks == null || !retrievedTasks.Any()) retrievedTasks = null;
            var taskList = _taskMaps.OutMap(retrievedTasks, new List<TaskListGET>());
            var response = new Pagination<TaskListGET>
            {
                Data = taskList,
                PageNumber = pageQuery.PageNumber,
                PageSize = pageQuery.PageSize,
                TotalRecords = totalRecords,
                Message = "Retrieved successfully",
                IsSuccessful = true,
                StatusCode = HttpStatusCode.OK.ToString()
            };
                _logger.LogInformation("get user tasks successful: {response}", response);

            return response;
        }

        public async Task<Pagination<TaskListGET>> GetUserTasks(int? status, PageQuery pageQuery)
        {
            var user = GetLoggedInUser();
            _logger.LogInformation($"Inside get user tasks list, task status: {status}");
            //filter by task status
            var taskStatus = status != null
                ? (Enum.IsDefined(typeof(TaskStatus), status)
                    ? (TaskStatus)status : throw new Exception("Wrong status passed as query parameter"))
                : TaskStatus.NotStarted;
            var retrievedTasks = (taskStatus == null)
            ? _unit.Tasks.GetTaskListByUserId(user.Id, user.CompanyId, pageQuery.PageNumber,
            pageQuery.PageSize, out var totalRecords)
            : _unit.Tasks.GetUserTaskListByStatus(taskStatus,user.Id, user.CompanyId,
            pageQuery.PageNumber, pageQuery.PageSize, out totalRecords);

            if (retrievedTasks == null || !retrievedTasks.Any()) retrievedTasks = null;
            var taskList = _taskMaps.OutMap(retrievedTasks, new List<TaskListGET>());
            var response = new Pagination<TaskListGET>
            {
                Data = taskList,
                PageNumber = pageQuery.PageNumber,
                PageSize = pageQuery.PageSize,
                TotalRecords = totalRecords,
                Message = "Retrieved successfully",
                IsSuccessful = true,
                StatusCode = HttpStatusCode.OK.ToString()
            };
            _logger.LogInformation("get user tasks by status successful: {response}", response);

            return response;
        }

        public async Task<Response> CreateTask(TaskPOST task)
        {
            var user = GetLoggedInUser();
            var newTask = _taskMaps.InMap(user, task);
            newTask.Status = Core.General.TaskStatus.NotStarted;

            await _unit.Tasks.Add(newTask, user);
            _unit.SaveToDB();
            var response = new Response()
            {
                Data = newTask,
                Exception = null,
                Message = "task successfully created",
                IsSuccessful = true,
                StatusCode = HttpStatusCode.Created.ToString()
            };
            _logger.LogInformation("create task successful: {response}", response);
            return response;
        }
        public async Task<Response> GetTaskData(string taskId)
        {
            var loggedInUser = GetLoggedInUser();
            _logger.LogInformation($"Inside get task details for taskId: {taskId}");
            var existingTask = await _unit.Tasks.GetTaskData(taskId, loggedInUser.CompanyId);
            if (existingTask is null || existingTask.IsDeleted) throw new NotFoundException($"Task with ID: {taskId} not found");
            var outMeeting = _taskMaps.OutMap(existingTask, new TaskGET());
            var response = new Response
            {
                Data = outMeeting,
                Message = "Task updated successfully",
                StatusCode = HttpStatusCode.OK.ToString(),
                IsSuccessful = true
            };
            _logger.LogInformation("Get task data successful: {response}", response);
            return response;
        }

        

        public async Task<Response> UpdateTask(TaskPOST task, string taskId)
        {
            var loggedInUser = GetLoggedInUser();
            var existingTask = await _unit.Tasks.GetTaskData(taskId, loggedInUser.CompanyId);
            if (existingTask is null || existingTask.IsDeleted) throw new NotFoundException($"Task with ID: {taskId} not found");
            existingTask = _taskMaps.InMap(loggedInUser, task, existingTask);
            _unit.SaveToDB();

            //update relational Models (taskitems)
            foreach (var item in task.Items)
            {
                var taskItem = existingTask.Items.Where(x => x.Id == item.Id).FirstOrDefault();
                if (taskItem is not null)
                {
                    var existingTaskItem = await _unit.Tasks.GetTaskItemData(taskItem.Id, taskId);
                    existingTaskItem = _taskMaps.InMap(existingTask, item, existingTaskItem);
                    _unit.SaveToDB();
                }
            }

            var response = new Response()
            {
                Data = existingTask,
                Exception = null,
                Message = "task successfully created",
                IsSuccessful = true,
                StatusCode = HttpStatusCode.Created.ToString()
            };
            _logger.LogInformation("update task successful: {response}", response);
            return response;

        }
   
        public async Task<Response> CompleteTaskItem(CompleteTaskDTO input, string taskId)
        {
            int completedtskCount = 0;
            var loggedInUser = GetLoggedInUser();      
            var existingTask = await _unit.Tasks.GetTaskData(taskId, loggedInUser.CompanyId);
            if (existingTask is null || existingTask.IsDeleted) throw new NotFoundException($"Task with ID: {taskId} not found");
            var taskItem = existingTask.Items.FirstOrDefault(x => x.Id == input.TaskItemId);
            if (taskItem is null) throw new NotFoundException($"Task item with ID: {input.TaskItemId} not found");
            if (taskItem.Status == TaskItemStatus.Completed) throw new BadRequestException("This task item has been completed");
            taskItem.Status = TaskItemStatus.Completed;
            foreach(var item in existingTask.Items)
            {
                if(item.Status == TaskItemStatus.Completed) completedtskCount++;
            }
            //check if all ask items are completed
            if (completedtskCount >= existingTask.Items.Count) existingTask.Status = TaskStatus.Completed;
            _unit.SaveToDB();
            var response = new Response()
            {
                Data = existingTask,
                Exception = null,
                Message = "task successfully created",
                IsSuccessful = true,
                StatusCode = HttpStatusCode.Created.ToString()
            };
            _logger.LogInformation("complete task item successful: {response}", response);
            return response;

        }
        public async Task<Response> AddTaskItemDocument(AddDocumentToTaskItemDTO input, string taskId)
        {
            var loggedInUser = GetLoggedInUser();
            var existingTask = await _unit.Tasks.GetTaskData(taskId, loggedInUser.CompanyId);
            if (existingTask is null || existingTask.IsDeleted) throw new NotFoundException($"Task with ID: {taskId} not found");
            var taskItem = existingTask.Items.FirstOrDefault(x => x.Id == input.TaskItemId);
            if (taskItem is null) throw new NotFoundException($"Task item with ID: {input.TaskItemId} not found");
            taskItem = _taskMaps.InMap( input, taskItem);
            existingTask.Status = existingTask.Status == TaskStatus.Completed ? TaskStatus.Completed: TaskStatus.Ongoing;
            
            _unit.SaveToDB();
            var response = new Response()
            {
                Message = "task item document successfully added",
                IsSuccessful = true,
                StatusCode = HttpStatusCode.Created.ToString()
            };
            _logger.LogInformation("add task item successful: {response}", response);
            return response;
        }


        //public async Task<Pagination<TaskListGET>> GetNotStartedTasks(PageQuery pageQuery)
        //{
        //    var person = GetLoggedInUser();
        //    _logger.LogInformation($"Inside get not started tasks");

        //    var retrievedTasks = _unit.Tasks.GetNotStartedTasks(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
        //    if (retrievedTasks == null || !retrievedTasks.Any()) retrievedTasks = null;
        //    var taskList = _taskMaps.OutMap(retrievedTasks, new List<TaskListGET>());
        //    var response = new Pagination<TaskListGET>
        //    {
        //        Data = taskList,
        //        PageNumber = pageQuery.PageNumber,
        //        PageSize = pageQuery.PageSize,
        //        TotalRecords = totalRecords,
        //        Message = "Retrieved successfully",
        //        IsSuccessful = true,
        //        StatusCode = HttpStatusCode.OK.ToString()
        //    };
        //    _logger.LogInformation("get not started tasks successful: {response}", response);

        //    return response;
        //}
        //public async Task<Pagination<TaskListGET>> GetOngoingTasks(PageQuery pageQuery)
        //{
        //    var person = GetLoggedInUser();
        //    _logger.LogInformation($"Inside get ongoing tasks");
        //    var retrievedTasks = _unit.Tasks.GetOngoingTasks(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
        //    if (retrievedTasks == null || !retrievedTasks.Any()) retrievedTasks = null;
        //    var taskList = _taskMaps.OutMap(retrievedTasks, new List<TaskListGET>());
        //    var response = new Pagination<TaskListGET>
        //    {
        //        Data = taskList,
        //        PageNumber = pageQuery.PageNumber,
        //        PageSize = pageQuery.PageSize,
        //        TotalRecords = totalRecords,
        //        Message = "Retrieved successfully",
        //        IsSuccessful = true,
        //        StatusCode = HttpStatusCode.OK.ToString()
        //    };
        //    _logger.LogInformation("get user ongoing successful: {response}", response);

        //    return response;
        //}
        //public async Task<Pagination<TaskListGET>> GetCompletedTasks(PageQuery pageQuery)
        //{
        //    var person = GetLoggedInUser();
        //    _logger.LogInformation($"Inside get completed tasks");
        //    var retrievedTasks = _unit.Tasks.GetCompletedTasks(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
        //    if (retrievedTasks == null || !retrievedTasks.Any()) retrievedTasks = null;
        //    var taskList = _taskMaps.OutMap(retrievedTasks, new List<TaskListGET>());
        //    var response = new Pagination<TaskListGET>
        //    {
        //        Data = taskList,
        //        PageNumber = pageQuery.PageNumber,
        //        PageSize = pageQuery.PageSize,
        //        TotalRecords = totalRecords,
        //        Message = "Retrieved successfully",
        //        IsSuccessful = true,
        //        StatusCode = HttpStatusCode.OK.ToString()
        //    };
        //    _logger.LogInformation("get user completed successful: {response}", response);

        //    return response;
        //}
        //public async Task<Pagination<TaskListGET>> GetDueTasks(PageQuery pageQuery)
        //{
        //    var person = GetLoggedInUser();
        //    _logger.LogInformation($"Inside get due tasks");
        //    var retrievedTasks = _unit.Tasks.GetDueTasks(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
        //    if (retrievedTasks == null || !retrievedTasks.Any()) retrievedTasks = null;
        //    var taskList = _taskMaps.OutMap(retrievedTasks, new List<TaskListGET>());
        //    var response = new Pagination<TaskListGET>
        //    {
        //        Data = taskList,
        //        PageNumber = pageQuery.PageNumber,
        //        PageSize = pageQuery.PageSize,
        //        TotalRecords = totalRecords,
        //        Message = "Retrieved successfully",
        //        IsSuccessful = true,
        //        StatusCode = HttpStatusCode.OK.ToString()
        //    };
        //    _logger.LogInformation("get due tasks successful: {response}", response);

        //    return response;
        //}
    }
}
