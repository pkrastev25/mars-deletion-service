using System;

namespace mars_deletion_svc.Services.Inerfaces
{
    public interface ILoggerService
    {
        void LogDeleteEvent(string message);

        void LogSkipEvent(string message);

        void LogWarningEvent(string message);

        void LogErrorEvent(Exception error);
    }
}