using RedRats.Safety;

namespace RedRats.Core
{
    /// <summary>
    /// Contains various utilities for working with strings.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Grabs a number from a string as an INT.
        /// </summary>
        /// <param name="s">The string to grab the number from.</param>
        /// <param name="pos">Starting position of the number.</param>
        /// <param name="chars">Amount of chars taken by the number.</param>
        /// <returns>The grabbed number.</returns>
        public static int GrabIntFrom(string s, int pos, int chars = 1)
        {
            SafetyNet.EnsureStringNotNullOrEmpty(s, "string to convert");
            SafetyNet.EnsureIntIsInRange(pos, 0, s.Length, "number's position");
            SafetyNet.EnsureIntIsInRange(chars, 0, s.Length - pos, "number's position");
            return int.Parse(s.Substring(pos, chars));
        }
        
        /// <summary>
        /// Tries to grab a number from a string as an INT.
        /// </summary>
        /// <param name="s">The string to grab the number from.</param>
        /// <param name="pos">Starting position of the number.</param>
        /// <param name="chars">Amount of chars taken by the number.</param>
        /// <returns>TRUE if it was able to grab a number.</returns>
        public static bool TryGrabIntFrom(string s, out int num, int pos, int chars = 1)
        {
            SafetyNet.EnsureStringNotNullOrEmpty(s, "string to convert");
            SafetyNet.EnsureIntIsInRange(pos, 0, s.Length, "number's position");
            SafetyNet.EnsureIntIsInRange(chars, 0, s.Length - pos, "number's position");
            return int.TryParse(s.Substring(pos, chars), out num);
        }
    }
}