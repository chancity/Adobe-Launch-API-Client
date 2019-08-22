using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdobeLaunch.Client.HelperInterfaces;
using AdobeLaunch.Client.HttpClientMiddleware;
using AdobeLaunch.Client.Internals;
using AdobeLaunch.Client.Models;

namespace AdobeLaunch.Client
{
    public class ReactorApi
    {
        private readonly ReactorClient _reactorClient;

        public ReactorApi(AccountOptions accountOptions, ITokenHandler<AccessToken> accessTokenHandler = null, HttpClientHandler httpClientHandler = null)
        {
            httpClientHandler = httpClientHandler ?? new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None
            };

            var loginClient = new LoginClient(httpClientHandler);
            var internalAccessTokenGetter = new InternalAccessTokenGetter(loginClient, accountOptions, accessTokenHandler);

            _reactorClient = new ReactorClient(accountOptions, internalAccessTokenGetter, httpClientHandler);
        }

        public IReactorClient Client => _reactorClient.Client;
        public HttpClient HttpClient => _reactorClient.HttpClient;
    }
}