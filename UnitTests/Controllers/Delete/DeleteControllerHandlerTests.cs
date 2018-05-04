using System;
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
    }
}