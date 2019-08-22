using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace AdobeLaunch.Client.Extensions
{
    internal static class HttpClientEx
    {
        public static void FixAcceptHeader(this HttpClient httpClient)
        {
            foreach (var value in httpClient.DefaultRequestHeaders.Accept)
            {
                if (value.MediaType.Contains("application/vnd.api+json"))
                {
                    var field = value.GetType().GetTypeInfo().BaseType.GetField(
                        "_mediaType",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    field.SetValue(value, "application/vnd.api+json;revision=1");
                    value.CharSet = "";
                    value.Parameters.Clear();
                }
            }
        }

        public static void SetDefaultRequestHeaders(this HttpClient httpClient, AccountOptions accountOptions)
        {
            httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() {NoCache = true};
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "C# Client v1.0.0");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/vnd.api+json;revision=1");
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Gw-Ims-Org-Id", accountOptions.OrganizationId);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Api-Key", accountOptions.ClientId);
            httpClient.FixAcceptHeader();
        }
    }
}