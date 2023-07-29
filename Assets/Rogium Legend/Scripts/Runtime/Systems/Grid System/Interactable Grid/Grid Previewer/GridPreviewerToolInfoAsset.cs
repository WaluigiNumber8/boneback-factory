using Rogium.Editors.Core.Defaults;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Contains access to Grid Previewer Tool Presets.
    /// </summary>
    [CreateAssetMenu(fileName = "New Grid Previewer Tool Info", menuName = EditorConstants.AssetMenuEditor + "Grid Previewer Tool info", order = 20)]
    [InlineEditor]
    public class GridPreviewerToolInfoAsset : ScriptableObject
    {
        [SerializeField] private PreviewerToolDataInfo[] toolInfo;
        
        public PreviewerToolDataInfo[] ToolInfo { get => toolInfo; }
    }
}