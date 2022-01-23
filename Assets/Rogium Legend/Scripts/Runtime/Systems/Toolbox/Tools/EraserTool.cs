using System;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    public class EraserTool<T> : ITool<T>
    {
        public event Action<Vector2Int, bool> OnEffectApplied;
        
        private T emptyValue;
        
        public EraserTool(T emptyValue)
        {
            this.emptyValue = emptyValue;
        }

        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value)
        {
            grid.SetValue(position, emptyValue);
            OnEffectApplied?.Invoke(position, true);
        }
    }
}