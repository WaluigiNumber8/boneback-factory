namespace RedRats.Core
{
    /// <summary>
    /// Contains various utilities for working with color.
    /// </summary>
    public static class ColorUtils
    {
        /// <summary>
        /// Converts a 0-255 value to 0-1.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A converted value.</returns>
        public static float ConvertTo01(float value) => value / 255;

        /// <summary>
        /// Converts a 0-1 value to 0-255.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value.</returns>
        public static int ConvertTo255(float value) => (int)(value * 255);
    }
}