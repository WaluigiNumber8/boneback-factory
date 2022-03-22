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
        public event Action<T> OnSelectValue;
        
        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Action<Vector2Int, bool> applyOnUI, Action finishProcess)
        {
            T selected = grid.GetValue(position);
            OnSelectValue?.Invoke(selected);
        }
    }
}