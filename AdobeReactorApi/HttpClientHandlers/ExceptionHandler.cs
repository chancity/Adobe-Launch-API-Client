using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AdobeShared.Exceptions;
using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.Validation;

namespace AdobeReactorApi.HttpClientHandlers
{
    public class ExceptionHandler : DelegatingHandler
    {
        private readonly JsonSchema4 _schema;

        public ExceptionHandler(HttpMessageHandler innerHandler)
        {
            _schema = JsonSchema4.FromTypeAsync<ErrorResponse>().Result;
            InnerHandler = innerHandler;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            try
            {
                HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    string responseMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HandleUnsuccessfulStatusCode(responseMessage, response.StatusCode);
                }
                else if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    throw new ContentNotFoundException("Content not found");
                }

                return response;
            }
            catch (WebException wex)
            {
                wex?.Response?.Dispose();
                throw;
            }
        }

        private void HandleUnsuccessfulStatusCode(string responseMessage, HttpStatusCode statusCode)
        {
            if (!TryParseErrorResponse(responseMessage, out ErrorResponse errorResponse))
            {
                throw new UnknownApiException(errorResponse, statusCode);
            }

            throw new ErrorResponseException(errorResponse);
        }

        private bool TryParseErrorResponse(string jsonResponse, out ErrorResponse error)
        {
            if (!jsonResponse.Contains("error_description") ||
                !jsonResponse.Contains("error"))
            {
                error = null;
                return false;
            }

            ICollection<ValidationError> errors = _schema.Validate(jsonResponse);

            if (errors.Count > 0)
            {
                error = null;
                return false;
            }

            try
            {
                error = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);
                return true;
            }
            catch
            {
                error = null;
                return false;
            }
        }
    }
}