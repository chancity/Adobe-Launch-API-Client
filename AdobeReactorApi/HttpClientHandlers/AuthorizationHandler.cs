using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AdobeLoginBase;

namespace AdobeReactorApi.HttpClientHandlers
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private readonly Func<Task<AccessToken>> _accessTokenGetter;

        public AuthorizationHandler(Func<Task<AccessToken>> accessTokenGetter, HttpMessageHandler innerHandler)
        {
            InnerHandler = innerHandler;
            _accessTokenGetter = accessTokenGetter;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _accessTokenGetter().ConfigureAwait(false);
            request.Headers.Add("Authorization", $"Bearer {accessToken.Token}");
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}