using mars_deletion_svc.Controllers;
using Xunit;

namespace UnitTests.Controllers
{
    public class HealthControllerTests
    {
        [Fact]
        public void HealthController_SuccessfulRequest_ReturnsOkResult()
        {
            // Arrange
            var healthController = new HealthController();

            // Act
            var result = healthController.HealthCheck();

            // Assert
            Assert.NotNull(result);
        }
    }
}