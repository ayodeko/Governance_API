using GovernancePortal.Service.ClientModels.General;
using GovernancePortal.Service.ClientModels.TaskManagement;
using GovernancePortal.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GovernancePortal.Web_API.Endpoints
{
    public static class TaskMgtEndpoints
    {
        public static WebApplication MapTaskMgtEndpoints(this WebApplication app)
        {
            app.MapGet("api/TaskMgt/list",([FromServices] ITaskService taskServices, PageQuery pageQuery) 
                => taskServices.GetTaskList(pageQuery));
            app.MapGet("api/TaskMgt/NotStarted/List", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
                => taskServices.GetNotStartedTasks(pageQuery));
            app.MapGet("api/TaskMgt/Ongoing/List", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
                => taskServices.GetOngoingTasks(pageQuery));
            app.MapGet("api/TaskMgt/Completed/List", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
                => taskServices.GetCompletedTasks(pageQuery));
            app.MapGet("api/TaskMgt/Due/List", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
                => taskServices.GetDueTasks(pageQuery));
            app.MapGet("api/TaskMgt/UserTasks", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
               => taskServices.GetUserTasks(pageQuery));
            app.MapPost("api/TaskMgt/CreateTask",([FromServices] ITaskService taskServices, TaskPOST input) 
                =>taskServices.CreateTask(input));
            app.MapGet("api/TaskMgt/{taskId}/Update", ([FromServices] ITaskService taskServices, string taskId)
               => taskServices.GetTaskData(taskId));
            app.MapPost("api/TaskMgt/{taskId}/UpdateTask", ([FromServices] ITaskService taskServices, TaskPOST input, string taskId)
            => taskServices.UpdateTask(input, taskId));
            app.MapGet("api/TaskMgt/{taskId}", ([FromServices] ITaskService taskServices, string taskId)
              => taskServices.GetTaskData(taskId));
            app.MapPost("api/TaskMgt/{taskId}/Complete", ([FromServices] ITaskService taskServices, CompleteTaskDTO input, string taskId)
              => taskServices.CompleteTaskItem(input, taskId));
            app.MapPost("api/TaskMgt/{taskId}/AddDocument", ([FromServices] ITaskService taskServices, AddDocumentToTaskItemDTO input, string taskId)
             => taskServices.AddTaskItemDocument(input, taskId));
            return app;


        }
    }
}
