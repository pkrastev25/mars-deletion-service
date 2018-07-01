using System;
using mars_deletion_svc.Services.Inerfaces;

namespace mars_deletion_svc.Services
{
    public class LoggerService : ILoggerService
    {
        public void LogInfoEvent(
            double performanceMetricInSeconds,
            string message
        )
        {
            Console.WriteLine($"{IncludeTimestamp()} |{performanceMetricInSeconds}s| [INFO] {message}");
        }

        public void LogInfoWithErrorEvent(
            double performanceMetricInSeconds,
            string message,
            Exception error
        )
        {
            var errorMessage = error.InnerException == null
                ? $"[ERROR] {error.Message}\n{error.StackTrace}"
                : $"[ERROR] {error.Message} {error.InnerException.Message}\n{error.StackTrace}";
            Console.Error.WriteLine($"{IncludeTimestamp()} |{performanceMetricInSeconds}s| [INFO] {message}\n{errorMessage}");
        }

        public void LogBackgroundJobInfoEvent(
            double performanceMetricInSeconds,
            string message
        )
        {
            Console.WriteLine($"{IncludeTimestamp()} |{performanceMetricInSeconds}s| [JOB][INFO] {message}");
        }
        
        public void LogBackgroundJobInfoEvent(
            string message
        )
        {
            Console.WriteLine($"{IncludeTimestamp()} [JOB][INFO] {message}");
        }

        public void LogBackgroundJobErrorEvent(
            double performanceMetricInSeconds,
            Exception error
        )
        {
            Console.Error.WriteLine(
                error.InnerException == null
                    ? $"{IncludeTimestamp()} |{performanceMetricInSeconds}s| [JOB][ERROR] {error.Message}\n{error.StackTrace}"
                    : $"{IncludeTimestamp()} |{performanceMetricInSeconds}s| [JOB][ERROR] {error.Message} {error.InnerException.Message}\n{error.StackTrace}"
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