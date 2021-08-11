using System;
using System.Runtime.Serialization;

namespace RogiumLegend.Global.SafetyChecks
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