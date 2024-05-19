using Rogium.Systems.ActionHistory;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// Creates null <see cref="IAction"/> so that it doesn't get added to Action History and uses the tool the normal way.
    /// </summary>
    public class SilentToolsActionFactory<T> : IToolActionFactory<T> where T : System.IComparable
    {
        public IAction Create(ToolBase<T> tool, ObjectGrid<T> grid, Vector2Int position, T value, T lastValue, Sprite graphicValue, Sprite lastGraphicValue, int layer)
        {
            tool.ApplyEffect(grid, position, value, graphicValue, layer);
            return null;
        }
    }
}