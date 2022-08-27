using Rogium.Systems.Toolbox;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Contains settable data for each tool.
    /// </summary>
    [System.Serializable]
    public struct PreviewerToolDataInfo
    {
        public ToolType tool;
        public bool isVisible;
        public bool permanentState;
        public bool followCursor;
        [PropertySpace]
        public bool autoMaterial;
        [HideIf(nameof(autoMaterial), true)]
        public Sprite customSprite;
        public Color customColor;
    }
}