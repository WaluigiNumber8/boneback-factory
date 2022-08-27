using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Extension methods for the <see cref="Quaternion"/> type.
    /// </summary>
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Rounds a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="quaternion">The <see cref="Quaternion"/> to round.</param>
        /// <param name="decimalPlaces">Amount of decimal places.</param>
        /// <returns>The rounded <see cref="Quaternion"/>.</returns>
        public static Quaternion Round(this Quaternion quaternion, int decimalPlaces = 0)
        {
            float multiplier = 1;
            for (int i = 0; i < decimalPlaces; i++)
            {
                multiplier *= 10f;
            }
            
            return new Quaternion(
                Mathf.Round(quaternion.x * multiplier) / multiplier,
                Mathf.Round(quaternion.y * multiplier) / multiplier,
                Mathf.Round(quaternion.z * multiplier) / multiplier,
                Mathf.Round(quaternion.w * multiplier) / multiplier);
        }
    }
}