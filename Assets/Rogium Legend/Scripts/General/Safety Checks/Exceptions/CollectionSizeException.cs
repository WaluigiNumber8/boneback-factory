using System;
using System.Runtime.Serialization;

namespace RogiumLegend.Global.SafetyChecks
{
    public class CollectionSizeException : Exception
    {
        public CollectionSizeException()
        {
        }

        public CollectionSizeException(string message) : base(message)
        {
        }
    }
}