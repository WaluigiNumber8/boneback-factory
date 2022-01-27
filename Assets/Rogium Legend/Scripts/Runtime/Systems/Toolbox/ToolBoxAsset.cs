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
        private readonly BrushTool<string> toolBrush;
        private readonly EraserTool<string> toolEraser;
        private readonly BucketTool<string> toolBucket;

        private InteractableEditorGrid UIGrid;
        private Sprite currentSprite;
        private ITool<string> currentTool;

        public ToolBoxAsset(InteractableEditorGrid UIGrid)
        {
            toolBrush = new BrushTool<string>();
            toolEraser = new EraserTool<string>(EditorDefaults.EmptyAssetID);
            toolBucket = new BucketTool<string>();

            this.UIGrid = UIGrid;
            SwitchTool(ToolType.Brush);
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
            switch (tool)
            {
                case ToolType.Brush:
                    currentTool = toolBrush;
                    break;
                case ToolType.Eraser:
                    currentTool = toolEraser;
                    break;
                case ToolType.Bucket:
                    currentTool = toolBucket;
                    break;
                case ToolType.ColorPicker:
                default:
                    throw new InvalidOperationException("Unknown or not yet supported Tool Type.");
            }
        }

        public void WhenDrawOnUIGrid(Vector2Int position, bool useEmpty)
        {
            Sprite value = (useEmpty) ? EditorDefaults.EmptyGridSprite : currentSprite;
            UIGrid.UpdateCellSprite(position, value);
        }
        
    }
}