using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AdobeLaunch.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AdobeLaunch.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HostBuilder hostBuilder = new HostBuilder();

            IHost host = hostBuilder
                .ConfigureHostConfiguration(ConfigureDelegate)
                .ConfigureServices(ConfigureDelegate)
                .ConfigureLogging(ConfigureLogging)
                .Build();

            await host.StartAsync().ConfigureAwait(false);
        }

        private static void ConfigureDelegate(HostBuilderContext hostBuilder, IServiceCollection services)
        {
            var configuration = hostBuilder.Configuration;
            hostBuilder.HostingEnvironment.EnvironmentName = configuration["ASPNETCORE_ENVIRONMENT"];

            var accountOptions = new AccountOptions(
                configuration["ORGANIZATION_ID"],
                configuration["TECHNICAL_ACCOUNT_ID"],
                configuration["CLIENT_ID"],
                configuration["CLIENT_SECRET"],
                GetSecurityKey(configuration["CERTIFICATE_PATH"]));


            var reactorApi = new ReactorApi(accountOptions);

            services
                .AddSingleton(reactorApi)
                .AddHostedService<ReactorService>();
        }

        private static SecurityKey GetSecurityKey(string filePath)
        {
            var certificate = new X509Certificate2(X509Certificate.CreateFromSignedFile(filePath));
            var rsaPrivateKey = certificate.GetRSAPrivateKey();
            return new RsaSecurityKey(rsaPrivateKey);
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