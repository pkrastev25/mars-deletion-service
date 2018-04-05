using System;
using mars_deletion_svc.Services.Inerfaces;

namespace mars_deletion_svc.Services
{
    public class LoggerService : ILoggerService
    {
        public void LogDeleteEvent(string message)
        {
            Console.WriteLine($"[DELETE] {message}");
        }

        public void LogSkipEvent(string message)
        {
            Console.WriteLine($"[SKIP] {message}");
        }

        public void LogWarningEvent(string message)
        {
            Console.WriteLine($"[WARNING] {message}");
        }

        public void LogErrorEvent(Exception error)
        {
            Console.Error.WriteLine($"[ERROR] {error.Message}");
            Console.Error.WriteLine(error.StackTrace);
        }
    }
}