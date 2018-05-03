﻿using System;
using System.Net;
using System.Net.Http;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.ResourceTypes.Metadata;
using mars_deletion_svc.Services.Inerfaces;
using Moq;
using UnitTests._DataMocks;
using Xunit;

namespace UnitTests.ResourceTypes.Metadata
{
    public class MetadataClientTests
    {
        [Fact]
        public async void DeleteResource_NotFoundStatusCode_NoExceptionThrown()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent("")
            };
            httpService
                .Setup(m => m.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var logerService = new Mock<ILoggerService>();
            var metadataClient = new MetadataClient(
                httpService.Object,
                logerService.Object
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
        public async void DeleteResource_InternalServerErrorStatusCode_ThrowsException()
        {
            // Arrange
            var httpService = new Mock<IHttpService>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("")
            };
            httpService
                .Setup(m => m.DeleteAsync(It.IsAny<string>()))
                .ReturnsAsync(httpResponseMessage);
            var logerService = new Mock<ILoggerService>();
            var metadataClient = new MetadataClient(
                httpService.Object,
                logerService.Object
            );
            Exception exception = null;

            try
            {
                // Act
                await metadataClient.DeleteResource(
                    DependantResourceDataMocks.MockDependantResourceModel()
                );
            }
            catch (FailedToDeleteResourceException e)
            {
                exception = e;
            }

            // Assert
            Assert.IsType<FailedToDeleteResourceException>(exception);
        }
    }
}