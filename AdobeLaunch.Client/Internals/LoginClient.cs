using System;
using System.Net.Http;
using System.Threading.Tasks;
using AdobeLaunch.Client.ApiConstants;
using AdobeLaunch.Client.HttpClientMiddleware;
using AdobeLaunch.Client.Models;
using Refit;

namespace AdobeLaunch.Client.Internals
{
    internal class LoginClient
    {
        internal ILoginClient Client { get; }

        public LoginClient(HttpMessageHandler innerHandler)
        {
            HttpClient httpClient = new HttpClient(new HttpExceptionHandler(innerHandler))
            {
                BaseAddress = new Uri(AdobeLogin.Hostname)
            };

            Client = RestService.For<ILoginClient>(httpClient);
        }
    }

    internal interface ILoginClient
    {
        [Multipart]
        [Post("/ims/exchange/jwt/")]
        Task<AccessToken> Login([AliasAs("client_id")] string clientId, [AliasAs("client_secret")] string clientSecret,
            [AliasAs("jwt_token")] string jwtToken);
    }
}