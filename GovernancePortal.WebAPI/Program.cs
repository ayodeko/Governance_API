using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GovernancePortal.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                         .ConfigureWebHostDefaults(webBuilder =>
                         {
                             webBuilder.UseStartup<Startup>();
                         })
              .ConfigureLogging(logging =>
              {
                  logging.ClearProviders();
                  logging.AddDebug();
                  logging.AddConsole();
                  logging.SetMinimumLevel(LogLevel.Information);
              });
    }
}
