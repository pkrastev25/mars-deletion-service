using System;
using System.Net.Http;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.ResourceTypes.Metadata;
using mars_deletion_svc.Services;
using Xunit;

namespace IntegrationTests.FileService
{
    public class FileClientTests
    {
        [Fact]
        public async void DeleteResource_ValidMetadataId_NoExceptionThrown()
        {
            // Arrange
            var dependantResourceModel = new DependantResourceModel
            {
                ResourceType = ResourceTypeEnum.Metadata,
                ResourceId = "4439722e-a6d0-4f7a-9d33-0cc5a2a66da0"
            };
            var httpService = new HttpService(new HttpClient());
            var fileClient = new FileClient(httpService);
            Exception exception = null;

            try
            {
                // Act
                await fileClient.DeleteResource(dependantResourceModel);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }
    }
}