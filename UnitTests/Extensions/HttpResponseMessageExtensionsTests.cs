using System;
using System.Net;
using System.Net.Http;
using mars_deletion_svc.MarkSession.Models;
using mars_deletion_svc.Utils;
using UnitTests._DataMocks;
using Xunit;

namespace UnitTests.Extensions
{
    public class HttpResponseMessageExtensionsTests
    {
        [Fact]
        public async void Deserialize_ModelJson_ReturnsModel()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                Content = new StringContent(MarkSessionModelDataMocks.MockMarkSessionModelJson)
            };

            // Act
            var markSessionModel = await httpResponseMessage.Deserialize<MarkSessionModel>();

            // Assert
            Assert.NotNull(markSessionModel);
        }

        [Fact]
        public void ThrowExceptionIfNotSuccessfulResponse_OkStatusCode_NoExceptionThrown()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
            Exception exception = null;

            try
            {
                // Act
                httpResponseMessage.ThrowExceptionIfNotSuccessfulResponse(
                    new Exception()
                );
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void ThrowExceptionIfNotSuccessfulResponse_InternalServerErrorStatusCode_ThrowsException()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            };
            Exception exception = null;

            try
            {
                // Act
                httpResponseMessage.ThrowExceptionIfNotSuccessfulResponse(
                    new Exception()
                );
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void ThrowExceptionIfNotSuccessfulResponseOrNot404Response_NotFoundStatusCode_NoExceptionThrown()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            };
            Exception exception = null;

            try
            {
                // Act
                httpResponseMessage.ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
                    new Exception()
                );
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void
            ThrowExceptionIfNotSuccessfulResponseOrNot404Response_InternalServerErrorStatusCode_ThrowsException()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError
            };
            Exception exception = null;

            try
            {
                // Act
                httpResponseMessage.ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
                    new Exception()
                );
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void IsEmptyResponse_NoContentStatusCode_ReturnsTrue()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent
            };

            // Act
            var result = httpResponseMessage.IsEmptyResponse();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmptyResponse_OkStatusCode_ReturnsFalse()
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };

            // Act
            var result = httpResponseMessage.IsEmptyResponse();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async void FormatRequestAndResponse_ConflictStatusCode_ReturnsString()
        {
            // Arrange
            var fallbackMessage = "Some message";
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Conflict,
                Content = new StringContent("Some error has occured!")
            };

            // Act
            var result = await httpResponseMessage.FormatRequestAndResponse(fallbackMessage);

            // Assert
            Assert.NotEmpty(result);
        }
    }
}