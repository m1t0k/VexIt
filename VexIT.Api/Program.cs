using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using VexIT.Api.StartUp;

namespace VexIT.Api
{
    public class Program
    {   /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var env = builderContext.HostingEnvironment;

                    config.AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.{Environment.MachineName}.json",
                            true, true);

                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(config.Build())
                        .CreateLogger();

                    Log.Logger.Information("App is starting.");
                })
                .UseStartup<Startup>()
                .UseSerilog() 
                .Build()
                .Migrate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            try
            {
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
