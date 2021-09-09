using UnityEngine;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Extends the GameObject class with useful methods.
    /// </summary>
    public static class GameObjectExtensions
    {
        public static void KillChildren(this GameObject gObject)
        {
            foreach (Transform child in gObject.transform)
            {
                if (child == gObject.transform) continue;
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}