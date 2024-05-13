using System;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// A tool that is able to select assets placed in the grid and update UI accordingly.
    /// </summary>
    /// <typeparam name="T">Any type of <see cref="IComparable"/>.</typeparam>
    public class SelectionTool<T> : ITool<T> where T : IComparable
    {
        public event Action<int, Vector2Int, Sprite> OnGraphicDraw;
        public event Action<T> OnSelectValue;
        
        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Sprite graphicValue, int layer, Action<int> whenEffectFinished)
        {
            T selected = grid.GetValue(position);
            OnSelectValue?.Invoke(selected);
        }

        public override string ToString() => "Selection Tool";
    }
}