using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Holds data for a <see cref="InteractableEditorGridV2"/> layer.
    /// </summary>
    [System.Serializable]
    public struct LayerInfo
    {
        public Image layer;
        public Color outOfFocusColor;
    }
}