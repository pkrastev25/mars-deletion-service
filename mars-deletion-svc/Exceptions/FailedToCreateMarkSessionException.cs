using System;

namespace mars_deletion_svc.Exceptions
{
    public class FailedToCreateMarkSessionException : Exception
    {
        public FailedToCreateMarkSessionException(
            string message
        ) : base(message)
        {
        }
    }
}