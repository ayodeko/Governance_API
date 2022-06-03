using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.ClientModels.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovernancePortal.WebAPI.Helpers
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
}
