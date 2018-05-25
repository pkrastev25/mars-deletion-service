using System;
using System.Net.Http;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.ResourceTypes.SimPlan;
using mars_deletion_svc.Services;
using Xunit;

namespace IntegrationTests.SimRunnerService
{
    public class SimPlanClientTests
    {
        [Fact]
        public async void DeleteResource_ValidSimPlanIdId_NoExceptionThrown()
        {
            // Arrange
            var projectId = "be69cb8c-45e4-4d80-8d55-419984aa2151";
            var dependantResourceModel = new DependantResourceModel
            {
                ResourceType = ResourceTypeEnum.SimPlan,
                ResourceId = "5b07ddcf52e35100015f04e3"
            };
            var httpService = new HttpService(new HttpClient());
            var simPlanClient = new SimPlanClient(httpService);
            Exception exception = null;

            try
            {
                // Act
                await simPlanClient.DeleteResource(dependantResourceModel, projectId);
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