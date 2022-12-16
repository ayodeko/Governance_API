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
    public static class GovernancePortalExceptionHandler
    {
        public static IApplicationBuilder UseGovernancePortalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var exceptionHandlerFeature =
                        context.Features.Get<IExceptionHandlerFeature>()!;
                    context.Response.StatusCode = (int) GetHttpStatusCode(exceptionHandlerFeature.Error);

                    // using static System.Net.Mime.MediaTypeNames;
                    context.Response.ContentType = "application/json";
                    var response = new Response()
                    {
                        IsSuccessful = false,
                        Message = "",
                        StatusCode = GetHttpStatusCode(exceptionHandlerFeature.Error).ToString(),
                        Exception = new
                        {
                            exceptionHandlerFeature.Error.Source,
                            exceptionHandlerFeature.Error.Message,
                            InnerExceptionMessage = exceptionHandlerFeature.Error.InnerException?.Message,
                            exceptionHandlerFeature.Error.StackTrace,
                        }
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                });
            });
            return app;
        }
        
        public static HttpStatusCode GetHttpStatusCode(Exception ex)
        {
            return ex switch
            {
                BadRequestException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}
