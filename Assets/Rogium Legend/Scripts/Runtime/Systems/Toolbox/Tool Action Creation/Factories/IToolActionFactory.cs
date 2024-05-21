using System;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Represents a factory that creates <see cref="ActionBase{T}"/>s.
    /// </summary>
    public interface IToolActionFactory<T> where T : IComparable
    {
        public ActionBase<T> Create(ToolBase<T> tool, ObjectGrid<T> grid, Vector2Int position, T value, T lastValue, Sprite graphicValue, Sprite lastGraphicValue, int layer, Action<T> fallback);
    }
}