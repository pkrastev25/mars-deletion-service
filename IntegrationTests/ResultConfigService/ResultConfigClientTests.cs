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
                ResourceId = "e019f476-b413-4ee7-965b-a4c0389cd086"
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