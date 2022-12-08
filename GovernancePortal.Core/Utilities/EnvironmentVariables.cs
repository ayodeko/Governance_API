using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Core.Utilities
{
    public static class EnvironmentVariables 
    {
        public static string ConnectionString { get; } = Environment.GetEnvironmentVariable("ConnectionString");

    }
}
