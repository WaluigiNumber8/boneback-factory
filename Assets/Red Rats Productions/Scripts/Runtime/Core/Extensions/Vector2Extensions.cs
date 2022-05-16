using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Extension Methods for the <see cref="Vector2"/> type.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Rounds a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="vector2">The <see cref="Vector2"/> to round.</param>
        /// <param name="decimalPlaces">Amount of decimal places.</param>
        /// <returns>The rounded <see cref="Vector2"/>.</returns>
        public static Vector2 Round(this Vector2 vector2, int decimalPlaces = 0)
        {
            float multiplier = 1;
            for (int i = 0; i < decimalPlaces; i++)
            {
                multiplier *= 10f;
            }

            return new Vector2(Mathf.Round(vector2.x * multiplier) / multiplier,
                               Mathf.Round(vector2.y * multiplier) / multiplier);
        }
        
        /// <summary>
        /// Returns distance between 2 <see cref="Vector2"/>s.
        /// </summary>
        /// <param name="vector2">The first <see cref="Vector2"/>.</param>
        /// <param name="other">The <see cref="Vector2"/> to compare to.</param>
        /// <returns>The distance.</returns>
        public static float DistanceTo(this Vector2 vector2, Vector2 other)
        {
            return Vector2.Distance(vector2, other);
        }
        
        /// <summary>
        /// Returns a rotated variant of the vector.
        /// </summary>
        /// <param name="vector">The vector to rotate.</param>
        /// <param name="degrees">How much to rotate in degrees.</param>
        /// <returns>A rotated vector.</returns>
        public static Vector2 Rotate(this Vector2 vector, float degrees) {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
            float tx = vector.x;
            float ty = vector.y;
            vector.x = (cos * tx) - (sin * ty);
            vector.y = (sin * tx) + (cos * ty);
            return vector;
        }
        
        /// <summary>
        /// Absolutes the vector.
        /// </summary>
        /// <param name="vector">The vector to absolute.</param>
        /// <returns>The absolutated vector.</returns>
        public static Vector2 Abs(this Vector2 vector)
        {
            return new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
        }
    }
}