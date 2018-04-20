using System;

namespace mars_deletion_svc.Exceptions
{
    public class FailedToGetMarkSessionException : Exception
    {
        public FailedToGetMarkSessionException(string message) : base(message)
        {
        }
    }
}