using System.Linq;
using RedRats.Safety;
using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Extends the GameObject class with useful methods.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Deletes all of this Transform's children.
        /// </summary>
        /// <param name="transform"></param>
        public static void KillChildren(this Transform transform)
        {
            SafetyNet.EnsureIsNotNull(transform, "Transform to kill children of");
            
            if (transform.childCount <= 0) return;
            
            foreach (Transform child in transform)
            {
                if (child == transform) continue;
                Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Get the amount of active children this Transform has.
        /// </summary>
        /// <param name="transform">The object, who's children will be counted.</param>
        /// <returns>Children count.</returns>
        public static int ActiveChildCount(this Transform transform)
        {
            return transform.Cast<Transform>().Count(child => child.gameObject.activeSelf);
        }
    }
}