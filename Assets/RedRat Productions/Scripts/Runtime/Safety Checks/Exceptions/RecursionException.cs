using System;

namespace RedRats.Safety
{
    public class RecursionException : Exception
    {
        public RecursionException() { }

        public RecursionException(string message) : base(message) { }
    }
}