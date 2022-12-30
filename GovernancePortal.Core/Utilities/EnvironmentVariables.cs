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
        public static string BODADMINAuthService { get; } = Environment.GetEnvironmentVariable("BODADMINAuthService");
        public static string BODADMINEmailService { get; } = Environment.GetEnvironmentVariable("BODADMINEmailService");
        public static string BODADMINSecretKey { get; } = Environment.GetEnvironmentVariable("BODADMINSecretKey");
        public static string BODADMINAudience { get; } = Environment.GetEnvironmentVariable("BODADMINAudience");
        public static string BODADMINIssuer { get; } = Environment.GetEnvironmentVariable("BODADMINIssuer");
        public static string DeleteActionTemplate { get; } = Environment.GetEnvironmentVariable("DeleteActionTemplate");
        public static string CreateActionTemplate { get; } = Environment.GetEnvironmentVariable("CreateActionTemplate");
        public static string UpdateActionTemplate { get; } = Environment.GetEnvironmentVariable("UpdateActionTemplate");
        public static string BODADMINFrontEndURL { get; } = Environment.GetEnvironmentVariable("BODADMINFrontEndURL");

    }
}
