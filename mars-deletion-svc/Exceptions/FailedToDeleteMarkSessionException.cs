using System;

namespace mars_deletion_svc.Exceptions
{
    public class FailedToDeleteMarkSessionException : Exception
    {
        public FailedToDeleteMarkSessionException(string message) : base(message)
        {
        }
    }
}