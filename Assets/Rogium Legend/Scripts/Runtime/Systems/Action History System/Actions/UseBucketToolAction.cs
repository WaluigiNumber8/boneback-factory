using System;
using System.Collections.Generic;
using Rogium.Systems.GridSystem;
using Rogium.Systems.Toolbox;
using UnityEngine;

namespace Rogium.Systems.ActionHistory
{
    /// <summary>
    /// An action that uses the <see cref="BucketTool{T}"/> on a grid.
    /// </summary>
    public class UseBucketToolAction<T> : IAction where T : IComparable
    {
        private readonly BucketTool<T> tool;
        private readonly ObjectGrid<T> grid;
        private readonly Vector2Int position;
        private readonly int layer;
        
        private readonly T value;
        private readonly T lastValue;
        private readonly Sprite graphicValue;
        private readonly Sprite lastGraphicValue;
        private readonly ISet<Vector2Int> affectedPositions;
        
        public UseBucketToolAction(BucketTool<T> tool, ObjectGrid<T> grid, Vector2Int position, T value, T lastValue, Sprite graphicValue, Sprite lastGraphicValue, int layer)
        {
            this.tool = tool;
            this.grid = grid;
            this.position = position;
            this.value = value;
            this.lastValue = lastValue;
            this.layer = layer;
            this.graphicValue = graphicValue;
            this.lastGraphicValue = lastGraphicValue;
            this.affectedPositions = tool.LastProcessedPositions;
        }
        
        public void Execute() => tool.ApplyEffect(grid, position, value, graphicValue, layer);

        public void Undo() => tool.ApplyEffect(grid, affectedPositions, lastValue, lastGraphicValue, layer);

        public bool NothingChanged() => value.CompareTo(lastValue) == 0;

        public object AffectedConstruct => grid;

        public override string ToString() => $"{tool}: {lastValue} -> {value} at {layer}-{position}";
    }
}