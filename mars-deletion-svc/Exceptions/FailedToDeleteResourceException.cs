using System;

namespace mars_deletion_svc.Exceptions
{
    public class FailedToDeleteResourceException : Exception
    {
        public FailedToDeleteResourceException(string message) : base(message)
        {
        }
    }
}