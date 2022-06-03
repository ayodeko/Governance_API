using System;
using System.Collections.Generic;
using System.Text;

namespace GovernancePortal.Service.ClientModels.General
{
    public class ErrorModel
    {
        public string Key { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
