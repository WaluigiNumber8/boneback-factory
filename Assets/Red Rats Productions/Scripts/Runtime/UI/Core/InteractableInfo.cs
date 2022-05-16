using UnityEngine;

namespace RedRats.UI.Core
{
    /// <summary>
    /// Contains UI data for an interactable.
    /// </summary>
    [System.Serializable]
    public struct InteractableInfo
    {
        public Sprite normal;
        public Sprite highlighted;
        public Sprite pressed;
        public Sprite selected;
        public Sprite disabled;
    }
}