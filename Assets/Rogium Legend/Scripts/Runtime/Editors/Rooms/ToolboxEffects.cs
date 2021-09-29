using System;
using Rogium.Editors.Core;
using Rogium.Global.GridSystem;
using UnityEngine;

namespace Rogium.Editors.RoomData
{
    /// <summary>
    /// Controls, what happens when the currently active tool is used.
    /// </summary>
    public class ToolboxEffects
    {

        /// <summary>
        /// Executes the tools effect on the asset.
        /// </summary>
        public void UseTool(ObjectGrid<string> assetGrid, AssetBase asset, Vector2Int gridPosition, ToolType tool)
        {
            switch (tool)
            {
                case ToolType.Brush:
                    assetGrid.SetValue(gridPosition.x, gridPosition.y, asset.ID);
                    break;
                case ToolType.Eraser:
                    break;
                case ToolType.Bucket:
                    break;
                case ToolType.Picker:
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Tool type '{tool}' is not supported.");
            }
        }
        
    }
}