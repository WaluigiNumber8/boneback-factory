using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Holds data for a <see cref="InteractableEditorGrid"/> layer.
    /// </summary>
    [System.Serializable]
    public struct LayerInfo
    {
        [HorizontalGroup(MarginRight = 0.025f), HideLabel]
        public Image layer;
        [HorizontalGroup, LabelText("Out of Focus")]
        public Color outOfFocusColor;
    }
}