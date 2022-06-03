using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Service.ClientModels.Exceptions
{
    public class ErrorModel
    {
        public string Key { get; set; }
        public string ErrorCode { get; set; }
        public string Message { get; set; }
    }

    public class FailureException :Exception
    {
        public dynamic ExData { get; set; }
        public FailureException(string message, dynamic data): base(message)
        {
            ExData = data;
                
        }
    }

    public class ReturnException : Exception
    {
        public ReturnException(string message) : base(message)
        {
        }

    }

    public class BadRequestException : Exception
    {

        public BadRequestException(string message) : base(message)
        {
        }
    }

    public class ValidationException : Exception
    {
        public List<ErrorModel> Errors { get; set; }
        public ValidationException(List<ErrorModel> errors)
        {
            Errors = errors;
        }

    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
