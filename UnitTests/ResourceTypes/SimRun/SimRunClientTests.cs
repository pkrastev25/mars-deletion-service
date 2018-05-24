using System;
using System.Net;
using System.Net.Http;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.ResourceTypes.SimRun;
using mars_deletion_svc.Services.Inerfaces;
using Moq;
using UnitTests._DataMocks;
using Xunit;

namespace UnitTests.ResourceTypes.SimRun
{
    public class SimRunClientTests
    {
        [Fact]
        public async void DeleteResource_OkStatusCode_NoExceptionThrown()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
            };
            httpService
                .Setup(m => m.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            httpService
                .Setup(m => m.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var simRunClient = new SimRunClient(
                httpService.Object
            );
            Exception exception = null;

            try
            {
                // Act
                await simRunClient.DeleteResource(
                    DependantResourceDataMocks.MockDependantResourceModel(),
                    It.IsAny<string>()
                );
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Asset
            Assert.Null(exception);
        }

        [Fact]
        public async void SimRunClient_InternalServerErrorStatusCode_ThrowsException()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("")
            };
            httpService
                .Setup(m => m.GetAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var simRunClient = new SimRunClient(
                httpService.Object
            );
            Exception exception = null;

            try
            {
                // Act
                await simRunClient.DeleteResource(
                    DependantResourceDataMocks.MockDependantResourceModel(),
                    It.IsAny<string>()
                );
            }
            catch (FailedToGetResourceException e)
            {
                exception = e;
            }

            // Assert
            Assert.IsType<FailedToGetResourceException>(exception);
        }
    }
}