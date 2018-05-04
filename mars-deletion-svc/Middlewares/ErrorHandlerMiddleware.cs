using System;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.Services.Inerfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace mars_deletion_svc.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private const int StatusCodeNoContent = 204;
        private const int StatusCodeConflict = 409;
        private const int StatusCodeInternalServerError = 500;

        private readonly RequestDelegate _requestDelegate;
        private readonly ILoggerService _loggerService;

        public ErrorHandlerMiddleware(
            RequestDelegate requestDelegate,
            ILoggerService loggerService
        )
        {
            _requestDelegate = requestDelegate;
            _loggerService = loggerService;
        }

        public async Task Invoke(
            HttpContext httpContext
        )
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception e)
            {
                _loggerService.LogErrorEvent(e);
                await HandleException(httpContext, e);
            }
        }

        private Task HandleException(
            HttpContext httpContext,
            Exception exception
        )
        {
            var result = JsonConvert.SerializeObject(new {error = exception.Message});
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = GetStatusCodeForError(exception);

            return httpContext.Response.WriteAsync(result);
        }

        private int GetStatusCodeForError(
            Exception exception
        )
        {
            switch (exception)
            {
                case BackgroundJobDoesNotExistException _:
                    return StatusCodeNoContent;
                case ResourceConflictException _:
                    return StatusCodeConflict;
            }

            return StatusCodeInternalServerError;
        }
    }
}