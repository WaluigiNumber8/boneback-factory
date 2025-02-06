using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Helper methods for UI Properties.
    /// </summary>
    public static class UIPropertyUtils
    {
        public static void ReleaseAllProperties(this Transform content, bool includeSelf = false)
        {
            InteractablePropertyBase[] p = content.GetComponentsInChildren<InteractablePropertyBase>();
            foreach (InteractablePropertyBase property in p)
            {
                if (includeSelf == false && property.transform == content) continue;
                property.ReleaseToPool();
            }
        }
    }
}