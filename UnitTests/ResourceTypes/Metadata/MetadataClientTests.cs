using System;
using System.Net;
using System.Net.Http;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.ResourceTypes.Metadata;
using mars_deletion_svc.Services;
using mars_deletion_svc.Services.Inerfaces;
using Moq;
using UnitTests.DataMocks;
using Xunit;

namespace UnitTests.ResourceTypes.Metadata
{
    public class MetadataClientTests
    {
        [Fact]
        public async void MetadataClient_NotFoundStatusCode_DoesNotThrowException()
        {
            // Assert
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("")
            };
            httpService
                .Setup(m => m.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var logerService = new LoggerService();
            var metadataClient = new MetadataClient(
                httpService.Object,
                logerService
            );
            Exception exception = null;

            try
            {
                // Act
                await metadataClient.DeleteResource(DependantResourceDataMocks.MockDependantResourceModel());
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Asset
            Assert.Null(exception);
        }

        [Fact]
        public async void MetadataClient_InternalServerErrorResponse_DoesNotThrowException()
        {
            // Assert
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("")
            };
            httpService
                .Setup(m => m.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var logerService = new LoggerService();
            var metadataClient = new MetadataClient(
                httpService.Object,
                logerService
            );

            // Assert
            await Assert.ThrowsAsync<FailedToDeleteResourceException>(
                // Act
                async () =>
                    await metadataClient.DeleteResource(
                        DependantResourceDataMocks.MockDependantResourceModel()
                    )
            );
        }
    }
}