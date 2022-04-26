using UnityEngine;

namespace BoubakProductions.Core
{
    public static class GameObjectUtils
    {
        /// <summary>
        /// Checks if an object is part of layermask.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="layerMask">The layermask to search.</param>
        /// <returns>TRUE if object is in the layermask.</returns>
        public static bool IsInLayerMask(GameObject obj, LayerMask layerMask)
        {
            return ((layerMask.value & (1 << obj.layer)) > 0);
        }
    }
}