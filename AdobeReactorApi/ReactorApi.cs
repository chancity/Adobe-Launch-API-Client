using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdobeJwt;
using AdobeLoginBase;
using AdobeReactorApi.Events;
using AdobeReactorApi.Extensions;
using AdobeReactorApi.HttpClientHandlers;
using AdobeReactorBase;
using Microsoft.IdentityModel.Tokens;

namespace AdobeReactorApi
{
    public class ReactorApi : AbstractTokenEvents
    {
        public AccessToken Token { get; internal set; }
        public IReactorClient Client => _reactorClient.Client;
        public HttpClient HttpClient => _reactorClient.HttpClient;

        private ReactorClient _reactorClient;
        private readonly AccountOptions _accountOptions;
        private readonly LoginClient _loginClient;
        private readonly JwtProvider _jwtProvider;


        public ReactorApi(AccountOptions accountOptions)
        {
            _accountOptions = accountOptions;
        }

        public ReactorApi(AccountOptions accountOptions, AccessToken token, HttpClientHandler httpClientHandler = null) : this(accountOptions)
        {
            httpClientHandler = GetHttpClientHandler(httpClientHandler);
            SetReactorClient(new ExceptionHandler(httpClientHandler));
            Token = token;
        }

        public ReactorApi(AccountOptions accountOptions, SecurityKey securityKey, TimeSpan? jwtExpiresIn = null, HttpClientHandler httpClientHandler = null) : this(accountOptions)
        {
            var expiresIn = jwtExpiresIn ?? TimeSpan.FromDays(1);
            _jwtProvider = new JwtProvider(accountOptions.ToJwtPayloadOptions(expiresIn), securityKey);

            httpClientHandler = GetHttpClientHandler(httpClientHandler);

            _loginClient = new LoginClient(Constants.AdobeLoginUrl, new ExceptionHandler(httpClientHandler));

            var authorizationHandler = new AuthorizationHandler(AccessTokenGetter, httpClientHandler);

            SetReactorClient(new ExceptionHandler(authorizationHandler));
        }

        private static HttpClientHandler GetHttpClientHandler(HttpClientHandler httpClientHandler)
        {
            return httpClientHandler ?? new HttpClientHandler
            {
                AutomaticDecompression =
                    DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None
            };
        }
        private void SetReactorClient(HttpMessageHandler httpMessageHandler)
        {
            _reactorClient = new ReactorClient(Constants.ReactorUrl, _accountOptions.OrganizationId, _accountOptions.ClientId, httpMessageHandler);
        }

        private async Task<AccessToken> AccessTokenGetter()
        {
            if (Token == null || Token.IsExpired)
                Token = await Login().ConfigureAwait(false);
            return Token;
        }

        private async Task<AccessToken> Login()
        {
            var jwtToken = await _jwtProvider.GenerateJwtToken().ConfigureAwait(false);
            var exchangeToken = new ExchangeToken(_accountOptions.ClientId, _accountOptions.ClientSecret, jwtToken);
            return await _loginClient.Login(exchangeToken.ClientId, exchangeToken.ClientSecret, exchangeToken.JwtToken).ConfigureAwait(false);
        }
    }
}