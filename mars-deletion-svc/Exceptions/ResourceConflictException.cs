using System;

namespace mars_deletion_svc.Exceptions
{
    public class ResourceConflictException : Exception
    {
        public ResourceConflictException(
            string message
        ) : base(message)
        {
        }
    }
}