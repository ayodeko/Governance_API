using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GovernancePortal.Web_API.Utility
{
    public interface IExceptionHandler
    {
        ActionResult GetResponse(Exception ex);
    }
    internal class ExceptionHandler : ControllerBase, IExceptionHandler
    {
        public ExceptionHandler()
        {

        }

        public ActionResult GetResponse(Exception ex)
        {
            if (ex is BadRequestException)
            {
                var exception = (BadRequestException)ex;
                return StatusCode(400, ex.Message);
            }
            else if (ex is NotFoundException)
            {
                var exception = (NotFoundException)ex;
                return StatusCode(404, ex.Message);
            }
            else if (ex is ValidationException)
            {
                var exception = (ValidationException)ex;
                return StatusCode(422, exception.Errors);
            }
            else if(ex is FailureException)
            {
                var exception = (FailureException)ex;
                return StatusCode(200, new Response { IsSuccessful = false, StatusCode = "122", Data = exception.ExData, Message = ex.Message });
            }

            //log the error in a customized way here

            return StatusCode(500, ex);
        }
        
    }

    public static class GovernancePortalExceptionHandler
    {
        public static IApplicationBuilder UseGovernancePortalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    // using static System.Net.Mime.MediaTypeNames;
                    context.Response.ContentType = "application/json";
                    var exceptionHandlerFeature =
                        context.Features.Get<IExceptionHandlerFeature>()!;
                    var response = new Response()
                    {
                        IsSuccessful = false,
                        Message = "",
                        StatusCode = HttpStatusCode.InternalServerError.ToString(),
                        Exception = JsonConvert.SerializeObject(exceptionHandlerFeature.Error)
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                });
            });
            return app;
        }
    }
}
