using System;

namespace mars_deletion_svc.Services.Inerfaces
{
    public interface ILoggerService
    {
        void LogInfoEvent(
            double performanceMetricInSeconds,
            string message
        );
        
        void LogInfoWithErrorEvent(
            double performanceMetricInSeconds,
            string message,
            Exception exception
        );

        void LogBackgroundJobInfoEvent(
            string message
        );
        
        void LogBackgroundJobInfoEvent(
            double performanceMetricInSeconds,
            string message
        );

        void LogBackgroundJobErrorEvent(
            double performanceMetricInSeconds,
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