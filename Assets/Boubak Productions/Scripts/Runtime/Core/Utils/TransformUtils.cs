using UnityEngine;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Utility methods for working with transforms.
    /// </summary>
    public static class TransformUtils
    {
        /// <summary>
        /// Rotate a transform towards a specific direction in 2D world space.
        /// </summary>
        /// <param name="transform">The transform to rotate.</param>
        /// <param name="direction">The direction to rotate to.</param>
        /// <param name="offsetAngle">The offset angle of the rotation</param>
        /// <param name="space">Te space the rotation will happen.</param>
        public static void SetRotation2D(Transform transform, Vector2 direction, int offsetAngle = 0, Space space = Space.World)
        {
            if (direction == Vector2.zero) return;
            
            float angle = Vector2.SignedAngle(Vector2.up, direction) + offsetAngle;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
    }
}