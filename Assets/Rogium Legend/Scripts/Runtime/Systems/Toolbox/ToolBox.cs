using System;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Houses all the tools used on an <see cref="InteractableEditorGrid"/>.
    /// </summary>
    public class ToolBox<T, TS> : IToolBox where T : IComparable
    {
        public event Action<ToolType> OnSwitchTool;
        public event Action<T> OnChangePaletteValue; 
        
        private readonly BrushTool<T> toolBrush;
        private readonly EraserTool<T> toolEraser;
        private readonly BucketTool<T> toolBucket;
        private readonly PickerTool<T> toolPicker;

        private readonly InteractableEditorGridBase UIGrid;
        private TS currentBrush;
        private ITool<T> currentTool;
        private ToolType currentToolType;
        private Action<Vector2Int, TS> drawOnGrid;
        private TS emptyValueEditor;

        public ToolBox(InteractableEditorGridBase UIGrid, T emptyValue, TS emptyValueEditor, Action<Vector2Int, TS> drawOnGrid)
        {
            this.UIGrid = UIGrid;
            this.drawOnGrid = drawOnGrid;
            this.emptyValueEditor = emptyValueEditor;
            
            toolBrush = new BrushTool<T>();
            toolEraser = new EraserTool<T>(emptyValue);
            toolBucket = new BucketTool<T>();
            toolPicker = new PickerTool<T>();

            toolPicker.OnPickValue += id => OnChangePaletteValue?.Invoke(id);
            
            currentTool = toolBrush;
        }
        
        /// <summary>
        /// Applies the effect of the current tool on a specific grid position.
        /// </summary>
        /// <param name="grid">The grid to affect.</param>
        /// <param name="position">The position to start with.</param>
        /// <param name="value">The value to set.</param>
        public void ApplyCurrent(ObjectGrid<T> grid, Vector2Int position, T value, TS brush)
        {
            currentBrush = brush;
            currentTool.ApplyEffect(grid, position, value, WhenDrawOnUIGrid, UIGrid.Apply);
        }

        /// <summary>
        /// Applies the effect of a specific tool based on tool type.
        /// </summary>
        /// <param name="tool">The tool to use.</param>
        /// <param name="grid">The grid to affect.</param>
        /// <param name="position">The position to start with.</param>
        /// <param name="value">The value to set.</param>
        public void ApplySpecific(ToolType tool, ObjectGrid<T> grid, Vector2Int position, T value)
        {
            GetTool(tool).ApplyEffect(grid, position, value, WhenDrawOnUIGrid, UIGrid.Apply);
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
            TS value = (useEmpty) ? emptyValueEditor : currentBrush;
            drawOnGrid?.Invoke(position, value);
        }
        
        /// <summary>
        /// Grabs a tool based on entered tool type.
        /// </summary>
        /// <param name="toolType">The type of tool to get.</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Is thrown when <see cref="ToolType"/> is unknown or unsupported.</exception>
        private ITool<T> GetTool(ToolType toolType)
        {
            ITool<T> tool = toolType switch
            {
                ToolType.Brush => toolBrush,
                ToolType.Eraser => toolEraser,
                ToolType.Bucket => toolBucket,
                ToolType.ColorPicker => toolPicker,
                _ => throw new InvalidOperationException("Unknown or not yet supported Tool Type.")
            };
            return tool;
        }

    }
}