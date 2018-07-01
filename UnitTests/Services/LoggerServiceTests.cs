using System;
using mars_deletion_svc.Services;
using UnitTests._HelperMocks;
using Xunit;

namespace UnitTests.Services
{
    public class LoggerServiceTests
    {
        [Fact]
        public void LogInfoEvent_StringMessage_StringMessageIsWrittenToTheConsole()
        {
            // Arrange
            var messageToWriteToConsole = "Some message to the console!";
            var loggerService = new LoggerService();

            using (var consoleOutputHelperMocks = new ConsoleHelperMocks(Console.Out))
            {
                // Act
                loggerService.LogInfoEvent(10, messageToWriteToConsole);

                // Assert
                Assert.Contains(messageToWriteToConsole, consoleOutputHelperMocks.GetOuput());
            }
        }

        [Fact]
        public void LogInforWithErrorEvent_StringMessage_StringMessageIsWrittenToTheConsole()
        {
            // Arrange
            var messageToWriteToConsole = "Some message to the console!";
            var exception = new Exception("The cause of the error!");
            var loggerService = new LoggerService();

            using (var consoleOutputHelperMocks = new ConsoleHelperMocks(Console.Error))
            {
                // Act
                loggerService.LogInfoWithErrorEvent(10, messageToWriteToConsole, exception);

                // Assert
                Assert.Contains(messageToWriteToConsole, consoleOutputHelperMocks.GetOuput());
            }
        }

        [Fact]
        public void LogBackgroundJobInfoEvent_StringMessage_StringMessageIsWrittenToTheConsole()
        {
            // Arrange
            var messageToWriteToConsole = "Some message to the console!";
            var loggerService = new LoggerService();

            using (var consoleOutputHelperMocks = new ConsoleHelperMocks(Console.Out))
            {
                // Act
                loggerService.LogBackgroundJobInfoEvent(messageToWriteToConsole);

                // Assert
                Assert.Contains(messageToWriteToConsole, consoleOutputHelperMocks.GetOuput());
            }
        }

        [Fact]
        public void LogBackgroundJobInfoEvent_StringMessageWithPerformanceMetric_StringMessageIsWrittenToTheConsole()
        {
            // Arrange
            var messageToWriteToConsole = "Some message to the console!";
            var loggerService = new LoggerService();

            using (var consoleOutputHelperMocks = new ConsoleHelperMocks(Console.Out))
            {
                // Act
                loggerService.LogBackgroundJobInfoEvent(10, messageToWriteToConsole);

                // Assert
                Assert.Contains(messageToWriteToConsole, consoleOutputHelperMocks.GetOuput());
            }
        }

        [Fact]
        public void LogBackgroundJobErrorEvent_StringMessage_StringMessageIsWrittenToTheConsole()
        {
            // Arrange
            var messageToWriteToConsole = "Some message to the console!";
            var loggerService = new LoggerService();

            using (var consoleOutputHelperMocks = new ConsoleHelperMocks(Console.Error))
            {
                // Act
                loggerService.LogBackgroundJobErrorEvent(10, new Exception(messageToWriteToConsole));

                // Assert
                Assert.Contains(messageToWriteToConsole, consoleOutputHelperMocks.GetOuput());
            }
        }

        [Fact]
        public void LogStartupInfoEvent_StringMessage_StringMessageIsWrittenToTheConsole()
        {
            // Arrange
            var messageToWriteToConsole = "Some message to the console!";
            var loggerService = new LoggerService();

            using (var consoleOutputHelperMocks = new ConsoleHelperMocks(Console.Out))
            {
                // Act
                loggerService.LogStartupInfoEvent(messageToWriteToConsole);

                // Assert
                Assert.Contains(messageToWriteToConsole, consoleOutputHelperMocks.GetOuput());
            }
        }

        [Fact]
        public void LogStartupErrorEvent_StringMessage_StringMessageIsWrittenToTheConsole()
        {
            // Arrange
            var messageToWriteToConsole = "Some message to the console!";
            var loggerService = new LoggerService();

            using (var consoleOutputHelperMocks = new ConsoleHelperMocks(Console.Error))
            {
                // Act
                loggerService.LogStartupErrorEvent(new Exception(messageToWriteToConsole));

                // Assert
                Assert.Contains(messageToWriteToConsole, consoleOutputHelperMocks.GetOuput());
            }
        }
    }
}