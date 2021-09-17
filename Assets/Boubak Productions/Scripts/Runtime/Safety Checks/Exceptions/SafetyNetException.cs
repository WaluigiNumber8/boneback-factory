
namespace BoubakProductions.Safety
{
    public class SafetyNetException : System.Exception
    {
        public SafetyNetException() { }

        public SafetyNetException(string message) : base(message) { }
    }
}