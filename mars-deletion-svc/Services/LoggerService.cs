using System;
using mars_deletion_svc.Services.Inerfaces;

namespace mars_deletion_svc.Services
{
    public class LoggerService : ILoggerService
    {
        public void LogInfoEvent(
            string message
        )
        {
            Console.WriteLine($"{IncludeTimestamp()} [INFO] {message}");
        }

        public void LogInfoWithErrorEvent(
            string message,
            Exception error
        )
        {
            var errorMessage = error.InnerException == null
                ? $"[ERROR] {error.Message}\n{error.StackTrace}"
                : $"[ERROR] {error.Message} {error.InnerException.Message}\n{error.StackTrace}";
            Console.Error.WriteLine($"{IncludeTimestamp()} [INFO] {message}\n{errorMessage}");
        }

        public void LogBackgroundJobInfoEvent(
            string message
        )
        {
            Console.WriteLine($"{IncludeTimestamp()} [JOB][INFO] {message}");
        }

        public void LogBackgroundJobErrorEvent(
            Exception error
        )
        {
            Console.Error.WriteLine(
                error.InnerException == null
                    ? $"{IncludeTimestamp()} [JOB][ERROR] {error.Message}\n{error.StackTrace}"
                    : $"{IncludeTimestamp()} [JOB][ERROR] {error.Message} {error.InnerException.Message}\n{error.StackTrace}"
            );
        }

        public void LogStartupInfoEvent(
            string message
        )
        {
            Console.WriteLine($"{IncludeTimestamp()} [STARTUP][INFO] {message}");
        }

        public void LogStartupErrorEvent(
            Exception exception
        )
        {
            Console.Error.WriteLine(
                exception.InnerException == null
                    ? $"{IncludeTimestamp()} [STARTUP][ERROR] {exception.Message}\n{exception.StackTrace}"
                    : $"{IncludeTimestamp()} [STARTUP][ERROR] {exception.Message} {exception.InnerException.Message}\n{exception.StackTrace}"
            );
        }

        private string IncludeTimestamp()
        {
            return $"- {DateTime.Now} -";
        }
    }
}