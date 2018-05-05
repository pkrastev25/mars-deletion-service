using System;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using mars_deletion_svc.BackgroundJobs;
using mars_deletion_svc.BackgroundJobs.Enums;
using Moq;
using Xunit;

namespace UnitTests.BackgroundJobs
{
    public class BackgroundJobsHandlerTests
    {
        [Fact]
        public async void CreateBackgroundJob_GetJobStatusForBackgroundJobId_JobIsCreated()
        {
            // Arrange
            var backgroundJobId = "5aec01fa8826ca000777996d";
            var backgroundJobClient = new Mock<IBackgroundJobClient>();
            backgroundJobClient
                .SetReturnsDefault("12345");
            var backgroundJobsHandler = new BackgroundJobsHandler(
                backgroundJobClient.Object
            );

            // Act
            await backgroundJobsHandler.CreateBackgroundJob(
                () => backgroundJobsHandler.GetJobStatusForBackgroundJobId(backgroundJobId)
            );

            // Assert
            backgroundJobClient.Verify(x => x.Create(
                It.Is<Job>(job =>
                    job.Method.Name == "GetJobStatusForBackgroundJobId" && (string) job.Args[0] == backgroundJobId),
                It.IsAny<EnqueuedState>()
            ));
        }

        [Fact]
        public async void GetJobStatusForBackgroundJobId_InvalidJobId_ThrowsException()
        {
            // Arrange
            var backgroundJobId = "0";
            var backgroundJobClient = new Mock<IBackgroundJobClient>();
            var backgroundJobsHandler = new BackgroundJobsHandler(
                backgroundJobClient.Object
            );
            Exception exception = null;

            try
            {
                // Act
                await backgroundJobsHandler.GetJobStatusForBackgroundJobId(backgroundJobId);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public async void GetJobStatusForBackgroundJobId_ValidJobId_ReturnsDoneStatus()
        {
            // Arrange
            var backgroundJobId = "5aec01fa8826ca000777996d";
            var backgroundJobClient = new Mock<IBackgroundJobClient>();
            var storageConnection = new Mock<IStorageConnection>();
            storageConnection
                .Setup(m => m.GetJobData(It.IsAny<string>()))
                .Returns(new JobData {State = BackgroundJobStateEnum.HangfireStateSucceededForBackgroundJob});
            var jobStorage = new Mock<JobStorage>();
            jobStorage
                .Setup(m => m.GetConnection())
                .Returns(storageConnection.Object);
            JobStorage.Current = jobStorage.Object;

            var backgroundJobsHandler = new BackgroundJobsHandler(
                backgroundJobClient.Object
            );

            // Act
            var status = await backgroundJobsHandler.GetJobStatusForBackgroundJobId(backgroundJobId);

            // Assert
            Assert.Equal(BackgroundJobStateEnum.StateDoneForBackgroundJob, status);
        }
    }
}