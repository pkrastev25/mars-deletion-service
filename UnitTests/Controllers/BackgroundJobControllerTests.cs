using mars_deletion_svc.BackgroundJobs.Enums;
using mars_deletion_svc.BackgroundJobs.Interfaces;
using mars_deletion_svc.Controllers;
using Moq;
using Xunit;

namespace UnitTests.Controllers
{
    public class BackgroundJobControllerTests
    {
        [Fact]
        public async void GetStatusForBackgroundJob_EmptyBackgroundJobId_ReturnsBadRequestObjectResult()
        {
            // Arrange
            var backgroundJobId = "";
            var backgroundJobsHandler = new Mock<IBackgroundJobsHandler>();
            var backgroundJobController = new BackgroundJobController(backgroundJobsHandler.Object);

            // Act
            var result = await backgroundJobController.GetStatusForBackgroundJob(backgroundJobId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetStatusForBackgroundJob_ValidBackgroundJobId_ReturnsBadRequestResult()
        {
            // Arrange
            var backgroundJobId = "5aec01fa8826ca000777996d";
            var backgroundJobsHandler = new Mock<IBackgroundJobsHandler>();
            backgroundJobsHandler
                .Setup(m => m.GetJobStatusForBackgroundJobId(backgroundJobId))
                .ReturnsAsync(BackgroundJobStateEnum.StateProcessingForBackgroundJob);
            var backgroundJobController = new BackgroundJobController(backgroundJobsHandler.Object);

            // Act
            var result = await backgroundJobController.GetStatusForBackgroundJob(backgroundJobId);

            // Assert
            Assert.NotNull(result);
        }
    }
}