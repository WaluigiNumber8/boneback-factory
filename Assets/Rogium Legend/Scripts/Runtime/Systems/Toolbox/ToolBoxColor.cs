using System;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Palettes;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Houses all the tools used on an <see cref="InteractableEditorGrid"/>.
    /// </summary>
    public class ToolBoxColor : IToolBox
    {
        private readonly BrushTool<int> toolBrush;
        private readonly EraserTool<int> toolEraser;
        private readonly BucketTool<int> toolBucket;

        private InteractableEditorGrid UIGrid;
        private Color currentColor;
        private ITool<int> currentTool;

        public ToolBoxColor(InteractableEditorGrid UIGrid)
        {
            toolBrush = new BrushTool<int>();
            toolEraser = new EraserTool<int>(EditorDefaults.EmptyColorID);
            toolBucket = new BucketTool<int>();

            this.UIGrid = UIGrid;
            SwitchTool(ToolType.Brush);
        }
        
        /// <summary>
        /// Applies the effect of the current tool on a specific grid position.
        /// </summary>
        /// <param name="grid">The grid to affect.</param>
        /// <param name="position">The position to start with.</param>
        /// <param name="value">The value to set.</param>
        public void ApplyCurrent(ObjectGrid<int> grid, Vector2Int position, ColorSlot value)
        {
            currentColor = value.CurrentColor;
            currentTool.ApplyEffect(grid, position, value.Index, WhenDrawOnUIGrid);
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
            Color value = (useEmpty) ? EditorDefaults.EmptyGridColor : currentColor;
            UIGrid.UpdateCellColor(position, value);
        }
        
    }
}