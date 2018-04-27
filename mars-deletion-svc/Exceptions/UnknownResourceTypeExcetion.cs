using System;

namespace mars_deletion_svc.Exceptions
{
    public class UnknownResourceTypeExcetion : Exception
    {
        public UnknownResourceTypeExcetion(
            string message
        ) : base(message)
        {
        }
    }
}