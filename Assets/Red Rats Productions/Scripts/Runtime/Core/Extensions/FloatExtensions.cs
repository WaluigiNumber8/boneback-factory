using UnityEngine;

namespace RedRats.Core
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
        
        /// <summary>
        /// Round a float value to a specific amount of decimal places.
        /// </summary>
        /// <param name="value">The value to round.</param>
        /// <param name="decimals">Amount of decimals.</param>
        /// <returns></returns>
        public static float Round(this float value, int decimals)
        {
            float mult = Mathf.Pow(10.0f, decimals);
            return Mathf.Round(value * mult) / mult;
        }
    }
}