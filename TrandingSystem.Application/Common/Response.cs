using System;
using System.Net;

namespace TradingSystem.Application.Common.Response
{
    public class Response<T>
    {
        public T Data { get; set; }
        public T? Data2 { get; set; }
        public string Message { get; set; }
        public string MessageOfDeveloper { get; set; }
        public HttpStatusCode? Status { get; set; }
        public bool Success { get; set; }

        public Response() { }

        // Private constructor for static methods
        private Response(T data, string message, bool success, HttpStatusCode status, string devMsg = null, T? data2 = default)
        {
            Data = data;
            Data2 = data2;
            Message = message;
            MessageOfDeveloper = devMsg;
            Status = status;
            Success = success;
        }

        // Static helper for success
        public static Response<T> SuccessResponse(T data, string message = "Everything is okay", T? data2 = default)
        {
            return new Response<T>(data, message, true, HttpStatusCode.OK, null, data2);
        }

        // Static helper for error
        public static Response<T> ErrorResponse(string errorMessage, T data = default, HttpStatusCode status = HttpStatusCode.ExpectationFailed)
        {
            return new Response<T>(data,  errorMessage, false, status, errorMessage);
        }

        // Static helper for error with exception
        public static Response<T> ErrorWithException(string errorMessage, string exceptionMsg, T data = default, HttpStatusCode status = HttpStatusCode.ExpectationFailed)
        {
            return new Response<T>(data, errorMessage, false, status, exceptionMsg);
        }
    }
}
