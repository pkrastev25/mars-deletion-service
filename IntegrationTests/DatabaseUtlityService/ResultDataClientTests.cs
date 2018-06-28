using System;
using System.Net.Http;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.ResourceTypes.ResultData;
using mars_deletion_svc.Services;
using Xunit;

namespace IntegrationTests.DatabaseUtlityService
{
    public class ResultDataClientTests
    {
        [Fact]
        public async void DeleteResource_ValidResultDataId_NoExceptionThrown()
        {
            // Arrange
            var dependantResourceModel = new DependantResourceModel
            {
                ResourceType = ResourceTypeEnum.ResultData,
                ResourceId = "7ada0dd6-0bd8-4872-9344-25555ca652a8"
            };
            var httpService = new HttpService(new HttpClient());
            var resultDataClient = new ResultDataClient(httpService);
            Exception exception = null;

            try
            {
                // Act
                await resultDataClient.DeleteResource(dependantResourceModel);
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