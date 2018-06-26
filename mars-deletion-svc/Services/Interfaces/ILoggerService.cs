using System;

namespace mars_deletion_svc.Services.Inerfaces
{
    public interface ILoggerService
    {
        void LogInfoEvent(
            string message
        );
        
        void LogInfoWithErrorEvent(
            string message,
            Exception exception
        );

        void LogBackgroundJobInfoEvent(
            string message
        );

        void LogBackgroundJobErrorEvent(
            Exception error
        );

        void LogStartupInfoEvent(
            string message
        );

        void LogStartupErrorEvent(
            Exception exception
        );
    }
}