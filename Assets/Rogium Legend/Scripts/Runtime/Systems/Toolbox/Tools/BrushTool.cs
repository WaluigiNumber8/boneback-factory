using System;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// The Brush Tool that fills a single cell on the grid.
    /// </summary>
    public class BrushTool<T> : ITool<T> where T : IComparable
    {
        public event Action<int, Vector2Int, Sprite> OnGraphicDraw;

        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Sprite graphicValue, int layerIndex, Action<int> whenEffectFinished)
        {
            grid.SetValue(position, value);
            OnGraphicDraw?.Invoke(layerIndex, position, graphicValue);
            whenEffectFinished?.Invoke(layerIndex);
        }

        public override string ToString() => "Brush Tool";
    }
}
