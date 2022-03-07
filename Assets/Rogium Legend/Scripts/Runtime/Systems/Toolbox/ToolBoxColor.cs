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
        public event Action<ToolType> OnSwitchTool;
        public event Action<int> OnChangePaletteValue; 

        private readonly BrushTool<int> toolBrush;
        private readonly EraserTool<int> toolEraser;
        private readonly BucketTool<int> toolBucket;
        private readonly PickerTool<int> toolPicker;

        private readonly InteractableEditorGridBase UIGrid;
        private Color currentColor;
        private ITool<int> currentTool;
        private ToolType currentToolType;

        public ToolBoxColor(InteractableEditorGridBase UIGrid)
        {
            toolBrush = new BrushTool<int>();
            toolEraser = new EraserTool<int>(EditorDefaults.EmptyColorID);
            toolBucket = new BucketTool<int>();
            toolPicker = new PickerTool<int>();

            this.UIGrid = UIGrid;

            toolPicker.OnPickValue += ctx => OnChangePaletteValue?.Invoke(ctx);

            currentTool = toolBrush;
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
            currentTool.ApplyEffect(grid, position, value.Index, WhenDrawOnUIGrid, UIGrid.Apply);
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
            Color value = (useEmpty) ? EditorDefaults.NoColor : currentColor;
            UIGrid.UpdateCell(position, value);
        }
        
    }
}