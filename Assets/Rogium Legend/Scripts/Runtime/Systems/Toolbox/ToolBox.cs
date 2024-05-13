using System;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Houses all the tools used on an <see cref="InteractableEditorGrid"/>.
    /// </summary>
    /// <typeparam name="T">The type in which to store data.</typeparam>
    public class ToolBox<T> : IToolBox where T : IComparable
    {
        public event Action<ToolType> OnSwitchTool;
        public event Action<T> OnChangePaletteValue;
        public event Action<T> OnSelectValue;

        private readonly SelectionTool<T> toolSelection;
        private readonly BrushTool<T> toolBrush;
        // private readonly EraserTool<T> toolEraser;
        private readonly BucketTool<T> toolBucket;
        private readonly PickerTool<T> toolPicker;

        private readonly InteractableEditorGridBase UIGrid;
        private readonly Action<int, Vector2Int, Sprite> drawOnGrid;
        private ITool<T> currentTool;
        private ToolType currentToolType;
        
        public ToolBox(InteractableEditorGridBase UIGrid, Action<int, Vector2Int, Sprite> drawOnGrid)
        {
            this.UIGrid = UIGrid;
            this.drawOnGrid = drawOnGrid;

            toolSelection = new SelectionTool<T>();
            toolBrush = new BrushTool<T>();
            // toolEraser = new EraserTool<T>(emptyValue);
            toolBucket = new BucketTool<T>();
            toolPicker = new PickerTool<T>();
            
            toolBrush.OnGraphicDraw += WhenDrawOnUIGrid;

            toolSelection.OnSelectValue += data => OnSelectValue?.Invoke(data);
            toolPicker.OnPickValue += data => OnChangePaletteValue?.Invoke(data);
            
            currentTool = toolBrush;
        }

        /// <summary>
        /// Applies the effect of the current tool on a specific grid position.
        /// </summary>
        /// <param name="grid">The grid to affect.</param>
        /// <param name="position">The position to start with.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="graphicValue">The sprite to draw onto the grid.</param>
        /// <param name="layerIndex">The index of the layer to draw onto.</param>
        public void ApplyCurrent(ObjectGrid<T> grid, Vector2Int position, T value, Sprite graphicValue, int layerIndex)
        {
            UseTool(currentTool, grid, position, value, graphicValue, layerIndex);
        }

        /// <summary>
        /// Applies the effect of a specific tool based on tool type.
        /// </summary>
        /// <param name="tool">The tool to use.</param>
        /// <param name="grid">The grid to affect.</param>
        /// <param name="position">The position to start with.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="graphicValue">The sprite to draw onto the grid.</param>
        /// <param name="layerIndex">The index of the layer to draw onto.</param>
        public void ApplySpecific(ToolType tool, ObjectGrid<T> grid, Vector2Int position, T value, Sprite graphicValue, int layerIndex)
        {
            UseTool(GetTool(tool), grid, position, value, graphicValue, layerIndex);
        }
        
        public void SwitchTool(ToolType tool)
        {
            if (currentToolType == tool) return;
            currentTool = tool switch
            {
                ToolType.Selection => toolSelection,
                ToolType.Brush => toolBrush,
                // ToolType.Eraser => toolEraser,
                ToolType.Bucket => toolBucket,
                ToolType.ColorPicker => toolPicker,
                _ => throw new InvalidOperationException("Unknown or not yet supported Tool Type.")
            };
            currentToolType = tool;
            OnSwitchTool?.Invoke(tool);
        }

        public void WhenDrawOnUIGrid(int layerIndex, Vector2Int position, Sprite value) => drawOnGrid?.Invoke(layerIndex, position, value);

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
                ToolType.Selection => toolSelection,
                ToolType.Brush => toolBrush,
                // ToolType.Eraser => toolEraser,
                ToolType.Bucket => toolBucket,
                ToolType.ColorPicker => toolPicker,
                _ => throw new InvalidOperationException("Unknown or not yet supported Tool Type.")
            };
            return tool;
        }

        private void UseTool(ITool<T> tool, ObjectGrid<T> grid, Vector2Int position, T value, Sprite graphicValue, int layerIndex)
        {
            T oldValue = grid.GetValue(position);
            Sprite oldGraphicValue = UIGrid.GetCell(position);
            ActionHistorySystem.AddAndExecute(new UseToolAction<T>(tool, grid, position, value, oldValue, graphicValue, oldGraphicValue, layerIndex, UIGrid.Apply));
        }
        
    }
}