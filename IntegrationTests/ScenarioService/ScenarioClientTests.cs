using System;
using System.Net.Http;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.ResourceTypes.Scenario;
using mars_deletion_svc.Services;
using Xunit;

namespace IntegrationTests.ScenarioService
{
    public class ScenarioClientTests
    {
        [Fact]
        public async void DeleteResource_ValidScenarioId_NoExceptionThrown()
        {
            // Arrange
            var dependantResourceModel = new DependantResourceModel
            {
                ResourceType = ResourceTypeEnum.Scenario,
                ResourceId = "6aad965db-fceb-4ee2-bd06-c89ab182ca4c"
            };
            var httpService = new HttpService(new HttpClient());
            var scenarioClient = new ScenarioClient(httpService);
            Exception exception = null;

            try
            {
                // Act
                await scenarioClient.DeleteResource(dependantResourceModel);
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