using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AdobeLaunch.Client.HelperInterfaces;
using AdobeLaunch.Client.Models;

namespace AdobeLaunch.Client.HttpClientMiddleware
{
    internal class HttpAuthorizationHandler : DelegatingHandler
    {
        private readonly ITokenHandler<AccessToken> _accessTokenHandler;

        public HttpAuthorizationHandler(ITokenHandler<AccessToken> accessTokenHandler, HttpMessageHandler innerHandler)
        {
            InnerHandler = innerHandler;
            _accessTokenHandler = accessTokenHandler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _accessTokenHandler.GetToken().ConfigureAwait(false);
            request.Headers.Add("Authorization", accessToken.Token);
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
               await _accessTokenHandler.SetToken(accessToken).ConfigureAwait(false);
            }

            return response;
        }
    }
}