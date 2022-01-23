using System;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    public interface ITool<T>
    {
        public event Action<Vector2Int, bool> OnEffectApplied;
        
        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value);
    }
}