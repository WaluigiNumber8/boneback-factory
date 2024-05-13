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
        public event Action<int, Vector2Int, Sprite> OnGraphicDraw;
        public event Action<T> OnPickValue; 

        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Sprite graphicValue, int layer, Action<int> whenEffectFinished)
        {
            T valueFromGrid = grid.GetValue(position);
            OnPickValue?.Invoke(valueFromGrid);
        }

        public override string ToString() => "Picker Tool";
    }
}