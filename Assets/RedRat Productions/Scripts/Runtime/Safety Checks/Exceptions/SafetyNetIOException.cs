namespace RedRats.Safety
{
    public class SafetyNetIOException : SafetyNetException
    {
        public SafetyNetIOException() : base() { }

        public SafetyNetIOException(string message) : base(message) { }
    }
}