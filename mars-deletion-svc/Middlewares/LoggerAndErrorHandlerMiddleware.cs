using System;
using System.Diagnostics;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.Middlewares.Models;
using mars_deletion_svc.Services.Inerfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace mars_deletion_svc.Middlewares
{
    public class LoggerAndErrorHandlerMiddleware
    {
        private const int StatusCodeNoContent = 204;
        private const int StatusCodeConflict = 409;
        private const int StatusCodeInternalServerError = 500;
        
        private const string ContentTypeJson = "application/json";

        private readonly RequestDelegate _requestDelegate;
        private readonly ILoggerService _loggerService;

        public LoggerAndErrorHandlerMiddleware(
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
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _requestDelegate(httpContext);

                stopwatch.Stop();
                _loggerService.LogInfoEvent(
                    stopwatch.Elapsed.TotalSeconds,
                    CreateRequestAndResponseMessage(httpContext)
                );
            }
            catch (Exception e)
            {
                stopwatch.Stop();

                await HandleException(httpContext, e);
                _loggerService.LogInfoWithErrorEvent(
                    stopwatch.Elapsed.TotalSeconds,
                    CreateRequestAndResponseMessage(httpContext),
                    e
                );
            }
        }

        private string CreateRequestAndResponseMessage(
            HttpContext httpContext
        )
        {
            return
                $"{httpContext.Response.StatusCode} {httpContext.Request.Method} {httpContext.Request.Path.Value}{httpContext.Request.QueryString.Value}";
        }

        private Task HandleException(
            HttpContext httpContext,
            Exception exception
        )
        {
            var result = JsonConvert.SerializeObject(new ErrorMessageModel(exception.Message));
            httpContext.Response.ContentType = ContentTypeJson;
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