using System;

namespace RedRats.Safety
{
    public class CollectionNotFoundException : Exception
    {
        public CollectionNotFoundException()
        {
        }

        public CollectionNotFoundException(string message) : base(message)
        {
        }
    }
}