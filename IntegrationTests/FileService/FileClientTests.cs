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
                ResourceId = "c9de8a5e-1ab1-431f-a759-f44d7eef4e19"
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