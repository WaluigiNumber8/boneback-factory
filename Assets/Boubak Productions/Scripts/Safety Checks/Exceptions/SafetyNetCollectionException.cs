using System;
using System.Runtime.Serialization;

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