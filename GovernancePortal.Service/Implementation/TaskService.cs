﻿using GovernancePortal.Core.General;
using GovernancePortal.Core.TaskManagement;
using GovernancePortal.Data;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.Meetings;
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

        public TaskService(IHttpContextAccessor context, ITaskMaps tasksMaps, IUnitOfWork unit, ILogger logger)
        {
            _taskMaps = tasksMaps;
            _unit = unit;
            _context = context;
            _logger = logger;
        }

        Person GetLoggedInUser()
        {
            var companyId = _context.HttpContext?.Request.Headers["CompanyId"].FirstOrDefault();
            return new Person()
            {
                Id = "18312549-7133-41cb-8fd2-e76e1d088bb6",
                Name = "User1",
                CompanyId = companyId ?? "CompanyId",
                UserType = UserType.StandaloneUser
            };
        }

        public async Task<Pagination<TaskListGET>> GetTaskList(PageQuery pageQuery)
        {
            var person = GetLoggedInUser();
            _logger.LogInformation($"Inside get tasks");

            var retrievedTasks =  _unit.Tasks.GetTaskList(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
            if (retrievedTasks == null || !retrievedTasks.Any())
                throw new NotFoundException($"No tasks found in company with Id: {person.CompanyId}");
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
        public async Task<Pagination<TaskListGET>> GetNotStartedTasks(PageQuery pageQuery)
        {
            var person = GetLoggedInUser();
            _logger.LogInformation($"Inside get not started tasks");

            var retrievedTasks = _unit.Tasks.GetNotStartedTasks(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
            if (retrievedTasks == null || !retrievedTasks.Any())
                throw new NotFoundException($"No tasks found in company with Id: {person.CompanyId}");
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
            _logger.LogInformation("get not started tasks successful: {response}", response);

            return response;
        }
        public async Task<Pagination<TaskListGET>> GetOngoingTasks(PageQuery pageQuery)
        {
            var person = GetLoggedInUser();
            _logger.LogInformation($"Inside get ongoing tasks");
            var retrievedTasks = _unit.Tasks.GetOngoingTasks(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
            if (retrievedTasks == null || !retrievedTasks.Any())
                throw new NotFoundException($"No tasks found in company with Id: {person.CompanyId}");
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
            _logger.LogInformation("get user ongoing successful: {response}", response);

            return response;
        }
        public async Task<Pagination<TaskListGET>> GetCompletedTasks(PageQuery pageQuery)
        {
            var person = GetLoggedInUser();
            _logger.LogInformation($"Inside get completed tasks");
            var retrievedTasks = _unit.Tasks.GetCompletedTasks(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
            if (retrievedTasks == null || !retrievedTasks.Any())
                throw new NotFoundException($"No tasks found in company with Id: {person.CompanyId}");
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
            _logger.LogInformation("get user completed successful: {response}", response);

            return response;
        }
        public async Task<Pagination<TaskListGET>> GetDueTasks(PageQuery pageQuery)
        {
            var person = GetLoggedInUser();
            _logger.LogInformation($"Inside get due tasks");
            var retrievedTasks = _unit.Tasks.GetDueTasks(person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
            if (retrievedTasks == null || !retrievedTasks.Any())
                throw new NotFoundException($"No tasks found in company with Id: {person.CompanyId}");
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
            _logger.LogInformation("get due tasks successful: {response}", response);

            return response;
        }
        public async Task<Pagination<TaskListGET>> GetUserTasks(PageQuery pageQuery)
        {
            var person = GetLoggedInUser();
            _logger.LogInformation($"Inside get user tasks");
            var retrievedTasks = _unit.Tasks.GetTaskListByUserId(person.Id, person.CompanyId, pageQuery.PageNumber, pageQuery.PageSize, out var totalRecords);
            if (retrievedTasks == null || !retrievedTasks.Any())
                throw new NotFoundException($"No tasks found in company with Id: {person.CompanyId}");
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

        public async Task<Response> CreateTask(TaskPOST task)
        {
            var person = GetLoggedInUser();
            var newTask = _taskMaps.InMap(person.CompanyId, task);
            newTask.Status = Core.General.TaskStatus.NotStarted;
            await _unit.Tasks.Add(newTask, person);
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
            _logger.LogInformation($"Inside get tasks for taskId: {taskId}");
            var existingMeeting = await _unit.Tasks.GetTaskData(taskId, loggedInUser.CompanyId);
            if (existingMeeting is null || existingMeeting.IsDeleted) throw new NotFoundException($"Task with ID: {taskId} not found");
            throw new NotFoundException($"Task with ID: {taskId} not found");
            var outMeeting = "";
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

        Task<Pagination<TaskGET>> ITaskService.GetTaskData(string taskId)
        {
            throw new NotFoundException($"Task with ID: {taskId} not found");

        }

        public Task<Response> UpdateTask(TaskPOST task, string taskId)
        {
            throw new NotFoundException($"Task with ID: {taskId} not found");

        }

        public Task<Response> CompleteTaskItem(CompleteTaskDTO task, string taskId)
        {
            throw new NotFoundException($"Task with ID: {taskId} not found");

        }

        public Task<Response> AddTaskItemDocument(AddDocumentToTaskItemDTO task, string taskId)
        {
            throw new NotFoundException($"Task with ID: {taskId} not found");

        }
    }
}
