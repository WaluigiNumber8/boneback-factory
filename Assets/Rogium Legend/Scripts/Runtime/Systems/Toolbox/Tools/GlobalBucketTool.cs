using System;
using RedRats.Safety;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Systems.Toolbox
{
    /// <summary>
    /// The bucket tool that replaces a value in the entire grid.
    /// </summary>
    public class GlobalBucketTool<T> : ToolBase<T> where T : IComparable
    {
        public GlobalBucketTool(Action<int, Vector2Int, Sprite> whenGraphicDrawn, Action<int> whenEffectFinished) : base(whenGraphicDrawn, whenEffectFinished) { }

        public override void ApplyEffect(ObjectGrid<T> grid, Vector2Int position, T value, Sprite graphicValue, int layer)
        {
            SafetyNet.EnsureIsNotNull(grid, nameof(grid));
            
            //Replace the value in the entire grid if it is the same as the one selected.
            T valueToOverride = grid.GetAt(position);
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    if (grid.GetAt(x, y).CompareTo(valueToOverride) != 0) continue;
                    
                    grid.SetTo(x, y, value);
                    whenGraphicDrawn?.Invoke(layer, new Vector2Int(x, y), graphicValue);
                }
            }
            whenEffectFinished?.Invoke(layer);
        }
    }
}