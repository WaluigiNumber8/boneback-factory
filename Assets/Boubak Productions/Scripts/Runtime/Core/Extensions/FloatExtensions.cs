namespace BoubakProductions.Core
{
    /// <summary>
    /// Extension methods for the float type.
    /// </summary>
    public static class FloatExtensions
    {
        /// <summary>
        /// Works like Mathf.Sign but with a 0.
        /// </summary>
        public static float Sign0(this float number)
        {
            return number == 0 ? 0 : number > 0 ? 1 : -1;
        }

        /// <summary>
        /// Converts seconds to milliseconds.
        /// </summary>
        /// <param name="seconds">The seconds to delay.</param>
        /// <returns>Seconds in milliseconds.</returns>
        public static int ToMilliseconds(this float seconds)
        {
            return (int) seconds * 1000;
        }
    }
}