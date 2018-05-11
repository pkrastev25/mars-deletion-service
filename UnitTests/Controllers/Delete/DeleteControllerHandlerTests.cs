using System;
using System.Threading.Tasks;
using mars_deletion_svc.Controllers;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.MarkSession.Interfaces;
using mars_deletion_svc.MarkSession.Models;
using Moq;
using Xunit;

namespace UnitTests.Controllers.Delete
{
    public class DeleteControllerHandlerTests
    {
        [Fact]
        public async void CreateMarkSessionAndDeleteDependantResurces_SuccessfulDelete_NoExceptionThrown()
        {
            // Arrange
            var markingServiceClient = new Mock<IMarkingServiceClient>();
            markingServiceClient
                .Setup(m => m.CreateMarkSession(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                )).ReturnsAsync(It.IsAny<MarkSessionModel>());
            var markSessionHandler = new Mock<IMarkSessionHandler>();
            markSessionHandler
                .Setup(m => m.DeleteMarkSessionAndDependantResources(It.IsAny<MarkSessionModel>()))
                .ReturnsAsync(It.IsAny<string>());
            var deleteControllerHandler = new DeleteControllerHandler(
                markingServiceClient.Object,
                markSessionHandler.Object
            );
            Exception exception = null;

            try
            {
                // Act
                await deleteControllerHandler.CreateMarkSessionAndDeleteDependantResurces(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                );
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async void DeleteMarkSessionAndDependantResources_SuccessfulDelete_ReturnsBackgroundJobId()
        {
            // Arrange
            var backgroundJobId = "1234";
            var markingServiceClient = new Mock<IMarkingServiceClient>();
            markingServiceClient
                .Setup(m => m.GetMarkSessionById(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<MarkSessionModel>());
            markingServiceClient
                .Setup(m => m.UpdateMarkSessionType(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var markSessionHandler = new Mock<IMarkSessionHandler>();
            markSessionHandler
                .Setup(m => m.DeleteMarkSessionAndDependantResources(It.IsAny<MarkSessionModel>()))
                .ReturnsAsync(backgroundJobId);
            var deleteControllerHandler = new DeleteControllerHandler(
                markingServiceClient.Object,
                markSessionHandler.Object
            );

            // Act
            var result = await deleteControllerHandler.DeleteMarkSessionAndDependantResources(It.IsAny<string>());

            // Assert
            Assert.Equal(result, backgroundJobId);
        }
    }
}