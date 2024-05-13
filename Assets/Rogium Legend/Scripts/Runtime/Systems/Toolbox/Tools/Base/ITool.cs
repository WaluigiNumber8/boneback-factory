using System;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    public interface ITool<T> where T : IComparable
    {
        public event Action<int, Vector2Int, Sprite> OnGraphicDraw;

        /// <summary>
        /// Applies the effect of the tool.
        /// </summary>
        /// <param name="grid">The grid to affect.</param>
        /// <param name="position">The position on the grid to affect.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="layer">The graphic layer index to draw the <see cref="graphicValue"/>.</param>
        /// <param name="graphicValue">The graphic that represents the data.</param>
        /// <param name="whenEffectFinished">The method to call when the process is finished.</param>
        public void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Sprite graphicValue, int layer, Action<int> whenEffectFinished);
    }
}