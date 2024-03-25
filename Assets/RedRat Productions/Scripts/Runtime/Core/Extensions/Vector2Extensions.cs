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
        public static Vector2 Round(this Vector2 vector2, int decimalPlaces = 2)
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
        /// Returns TRUE if the <see cref="Vector2"/> is zero.
        /// <p>Uses <see cref="Vector2"/></p>.Distance to measure sameness.
        /// </summary>
        /// <param name="vector2">The <see cref="Vector2"/> to measure.</param>
        /// <param name="distance">How far from each other can the vectors be, to be considered the same.</param>
        /// <returns>TRUE if vector is zero.</returns>
        public static bool IsZero(this Vector2 vector2, float distance = 0.01f)
        {
            return vector2.IsSameAs(Vector2.zero, distance);
        }
        
        /// <summary>
        /// Returns TRUE if 2 <see cref="Vector2"/>s are the same.
        /// <p>Uses <see cref="Vector2"/></p>.Distance to measure sameness.
        /// </summary>
        /// <param name="vector2">Vector2 A</param>
        /// <param name="other">Vector2 B</param>
        /// <param name="tolerance">How far from each other can the vectors be, to be considered the same.</param>
        /// <returns>TRUE if vectors are the same.</returns>
        public static bool IsSameAs(this Vector2 vector2, Vector2 other, float tolerance = 0.01f)
        {
            return (Vector2.Distance(vector2, other) > tolerance);
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