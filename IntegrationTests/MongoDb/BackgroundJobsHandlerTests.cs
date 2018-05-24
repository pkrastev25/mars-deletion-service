using Hangfire;
using mars_deletion_svc.BackgroundJobs;
using mars_deletion_svc.BackgroundJobs.Enums;
using Xunit;

namespace IntegrationTests.MongoDb
{
    public class BackgroundJobsHandlerTests
    {
        [Fact]
        public async void GetJobStatusForBackgroundJobId_ValidBackgroundJobId_ThrowsException()
        {
            // Arrange
            var backgroundJobId = "5af329e9baf2ac0007a3d134";
            var backgroundJobClient = new BackgroundJobClient();
            var backgroundJobsHandler = new BackgroundJobsHandler(
                backgroundJobClient
            );

            // Act
            var result = await backgroundJobsHandler.GetJobStatusForBackgroundJobId(backgroundJobId);

            // Assert
            Assert.Equal(result, BackgroundJobStateEnum.StateDoneForBackgroundJob);
        }
    }
}