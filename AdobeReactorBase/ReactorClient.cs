using System;
using System.Net.Http;
using Refit;

namespace AdobeReactorBase
{
    public class ReactorClient
    {
        public IReactorClient Client { get; }
        public HttpClient HttpClient { get; }

        public ReactorClient(string url, string organizationId, string clientId, HttpMessageHandler innerHandler)
        {
            HttpClient httpClient = new HttpClient(innerHandler)
            {
                BaseAddress = new Uri(url),
            };

            httpClient.SetDefaultRequestHeaders(organizationId, clientId);


            Client = RestService.For<IReactorClient>(httpClient);
            HttpClient = httpClient;
        }
    }
}