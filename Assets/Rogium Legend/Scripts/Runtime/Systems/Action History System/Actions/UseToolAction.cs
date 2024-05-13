using System;
using Rogium.Systems.GridSystem;
using Rogium.Systems.Toolbox;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that uses the <see cref="ITool{T}"/> on a grid.
    /// </summary>
    public class UseToolAction<T> : IAction where T : IComparable
    {
        private readonly ITool<T> brushTool;
        private readonly ObjectGrid<T> grid;
        private readonly Vector2Int position;
        
        private readonly Action<int, Vector2Int, bool> applyOnUI;
        private readonly Action<int> finishProcess;

        private readonly int layerIndex;
        private readonly T value;
        private readonly T oldValue;
        private readonly Sprite graphicValue;
        private readonly Sprite oldGraphicValue;

        public UseToolAction(ITool<T> brushTool, ObjectGrid<T> grid, Vector2Int position, T value, T oldValue, Sprite graphicValue, Sprite oldGraphicValue, int layerIndex, Action<int> finishProcess)
        {
            this.brushTool = brushTool;
            this.grid = grid;
            this.position = position;
            this.value = value;
            this.oldValue = oldValue;
            this.layerIndex = layerIndex;
            this.graphicValue = graphicValue;
            this.oldGraphicValue = oldGraphicValue;
            this.finishProcess = finishProcess;
        }

        public void Execute()
        {
            brushTool.ApplyEffect(grid, position, value, graphicValue, layerIndex, finishProcess);
        }

        public void Undo()
        {
            brushTool.ApplyEffect(grid, position, oldValue, oldGraphicValue, layerIndex, finishProcess);
        }

        public bool NothingChanged() => value.CompareTo(oldValue) == 0;

        public override string ToString() => $"{brushTool}: {oldValue} -> {value} at {layerIndex}-{position}";
    }
}