using UnityEngine;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Extension Methods for the <see cref="Vector3"/> type.
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Rounds a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="vector3">The <see cref="Vector3"/> to round.</param>
        /// <param name="decimalPlaces">Amount of decimal places.</param>
        /// <returns>The rounded <see cref="Vector3"/>.</returns>
        public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 0)
        {
            float multiplier = 1;
            for (int i = 0; i < decimalPlaces; i++)
            {
                multiplier *= 10f;
            }
            return new Vector3(
                Mathf.Round(vector3.x * multiplier) / multiplier,
                Mathf.Round(vector3.y * multiplier) / multiplier,
                Mathf.Round(vector3.z * multiplier) / multiplier);
        }
    }
}