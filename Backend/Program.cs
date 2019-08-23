using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Backend
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigureDelegate)
                .ConfigureLogging(ConfigureLogging)
                .UseStartup<Startup>();
        }

        private static void ConfigureDelegate(IConfigurationBuilder builder)
        {
            builder.AddInMemoryCollection(Defaults.Configuration).AddEnvironmentVariables();
        }

        private static void ConfigureLogging(ILoggingBuilder logBuilder)
        {
            logBuilder.ClearProviders();
            logBuilder.AddConsole();
            logBuilder.SetMinimumLevel(LogLevel.None);
        }
    }
}