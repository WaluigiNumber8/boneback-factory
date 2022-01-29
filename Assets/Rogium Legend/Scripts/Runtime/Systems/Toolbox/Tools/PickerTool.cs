using System;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// The Color Picker Tool, that selects an item from the palette based on whats on the canvas.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PickerTool<T> : ITool<T> where T : IComparable
    {
        public event Action<T> OnPickValue; 

        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Action<Vector2Int, bool> applyOnUI)
        {
            T valueFromGrid = grid.GetValue(position);
            OnPickValue?.Invoke(valueFromGrid);
        }
    }
}