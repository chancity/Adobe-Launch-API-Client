using System;
using System.Net.Http;
using Refit;

namespace AdobeReactorBase
{
    public class ReactorClient
    {
        public IReactorClient Client { get; }
        public HttpClient HttpClient { get; }

        public ReactorClient(HttpClient httpClient, string organizationId, string clientId)
        {
            httpClient.SetDefaultRequestHeaders(organizationId, clientId);

            Client = RestService.For<IReactorClient>(httpClient);
            HttpClient = httpClient;
        }

        public ReactorClient(string url, string organizationId, string clientId, HttpMessageHandler innerHandler) :
            this(new HttpClient(innerHandler) {BaseAddress = new Uri(url)}, organizationId, clientId) { }
    }
}