using System;
using System.Threading.Tasks;
using mars_deletion_svc.DependantResource;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.Metadata.Interfaces;
using mars_deletion_svc.ResourceTypes.ResultConfig.Interfaces;
using mars_deletion_svc.ResourceTypes.ResultData.Interfaces;
using mars_deletion_svc.ResourceTypes.Scenario.Interfaces;
using mars_deletion_svc.ResourceTypes.SimPlan.Interfaces;
using mars_deletion_svc.ResourceTypes.SimRun.Interfaces;
using Moq;
using UnitTests._DataMocks;
using Xunit;

namespace UnitTests.DependantResource
{
    public class DependantResourceHandlerTests
    {
        [Fact]
        public async void DeleteDependantResourcesForMarkSession_MarkSessionModel_NoExceptionThrown()
        {
            // Arrange
            var fileClient = new Mock<IFileClient>();
            fileClient
                .Setup(m => m.DeleteResource(It.IsAny<DependantResourceModel>()))
                .Returns(Task.CompletedTask);
            var scenarioClient = new Mock<IScenarioClient>();
            scenarioClient
                .Setup(m => m.DeleteResource(It.IsAny<DependantResourceModel>()))
                .Returns(Task.CompletedTask);
            var resultConfigClient = new Mock<IResultConfigClient>();
            resultConfigClient
                .Setup(m => m.DeleteResource(It.IsAny<DependantResourceModel>()))
                .Returns(Task.CompletedTask);
            var simPlanClient = new Mock<ISimPlanClient>();
            simPlanClient
                .Setup(m => m.DeleteResource(It.IsAny<DependantResourceModel>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var simRunClient = new Mock<ISimRunClient>();
            simRunClient
                .Setup(m => m.DeleteResource(It.IsAny<DependantResourceModel>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var resultDataClient = new Mock<IResultDataClient>();
            resultDataClient
                .Setup(m => m.DeleteResource(It.IsAny<DependantResourceModel>()))
                .Returns(Task.CompletedTask);
            var dependantResourceHandler = new DependantResourceHandler(
                fileClient.Object,
                scenarioClient.Object,
                resultConfigClient.Object,
                simPlanClient.Object,
                simRunClient.Object,
                resultDataClient.Object
            );
            Exception exception = null;

            try
            {
                // Act
                await dependantResourceHandler.DeleteDependantResourcesForMarkSession(
                    MarkSessionModelDataMocks.MockMarkSessionModel()
                );
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