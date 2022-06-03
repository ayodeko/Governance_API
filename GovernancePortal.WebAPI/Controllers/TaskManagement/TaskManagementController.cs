using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GovernancePortal.Service.ClientModels.General;
using System;
using System.Threading.Tasks;
using GovernancePortal.Service.Interface;
using GovernancePortal.WebAPI.Helpers;
using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Core.General;

namespace GovernancePortal.WebAPI.Controllers.TaskManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {

        private readonly ITaskService _taskService;
        private readonly IExceptionHandler _exceptionHandler;
        public TaskManagementController(ITaskService taskService, IExceptionHandler exceptionHandler)
        {
            _taskService = taskService;
            _exceptionHandler = exceptionHandler;
        }

        [HttpPost("Task/Create")]
        public async Task<ActionResult<Response>> CreateTask(TaskPOST task)
        {
            try
            {
                Person loggedUser = GetLoggedUser();
                var newTask = await _taskService.CreateTask(loggedUser, task);
                task.Id = newTask.Id;
                return Ok(
                    new Response
                    {
                        Data = task,
                        Message = "Task created successfully",
                        StatusCode = "00",
                        IsSuccessful = true
                    });
            }
            catch (Exception ex)
            {
                return _exceptionHandler.GetResponse(ex);
            }
        }

        private Person GetLoggedUser()
        {
            return new Person { CompanyId = "CompanyId1", Id = "Company1UserAdmin1" };
        }
    }
}
