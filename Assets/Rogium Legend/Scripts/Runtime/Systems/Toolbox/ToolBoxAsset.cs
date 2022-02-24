using System;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.GridSystem;
using Rogium.UserInterface.AssetSelection;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Houses all the tools used on an <see cref="InteractableEditorGrid"/>.
    /// </summary>
    public class ToolBoxAsset : IToolBox
    {
        public event Action<ToolType> OnSwitchTool;
        public event Action<string> OnChangePaletteValue; 
        
        private readonly BrushTool<string> toolBrush;
        private readonly EraserTool<string> toolEraser;
        private readonly BucketTool<string> toolBucket;
        private readonly PickerTool<string> toolPicker;

        private InteractableEditorGrid UIGrid;
        private Sprite currentSprite;
        private ITool<string> currentTool;
        private ToolType currentToolType;

        public ToolBoxAsset(InteractableEditorGrid UIGrid)
        {
            toolBrush = new BrushTool<string>();
            toolEraser = new EraserTool<string>(EditorDefaults.EmptyAssetID);
            toolBucket = new BucketTool<string>();
            toolPicker = new PickerTool<string>();

            toolPicker.OnPickValue += ctx => OnChangePaletteValue?.Invoke(ctx);
            
            this.UIGrid = UIGrid;
            currentTool = toolBrush;
        }
        
        /// <summary>
        /// Applies the effect of the current tool on a specific grid position.
        /// </summary>
        /// <param name="grid">The grid to affect.</param>
        /// <param name="position">The position to start with.</param>
        /// <param name="value">The value to set.</param>
        public void ApplyCurrent(ObjectGrid<string> grid, Vector2Int position, AssetSlot value)
        {
            currentSprite = value.Asset.Icon;
            currentTool.ApplyEffect(grid, position, value.Asset.ID, WhenDrawOnUIGrid);
        }

        public void SwitchTool(ToolType tool)
        {
            if (currentToolType == tool) return;
            currentTool = tool switch
            {
                ToolType.Brush => toolBrush,
                ToolType.Eraser => toolEraser,
                ToolType.Bucket => toolBucket,
                ToolType.ColorPicker => toolPicker,
                _ => throw new InvalidOperationException("Unknown or not yet supported Tool Type.")
            };
            currentToolType = tool;
            OnSwitchTool?.Invoke(tool);
        }

        public void WhenDrawOnUIGrid(Vector2Int position, bool useEmpty)
        {
            Sprite value = (useEmpty) ? null : currentSprite;
            UIGrid.UpdateCellSprite(position, value);
        }
        
    }
}