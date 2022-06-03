using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Service.ClientModels.General
{
    public class Response
    {
        public string StatusCode { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
}
