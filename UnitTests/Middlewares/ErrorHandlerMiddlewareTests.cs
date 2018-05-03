using System;
using System.Net;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.Middlewares;
using mars_deletion_svc.Services;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace UnitTests.Middlewares
{
    public class ErrorHandlerMiddlewareTests
    {
        [Fact]
        public async void Invoke_ResourceConflictException_ReturnsConflictStatusCode()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var loggerService = new LoggerService();
            var errorHandlerMiddleware = new ErrorHandlerMiddleware(
                async innerHttpContext => await Task.FromException(new ResourceConflictException("")),
                loggerService
            );

            // Act
            await errorHandlerMiddleware.Invoke(httpContext);

            // Asset
            Assert.Equal((int) HttpStatusCode.Conflict, httpContext.Response.StatusCode);
        }

        [Fact]
        public async void Invoke_GenericException_ReturnsInternalServerErrorStatusCode()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var loggerService = new LoggerService();
            var errorHandlerMiddleware = new ErrorHandlerMiddleware(
                async innerHttpContext => await Task.FromException(new Exception("")),
                loggerService
            );

            // Act
            await errorHandlerMiddleware.Invoke(httpContext);

            // Asset
            Assert.Equal((int) HttpStatusCode.InternalServerError, httpContext.Response.StatusCode);
        }
    }
}