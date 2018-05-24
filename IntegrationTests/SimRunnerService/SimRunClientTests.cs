using System;
using System.Net.Http;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.ResourceTypes.SimRun;
using mars_deletion_svc.Services;
using Xunit;

namespace IntegrationTests.SimRunnerService
{
    public class SimRunClientTests
    {
        [Fact]
        public async void DeleteResource_ValidSimRunId_NoExceptionThrown()
        {
            // Arrange
            var projectId = "623be379-ed40-49f3-bdd8-416f8cd0faa6";
            var dependantResourceModel = new DependantResourceModel
            {
                ResourceType = ResourceTypeEnum.SimRun,
                ResourceId = "5b06acef52e35100015f04da"
            };
            var httpService = new HttpService(new HttpClient());
            var simRunClient = new SimRunClient(httpService);
            Exception exception = null;

            try
            {
                // Act
                await simRunClient.DeleteResource(dependantResourceModel, projectId);
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