using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Contains various utilities for working with floats.
    /// </summary>
    public static class FloatUtils
    {
        /// <summary>
        /// Checks if 2 floats are the same (with tolerance.)
        /// </summary>
        /// <param name="a">Float A.</param>
        /// <param name="b">Float B.</param>
        /// <param name="t">Tolerance value.</param>
        /// <returns>TRUE if values are within the tolerance offset.</returns>
        public static bool AreEqual(float a, float b, float t)
        {
            return (Mathf.Abs(b - a) < t);
        }
    }
}