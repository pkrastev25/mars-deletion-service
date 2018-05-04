using System;

namespace mars_deletion_svc.Exceptions
{
    public class BackgroundJobDoesNotExistException : Exception
    {
        public BackgroundJobDoesNotExistException(
            string message
        ) : base(message)
        {
        }
        
        public BackgroundJobDoesNotExistException(
            string message,
            Exception exception
        ) : base(message, exception)
        {
        }
    }
}