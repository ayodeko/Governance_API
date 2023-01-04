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
            //get calls
            app.MapGet("api/TaskMgt/GetTaskList",([FromServices] ITaskService taskServices, int? status, string? userId, string? searchString,PageQuery pageQuery) 
                => taskServices.GetTaskList(status, userId, searchString, pageQuery)).RequireAuthorization();
            app.MapGet("api/TaskMgt/SearchByTitle",([FromServices] ITaskService taskServices, int? status, string? userId, string searchString, PageQuery pageQuery) 
                => taskServices.GetTaskListBySearch(status, userId, searchString, pageQuery)).RequireAuthorization();
            app.MapGet("api/TaskMgt/{taskId}/Update", ([FromServices] ITaskService taskServices, string taskId)
                => taskServices.GetTaskData(taskId)).RequireAuthorization();
            app.MapGet("api/TaskMgt/{taskId}", ([FromServices] ITaskService taskServices, string taskId)
                => taskServices.GetTaskData(taskId)).RequireAuthorization();
        

            //post calls
            app.MapPost("api/TaskMgt/CreateTask", ([FromServices] ITaskService taskServices, TaskPOST input)
                => taskServices.CreateTask(input)).RequireAuthorization();
            app.MapPost("api/TaskMgt/{taskId}/UpdateTask", ([FromServices] ITaskService taskServices, TaskPOST input, string taskId)
                => taskServices.UpdateTask(input, taskId)).RequireAuthorization();
            app.MapPost("api/TaskMgt/{taskId}/Complete", ([FromServices] ITaskService taskServices, CompleteTaskDTO input, string taskId)
                => taskServices.CompleteTaskItem(input, taskId)).RequireAuthorization();
            app.MapPost("api/TaskMgt/{taskId}/AddDocument", ([FromServices] ITaskService taskServices, AddDocumentToTaskItemDTO input, string taskId)
                => taskServices.AddTaskItemDocument(input, taskId)).RequireAuthorization();


            return app;

            //app.MapGet("api/TaskMgt/UserTasks", ([FromServices] ITaskService taskServices, PageQuery pageQuery)
            //   => taskServices.GetUserTasks(pageQuery)).RequireAuthorization();
            //app.MapGet("api/TaskMgt/UserTasksByStatus", ([FromServices] ITaskService taskServices, int? status, PageQuery pageQuery)
            //  => taskServices.GetUserTasks(status, pageQuery)).RequireAuthorization();
        }
    }
}
