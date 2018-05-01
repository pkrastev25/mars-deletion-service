using System;
using mars_deletion_svc.Services.Inerfaces;

namespace mars_deletion_svc.Services
{
    public class LoggerService : ILoggerService
    {
        public void LogDeleteEvent(
            string message
        )
        {
            Console.WriteLine($"{IncludeTimestamp()} [DELETE] {message}");
        }

        public void LogErrorEvent(
            Exception error
        )
        {
            Console.Error.WriteLine($"{IncludeTimestamp()} [ERROR] {error.Message}\n{error.StackTrace}");
        }

        private string IncludeTimestamp()
        {
            return $"- {DateTime.Now} -";
        }
    }
}