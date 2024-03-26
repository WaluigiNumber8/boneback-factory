namespace RedRats.Core
{
    /// <summary>
    /// Extension methods for the integer type.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Works like Mathf.Sign but with a 0.
        /// </summary>
        public static int Sign0(this int number)
        {
            return number == 0 ? 0 : number > 0 ? 1 : -1;
        }
        
        /// <summary>
        /// Converts the milliseconds to seconds.
        /// </summary>
        /// <param name="milliseconds">The milliseconds to convert.</param>
        /// <returns>Those milliseconds in seconds.</returns>
        public static float ToSeconds(this int milliseconds)
        {
            return (float) milliseconds / 1000;
        }
        
        /// <summary>
        /// Remaps a value from one range to another.
        /// </summary>
        /// <param name="value">The value to remap</param>
        /// <param name="from1">First Range min value.</param>
        /// <param name="to1">First range max value.</param>
        /// <param name="from2">Target range min value.</param>
        /// <param name="to2">target range max value.</param>
        /// <returns>The remapped value.</returns>
        public static int Remap(this int value, int from1, int from2, int to1, int to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

    }
}