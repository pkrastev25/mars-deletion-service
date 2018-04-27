using System;

namespace mars_deletion_svc.Exceptions
{
    public class FailedToGetResourceException : Exception
    {
        public FailedToGetResourceException(
            string message
        ) : base(message)
        {
        }
    }
}