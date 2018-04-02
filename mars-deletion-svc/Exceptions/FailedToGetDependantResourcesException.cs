using System;

namespace mars_deletion_svc.Exceptions
{
    public class FailedToGetDependantResourcesException : Exception
    {
        public FailedToGetDependantResourcesException(string message) : base(message)
        {
        }
    }
}