using System.Collections;
using UnityEngine;

namespace RogiumLegend.Global.SafetyChecks
{
    public class SafetyNetException : System.Exception
    {
        public SafetyNetException() { }

        public SafetyNetException(string message) : base(message) { }
    }
}