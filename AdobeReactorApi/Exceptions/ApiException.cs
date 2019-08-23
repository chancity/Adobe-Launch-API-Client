using System;
using System.Net;

namespace AdobeShared.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message)
        {
        }

        public ApiException()
        {
        }
    }

    public class ErrorResponseException : ApiException
    {
        public ErrorResponse ErrorResponse { get; }

        public ErrorResponseException(ErrorResponse errorResponse) : base(
            errorResponse?.ErrorDescription ?? errorResponse?.Error)
        {
            ErrorResponse = errorResponse;
        }

        public ErrorResponseException(string message) : base(message)
        {
        }
    }

    public class ContentNotFoundException : ErrorResponseException
    {
        public ContentNotFoundException(ErrorResponse errorResponse) : base(errorResponse)
        {
        }

        public ContentNotFoundException(string message) : base(message)
        {
        }
    }

    public class UnknownApiException : ErrorResponseException
    {
        public HttpStatusCode StatusCode { get; }

        public UnknownApiException(ErrorResponse errorResponse, HttpStatusCode code) : base(errorResponse)
        {
            StatusCode = code;
        }
    }
}