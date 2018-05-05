using System;
using mars_deletion_svc.Services;
using UnitTests._HelperMocks;
using Xunit;

namespace UnitTests.Services
{
    public class LoggerServiceTests
    {
        [Fact]
        public void LogDeleteEvent_StringMessage_NoExceptionThrown()
        {
            // Arrange
            var messageToWriteToConsole = "Some message to the console!";
            var loggerService = new LoggerService();

            using (var consoleOutputHelperMocks = new ConsoleHelperMocks(Console.Out))
            {
                // Act
                loggerService.LogDeleteEvent(messageToWriteToConsole);

                // Asser
                Assert.Contains(messageToWriteToConsole, consoleOutputHelperMocks.GetOuput());
            }
        }

        [Fact]
        public void LogErrorEvent_StringMessage_WritesToConsole()
        {
            // Arrange
            var messageToWriteToConsole = "Some error has occurred!";
            var loggerService = new LoggerService();

            using (var consoleOutputHelperMocks = new ConsoleHelperMocks(Console.Error))
            {
                // Act
                loggerService.LogErrorEvent(new Exception(messageToWriteToConsole));

                // Asser
                Assert.Contains(messageToWriteToConsole, consoleOutputHelperMocks.GetOuput());
            }
        }
    }
}