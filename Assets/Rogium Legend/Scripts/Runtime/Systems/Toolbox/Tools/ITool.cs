using System;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    public interface ITool<T> where T : IComparable
    {
        /// <summary>
        /// Applies the effect of the tool.
        /// </summary>
        /// <param name="grid">The grid to affect.</param>
        /// <param name="position">The position on the grid to affect.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="applyOnUI">The method that will update UI.</param>
        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Action<Vector2Int, bool> applyOnUI);
    }
}