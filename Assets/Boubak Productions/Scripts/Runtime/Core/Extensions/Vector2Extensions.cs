using UnityEngine;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Extension Methods for the <see cref="Vector2"/> type.
    /// </summary>
    public static class Vector2Extensions
    {
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