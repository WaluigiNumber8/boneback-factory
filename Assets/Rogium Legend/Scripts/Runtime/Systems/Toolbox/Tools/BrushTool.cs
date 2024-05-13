using System;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// The Brush Tool that fills a single cell on the grid.
    /// </summary>
    public class BrushTool<T> : ToolBase<T> where T : IComparable
    {
        public BrushTool(Action<int, Vector2Int, Sprite> whenGraphicDrawn, Action<int> whenEffectFinished) : base(whenGraphicDrawn, whenEffectFinished) { }

        public override void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Sprite graphicValue, int layer)
        {
            grid.SetValue(position, value);
            whenGraphicDrawn?.Invoke(layer, position, graphicValue);
            whenEffectFinished?.Invoke(layer);
        }

        public override string ToString() => "Brush Tool";

    }
}
