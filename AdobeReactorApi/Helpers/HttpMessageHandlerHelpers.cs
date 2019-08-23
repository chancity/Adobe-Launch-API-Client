using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using AdobeReactorApi.HttpClientHandlers;

namespace AdobeReactorApi.Helpers
{
    internal static class HttpMessageHandlerHelpers
    {
        internal static ExceptionHandler GetExceptionHandler(this HttpClientHandler httpClientHandler)
        {
            return new ExceptionHandler(GetHttpClientHandler(httpClientHandler));
        }

        private static HttpClientHandler GetHttpClientHandler(HttpClientHandler httpClientHandler)
        {
            return httpClientHandler ?? new HttpClientHandler
            {
                AutomaticDecompression =
                    DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.None
            };
        }
    }
}
