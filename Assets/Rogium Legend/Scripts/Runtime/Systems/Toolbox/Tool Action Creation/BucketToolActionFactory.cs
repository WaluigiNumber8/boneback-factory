using System;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Creates new <see cref="IAction"/>s for the <see cref="BucketTool{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BucketToolActionFactory<T> : IToolActionFactory<T> where T : IComparable
    {
        public IAction Create(ToolBase<T> tool, ObjectGrid<T> grid, Vector2Int position, T value, T lastValue, Sprite graphicValue, Sprite lastGraphicValue, int layer)
        {
            return new UseBucketToolAction<T>(tool as BucketTool<T>, grid, position, value, lastValue, graphicValue, lastGraphicValue, layer);
        }
    }
}