using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Contains access to Grid Previewer Tool Presets.
    /// </summary>
    [CreateAssetMenu(fileName = "New Grid Previewer Tool Info", menuName = EditorAssetPaths.AssetMenuEditor + "Grid Previewer Tool info", order = 20)]
    public class GridPreviewerToolInfoAsset : ScriptableObject
    {
        [SerializeField] private PreviewerToolDataInfo[] toolInfo;
        
        public PreviewerToolDataInfo[] ToolInfo { get => toolInfo; }
    }
}