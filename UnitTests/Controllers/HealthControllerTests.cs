using mars_deletion_svc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTests.Controllers
{
    public class HealthControllerTests
    {
        [Fact]
        public void HealthController_SuccessfulRequest_ReturnsOkResult()
        {
            // Arrange
            var healthController = new HealtController();

            // Act
            var result = healthController.HealthCheck();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}