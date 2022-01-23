using System;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// The Brush Tool that fills a single cell on the grid.
    /// </summary>
    public class BrushTool<T> : ITool<T>
    {
        public event Action<Vector2Int, bool> OnEffectApplied;
        
        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value)
        {
            grid.SetValue(position, value);
            OnEffectApplied?.Invoke(position, false);
        }

    }
}
