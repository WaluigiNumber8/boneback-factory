using System;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    public class EraserTool<T> : ITool<T> where T : IComparable
    {
        private T emptyValue;
        
        public EraserTool(T emptyValue)
        {
            this.emptyValue = emptyValue;
        }

        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Action<Vector2Int, bool> applyOnUI)
        {
            grid.SetValue(position, emptyValue);
            applyOnUI?.Invoke(position, true);
        }
    }
}