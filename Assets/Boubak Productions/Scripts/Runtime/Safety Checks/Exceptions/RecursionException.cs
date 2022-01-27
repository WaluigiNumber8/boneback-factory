using System;

namespace BoubakProductions.Safety
{
    public class RecursionException : Exception
    {
        public RecursionException() { }

        public RecursionException(string message) : base(message) { }
    }
}