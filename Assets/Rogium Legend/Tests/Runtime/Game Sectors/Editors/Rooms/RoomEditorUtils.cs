using Rogium.Editors.Core;
using Rogium.Editors.Rooms;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Tests.Editors.Rooms
{
    /// <summary>
    /// Utils for the <see cref="RoomEditorOverseerMono"/> tests.
    /// </summary>
    public static class RoomEditorUtils
    {
        private static readonly RoomEditorOverseerMono roomEditor = RoomEditorOverseerMono.GetInstance();
        
        public static void FillEntireActiveLayer()
        {
            ObjectGrid<AssetData> layer = roomEditor.GetCurrentGridCopy;
            ActionHistorySystem.ForceBeginGrouping();
            for (int i = 0; i < layer.Width; i++)
            {
                for (int j = 0; j < layer.Height; j++)
                {
                    roomEditor.UpdateGridCell(new Vector2Int(i, j));
                }
            }
            ActionHistorySystem.ForceEndGrouping();
        }
    }
}