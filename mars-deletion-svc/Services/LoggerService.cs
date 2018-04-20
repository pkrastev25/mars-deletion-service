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

        public void LogErrorEvent(Exception error)
        {
            Console.Error.WriteLine($"[ERROR] {error.Message}");
            Console.Error.WriteLine(error.StackTrace);
        }
    }
}