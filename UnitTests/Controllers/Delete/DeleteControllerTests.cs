using System.Threading.Tasks;
using mars_deletion_svc.Controllers;
using mars_deletion_svc.Controllers.Interfaces;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.ResourceTypes.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTests.Controllers.Delete
{
    public class DeleteControllerTests
    {
        [Fact]
        public async void DeleteResource_NoExceptions_ReturnsOkResult()
        {
            // Arrange
            var resourceType = ResourceTypeEnum.Project;
            var resourceId = "0";
            var projectId = "0";

            var deleteControllerHandler = new Mock<IDeleteControllerHandler>();
            deleteControllerHandler
                .Setup(m => m.CreateMarkSessionAndDeleteDependantResurces(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                )).Returns(Task.CompletedTask);
            var deleteController = new DeleteController(deleteControllerHandler.Object);

            // Act
            var result = await deleteController.DeleteResource(
                resourceType,
                resourceId,
                projectId
            );

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void DeleteResource_EmptyResourceType_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var resourceType = "";
            var resourceId = "0";
            var projectId = "0";

            var deleteControllerHandler = new Mock<IDeleteControllerHandler>();
            deleteControllerHandler
                .Setup(m => m.CreateMarkSessionAndDeleteDependantResurces(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()
                )).ThrowsAsync(new ResourceConflictException(It.IsAny<string>()));
            var deleteController = new DeleteController(deleteControllerHandler.Object);

            // Act
            var result = await deleteController.DeleteResource(
                resourceType,
                resourceId,
                projectId
            );

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}