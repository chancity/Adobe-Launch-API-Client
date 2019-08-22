using System.Threading.Tasks;
using AdobeLaunch.Client;
using Microsoft.Extensions.Logging;

namespace AdobeLaunch.ApiProxy.Services
{
    public class ReactorService
    {
        private readonly ILogger _logger;
        private readonly ReactorApi _reactorApi;

        public ReactorService(ReactorApi reactorApi, ILoggerFactory loggerFactory)
        {
            _reactorApi = reactorApi;
            _logger = loggerFactory.CreateLogger<ReactorService>();
        }

        public Task<string> CatchAll(string path)
        {
           return _reactorApi.HttpClient.GetStringAsync(path);
        }
    }
}