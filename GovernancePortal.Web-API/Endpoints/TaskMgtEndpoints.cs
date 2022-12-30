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
            app.MapGet("api/TaskMgt/GetAllTasks",([FromServices] ITaskService taskServices, PageQuery pageQuery) 
                => taskServices.GetTaskList(pageQuery)).RequireAuthorization();
            app.MapGet("api/TaskMgt/SearchByTitle",([FromServices] ITaskService taskServices, string title, PageQuery pageQuery) 
                => taskServices.GetTaskListBySearch(title, pageQuery)).RequireAuthorization();

            app.MapGet("api/TaskMgt/GetTaskListByStatus", ([FromServices] ITaskService taskServices, int? status, PageQuery pageQuery)
                => taskServices.GetTasks(status, pageQuery)).RequireAuthorization();

            app.MapGet("api/TaskMgt/NotStarted/List", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
                => taskServices.GetNotStartedTasks(pageQuery)).RequireAuthorization();
            app.MapGet("api/TaskMgt/Ongoing/List", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
                => taskServices.GetOngoingTasks(pageQuery)).RequireAuthorization();
            app.MapGet("api/TaskMgt/Completed/List", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
                => taskServices.GetCompletedTasks(pageQuery)).RequireAuthorization();
            app.MapGet("api/TaskMgt/Due/List", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
                => taskServices.GetDueTasks(pageQuery)).RequireAuthorization();
            app.MapGet("api/TaskMgt/UserTasks", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
               => taskServices.GetUserTasks(pageQuery)).RequireAuthorization();

            app.MapGet("api/TaskMgt/UserTasksByStatus", ([FromServices] ITaskService taskServices, int? status, PageQuery pageQuery)
              => taskServices.GetUserTasks(status, pageQuery)).RequireAuthorization();

            app.MapPost("api/TaskMgt/CreateTask",([FromServices] ITaskService taskServices, TaskPOST input) 
                =>taskServices.CreateTask(input)).RequireAuthorization();
            app.MapGet("api/TaskMgt/{taskId}/Update", ([FromServices] ITaskService taskServices, string taskId)
               => taskServices.GetTaskData(taskId)).RequireAuthorization();
            app.MapPost("api/TaskMgt/{taskId}/UpdateTask", ([FromServices] ITaskService taskServices, TaskPOST input, string taskId)
            => taskServices.UpdateTask(input, taskId)).RequireAuthorization();
            app.MapGet("api/TaskMgt/{taskId}", ([FromServices] ITaskService taskServices, string taskId)
              => taskServices.GetTaskData(taskId)).RequireAuthorization();
            app.MapPost("api/TaskMgt/{taskId}/Complete", ([FromServices] ITaskService taskServices, CompleteTaskDTO input, string taskId)
              => taskServices.CompleteTaskItem(input, taskId)).RequireAuthorization();
            app.MapPost("api/TaskMgt/{taskId}/AddDocument", ([FromServices] ITaskService taskServices, AddDocumentToTaskItemDTO input, string taskId)
             => taskServices.AddTaskItemDocument(input, taskId)).RequireAuthorization();
            return app;


        }
    }
}
