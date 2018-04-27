using System;

namespace mars_deletion_svc.Exceptions
{
    public class MarkSessionDoesNotExistException : Exception
    {
        public MarkSessionDoesNotExistException(
            string message
        ) : base(message)
        {
        }
    }
}