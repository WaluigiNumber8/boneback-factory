using System.Collections;
using Rogium.Editors.Rooms;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.Toolbox;
using UnityEngine;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// General utility methods for the Room Editor.
    /// </summary>
    public class TUtilsRoomEditor
    {
        public static IEnumerator FillTileLayer()
        {
            RoomEditorOverseerMono.GetInstance().Toolbox.SwitchTool(ToolType.Fill);
            RoomEditorOverseerMono.GetInstance().UpdateGridCell(new Vector2Int(0, 0));
            ActionHistorySystem.ForceEndGrouping();
            yield return null;
        }
    }
}