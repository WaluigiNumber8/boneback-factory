using System;

namespace BoubakProductions.Safety
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