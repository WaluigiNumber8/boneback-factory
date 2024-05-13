using System;
using Rogium.Systems.GridSystem;
using Rogium.Systems.Toolbox;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that uses the <see cref="ToolBase{T}"/> on a grid.
    /// </summary>
    public class UseToolAction<T> : IAction where T : IComparable
    {
        private readonly ToolBase<T> brushTool;
        private readonly ObjectGrid<T> grid;
        private readonly Vector2Int position;
        private readonly int layer;
        
        private readonly T value;
        private readonly T oldValue;
        private readonly Sprite graphicValue;
        private readonly Sprite oldGraphicValue;

        public UseToolAction(ToolBase<T> brushTool, ObjectGrid<T> grid, Vector2Int position, T value, T oldValue, Sprite graphicValue, Sprite oldGraphicValue, int layer)
        {
            this.brushTool = brushTool;
            this.grid = grid;
            this.position = position;
            this.value = value;
            this.oldValue = oldValue;
            this.layer = layer;
            this.graphicValue = graphicValue;
            this.oldGraphicValue = oldGraphicValue;
        }

        public void Execute() => brushTool.ApplyEffect(grid, position, value, graphicValue, layer);

        public void Undo() => brushTool.ApplyEffect(grid, position, oldValue, oldGraphicValue, layer);

        public bool NothingChanged() => value.CompareTo(oldValue) == 0;

        public override string ToString() => $"{brushTool}: {oldValue} -> {value} at {layer}-{position}";
    }
}