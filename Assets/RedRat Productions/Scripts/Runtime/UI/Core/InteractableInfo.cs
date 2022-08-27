using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.UI.Core
{
    /// <summary>
    /// Contains UI data for an interactable.
    /// </summary>
    [System.Serializable]
    public struct InteractableInfo
    {
        [HorizontalGroup("ButtonSprites"), PreviewField(60), HideLabel]
        public Sprite normal;
        [HorizontalGroup("ButtonSprites"), PreviewField(60), HideLabel]
        public Sprite highlighted;
        [HorizontalGroup("ButtonSprites"), PreviewField(60), HideLabel]
        public Sprite pressed;
        [HorizontalGroup("ButtonSprites"), PreviewField(60), HideLabel]
        public Sprite selected;
        [HorizontalGroup("ButtonSprites"), PreviewField(60), HideLabel]
        public Sprite disabled;
    }
}