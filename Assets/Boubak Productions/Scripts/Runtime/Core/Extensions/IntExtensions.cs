using UnityEngine;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Extension methods for the integer type.
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// Converts the milliseconds to seconds.
        /// </summary>
        /// <param name="milliseconds">The milliseconds to convert.</param>
        /// <returns>Those milliseconds in seconds.</returns>
        public static float ToSeconds(this int milliseconds)
        {
            return (float) milliseconds / 1000;
        }

    }
}