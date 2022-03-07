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
        
        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Action<Vector2Int, bool> applyOnUI, Action finishProcess)
        {
            grid.SetValue(position, value);
            applyOnUI?.Invoke(position, false);
            finishProcess?.Invoke();
        }

    }
}
