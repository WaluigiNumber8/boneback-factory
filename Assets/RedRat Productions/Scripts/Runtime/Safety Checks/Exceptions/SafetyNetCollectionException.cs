using System;

namespace RedRats.Safety
{
    public class SafetyNetCollectionException : Exception
    {
        public SafetyNetCollectionException()
        {
        }

        public SafetyNetCollectionException(string message) : base(message)
        {
        }
    }
}