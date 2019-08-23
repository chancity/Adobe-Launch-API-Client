using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AdobeReactorApi;
using Backend.Models;
using Microsoft.Extensions.Logging;

namespace Backend.Services
{
    public class ReactorService
    {
        private const string removeFromPath = "/adobe_launch/proxy";
        private readonly ILogger _logger;
        private readonly ReactorApi _reactorApi;


        public ReactorService(ReactorApi reactorApi, ILoggerFactory loggerFactory)
        {
            _reactorApi = reactorApi;
            _logger = loggerFactory.CreateLogger<ReactorService>();
        }

        public async Task<string> CatchAll(RequestData requestData)
        {
            var requestEndpoint = requestData.OriginUrl.PathAndQuery.Replace(removeFromPath, "");
            _logger.LogDebug($"requestEndpoint: {requestEndpoint}");

            using (var requestMessage = new HttpRequestMessage(new HttpMethod(requestData.Method), requestEndpoint))
            {
                if (requestData.HasFormContentType)
                {
                    requestMessage.Content = new FormUrlEncodedContent(requestData.Form);
                }
                else
                {
                    Stream stream = new MemoryStream();
                    await requestData.Body.CopyToAsync(stream).ConfigureAwait(false);
                    requestMessage.Content = new StreamContent(stream);
                }

                using (var responseMessage =
                    await _reactorApi.HttpClient.SendAsync(requestMessage).ConfigureAwait(false))
                {
                    var ret = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var protocol = requestData.IsHttps ? "https" : "http";
                    ret = ret.Replace($"{Constants.ReactorUrl}/",
                        $"{protocol}://{requestData.OriginalRequest.Host}{removeFromPath}/");
                    return ret;
                }
            }
        }
    }
}