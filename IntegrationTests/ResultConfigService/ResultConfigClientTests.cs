using System;
using System.Net.Http;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.ResourceTypes.ResultConfig;
using mars_deletion_svc.Services;
using Xunit;

namespace IntegrationTests.ResultConfigService
{
    public class ResultConfigClientTests
    {
        [Fact]
        public async void DeleteResource_ValidConfigId_NoExceptionThrown()
        {
            // Arrange
            var dependantResourceModel = new DependantResourceModel
            {
                ResourceType = ResourceTypeEnum.ResultConfig,
                ResourceId = "6c40eb45-1e21-435e-81c8-895e55c6c5d8"
            };
            var httpService = new HttpService(new HttpClient());
            var resultConfigClient = new ResultConfigClient(httpService);
            Exception exception = null;

            try
            {
                // Act
                await resultConfigClient.DeleteResource(dependantResourceModel);
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