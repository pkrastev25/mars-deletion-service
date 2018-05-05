using System;
using System.Collections.Generic;
using System.Threading;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.MarkSession.Interfaces;
using mars_deletion_svc.MarkSession.Models;
using mars_deletion_svc.Services;
using mars_deletion_svc.Services.Inerfaces;
using Moq;
using UnitTests._DataMocks;
using Xunit;

namespace UnitTests.Services
{
    public class HostedStartupServiceTests
    {
        [Fact]
        public async void StartAsync_FalseCancellationToken_NoExceptionThrown()
        {
            // Arrange
            var markingServiceClient = new Mock<IMarkingServiceClient>();
            markingServiceClient
                .Setup(m => m.GetMarkSessionsByMarkSessionType(It.IsAny<string>()))
                .ReturnsAsync(
                    new List<MarkSessionModel>
                    {
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel()
                    }
                );
            var markSessionHandler = new Mock<IMarkSessionHandler>();
            markSessionHandler
                .Setup(m => m.DeleteMarkSessionAndDependantResources(It.IsAny<MarkSessionModel>()))
                .ReturnsAsync("12345");
            var loggerService = new Mock<ILoggerService>();
            var hostedStartupService = new HostedStartupService(
                markingServiceClient.Object,
                markSessionHandler.Object,
                loggerService.Object
            );
            var cancellationToken = new CancellationToken(false);
            Exception exception = null;

            try
            {
                // Act
                await hostedStartupService.StartAsync(cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async void StartAsync_TrueCancellationToken_NoExceptionThrown()
        {
            // Arrange
            var markingServiceClient = new Mock<IMarkingServiceClient>();
            markingServiceClient
                .Setup(m => m.GetMarkSessionsByMarkSessionType(It.IsAny<string>()))
                .ReturnsAsync(
                    new List<MarkSessionModel>
                    {
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel()
                    }
                );
            var markSessionHandler = new Mock<IMarkSessionHandler>();
            markSessionHandler
                .Setup(m => m.DeleteMarkSessionAndDependantResources(It.IsAny<MarkSessionModel>()))
                .ReturnsAsync("12345");
            var loggerService = new Mock<ILoggerService>();
            var hostedStartupService = new HostedStartupService(
                markingServiceClient.Object,
                markSessionHandler.Object,
                loggerService.Object
            );
            var cancellationToken = new CancellationToken(true);
            Exception exception = null;

            try
            {
                // Act
                await hostedStartupService.StartAsync(cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }
        
        [Fact]
        public async void StopAsync_FalseCancellationToken_NoExceptionThrown()
        {
            // Arrange
            var markingServiceClient = new Mock<IMarkingServiceClient>();
            markingServiceClient
                .Setup(m => m.GetMarkSessionsByMarkSessionType(It.IsAny<string>()))
                .ReturnsAsync(
                    new List<MarkSessionModel>
                    {
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel()
                    }
                );
            var markSessionHandler = new Mock<IMarkSessionHandler>();
            markSessionHandler
                .Setup(m => m.DeleteMarkSessionAndDependantResources(It.IsAny<MarkSessionModel>()))
                .ReturnsAsync("12345");
            var loggerService = new Mock<ILoggerService>();
            var hostedStartupService = new HostedStartupService(
                markingServiceClient.Object,
                markSessionHandler.Object,
                loggerService.Object
            );
            var cancellationToken = new CancellationToken(false);
            Exception exception = null;

            try
            {
                // Act
                await hostedStartupService.StopAsync(cancellationToken);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.Null(exception);
        }
        
        [Fact]
        public async void StopAsync_TrueCancellationToken_NoExceptionThrown()
        {
            // Arrange
            var markingServiceClient = new Mock<IMarkingServiceClient>();
            markingServiceClient
                .Setup(m => m.GetMarkSessionsByMarkSessionType(It.IsAny<string>()))
                .ReturnsAsync(
                    new List<MarkSessionModel>
                    {
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel(),
                        MarkSessionModelDataMocks.MockMarkSessionModel()
                    }
                );
            var markSessionHandler = new Mock<IMarkSessionHandler>();
            markSessionHandler
                .Setup(m => m.DeleteMarkSessionAndDependantResources(It.IsAny<MarkSessionModel>()))
                .ReturnsAsync("12345");
            var loggerService = new Mock<ILoggerService>();
            var hostedStartupService = new HostedStartupService(
                markingServiceClient.Object,
                markSessionHandler.Object,
                loggerService.Object
            );
            var cancellationToken = new CancellationToken(true);
            Exception exception = null;

            try
            {
                // Act
                await hostedStartupService.StopAsync(cancellationToken);
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