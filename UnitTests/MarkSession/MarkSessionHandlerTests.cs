﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using mars_deletion_svc.BackgroundJobs.Interfaces;
using mars_deletion_svc.DependantResource.Interfaces;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.MarkSession;
using mars_deletion_svc.MarkSession.Models;
using mars_deletion_svc.Services.Inerfaces;
using Moq;
using Xunit;

namespace UnitTests.MarkSession
{
    public class MarkSessionHandlerTests
    {
        [Fact]
        public async void DeleteMarkSessionAndDependantResources_StartBackgroundJob_ReturnsBackgroundJobId()
        {
            // Arrange
            var backgroundJobClient = new Mock<IBackgroundJobsHandler>();
            backgroundJobClient
                .Setup(m => m.CreateBackgroundJob(It.IsAny<Expression<Func<Task>>>()))
                .ReturnsAsync("12345");
            var markingServiceClient = new Mock<IMarkingServiceClient>();
            markingServiceClient
                .Setup(m => m.GetMarkSessionById(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<MarkSessionModel>());
            markingServiceClient
                .Setup(m => m.DeleteEmptyMarkingSession(It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var dependantResourceHandler = new Mock<IDependantResourceHandler>();
            dependantResourceHandler
                .Setup(m => m.DeleteDependantResourcesForMarkSession(It.IsAny<MarkSessionModel>()))
                .Returns(Task.CompletedTask);
            var loggerService = new Mock<ILoggerService>();
            var markSessionHandler = new MarkSessionHandler(
                backgroundJobClient.Object,
                markingServiceClient.Object,
                dependantResourceHandler.Object,
                loggerService.Object
            );

            // Act
            var backgroundJobId = await markSessionHandler.DeleteMarkSessionAndDependantResources(
                It.IsAny<MarkSessionModel>()
            );

            // Asset
            Assert.NotEmpty(backgroundJobId);
        }

        [Fact]
        public async void StartDeletionProcess_SuccessfulDeletion_NoExceptionThrown()
        {
            // Arrange
            var backgroundJobClient = new Mock<IBackgroundJobsHandler>();
            var markingServiceClient = new Mock<IMarkingServiceClient>();
            markingServiceClient
                .Setup(m => m.GetMarkSessionById(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<MarkSessionModel>());
            markingServiceClient
                .Setup(m => m.DeleteEmptyMarkingSession(It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var dependantResourceHandler = new Mock<IDependantResourceHandler>();
            dependantResourceHandler
                .Setup(m => m.DeleteDependantResourcesForMarkSession(It.IsAny<MarkSessionModel>()))
                .Returns(Task.CompletedTask);
            var loggerService = new Mock<ILoggerService>();
            var markSessionHandler = new MarkSessionHandler(
                backgroundJobClient.Object,
                markingServiceClient.Object,
                dependantResourceHandler.Object,
                loggerService.Object
            );
            Exception exception = null;

            try
            {
                // Act
                await markSessionHandler.StartDeletionProcess(It.IsAny<string>());
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Asset
            Assert.Null(exception);
        }

        [Fact]
        public async void StartDeletionProcess_MarkSessionDoesNotExistException_NoExceptionThrown()
        {
            // Arrange
            var backgroundJobClient = new Mock<IBackgroundJobsHandler>();
            var markingServiceClient = new Mock<IMarkingServiceClient>();
            markingServiceClient
                .Setup(m => m.GetMarkSessionById(It.IsAny<string>()))
                .ThrowsAsync(new MarkSessionDoesNotExistException(It.IsAny<string>()));
            markingServiceClient
                .Setup(m => m.DeleteEmptyMarkingSession(It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var dependantResourceHandler = new Mock<IDependantResourceHandler>();
            dependantResourceHandler
                .Setup(m => m.DeleteDependantResourcesForMarkSession(It.IsAny<MarkSessionModel>()))
                .Returns(Task.CompletedTask);
            var loggerService = new Mock<ILoggerService>();
            var markSessionHandler = new MarkSessionHandler(
                backgroundJobClient.Object,
                markingServiceClient.Object,
                dependantResourceHandler.Object,
                loggerService.Object
            );
            Exception exception = null;

            try
            {
                // Act
                await markSessionHandler.StartDeletionProcess(It.IsAny<string>());
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Asset
            Assert.Null(exception);
        }
    }
}