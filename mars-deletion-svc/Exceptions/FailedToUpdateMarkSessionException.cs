using System;

namespace mars_deletion_svc.Exceptions
{
    public class FailedToUpdateMarkSessionException : Exception
    {
        public FailedToUpdateMarkSessionException(
            string message
        ) : base(message)
        {
        }
    }
}