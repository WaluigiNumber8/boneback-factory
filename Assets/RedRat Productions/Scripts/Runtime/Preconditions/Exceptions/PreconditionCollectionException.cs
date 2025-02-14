using System;

namespace RedRats.Safety
{
    public class PreconditionCollectionException : Exception
    {
        public PreconditionCollectionException()
        {
        }

        public PreconditionCollectionException(string message) : base(message)
        {
        }
    }
}