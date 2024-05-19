using System;
using System.Collections.Generic;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Creates <see cref="IAction"/>s for the toolbox.
    /// </summary>
    public class ToolActionFactory<T> : IToolActionFactory<T> where T : IComparable
    {
        private IDictionary<Type, IToolActionFactory<T>> factories;

        public ToolActionFactory() => InitFactories();

        private void InitFactories()
        {
            factories = new Dictionary<Type, IToolActionFactory<T>>();
            factories.Add(typeof(BrushTool<T>), new BrushToolActionFactory<T>());
            factories.Add(typeof(BucketTool<T>), new BucketToolActionFactory<T>());
            factories.Add(typeof(PickerTool<T>), new SilentToolsActionFactory<T>());
            factories.Add(typeof(SelectionTool<T>), new SilentToolsActionFactory<T>());
        }
        
        public IAction Create(ToolBase<T> tool, ObjectGrid<T> grid, Vector2Int position, T value, T lastValue, Sprite graphicValue, Sprite lastGraphicValue, int layer)
        {
            factories.TryGetValue(tool.GetType(), out IToolActionFactory<T> factory);
            return factory?.Create(tool, grid, position, value, lastValue, graphicValue, lastGraphicValue, layer);
        }
    }
}