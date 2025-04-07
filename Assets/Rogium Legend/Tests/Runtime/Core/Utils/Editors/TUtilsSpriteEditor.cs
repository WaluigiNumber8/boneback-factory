using System.Collections;
using Rogium.Editors.Sprites;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.Toolbox;
using UnityEngine;

namespace Rogium.Tests.Core
{
    /// <summary>
    /// General utility methods for the Room Editor.
    /// </summary>
    public static class TUtilsSpriteEditor
    {
        public static IEnumerator FillCanvas()
        {
            SelectTool(ToolType.Fill);
            SpriteEditorOverseerMono.Instance.UpdateCell(new Vector2Int(0, 0));
            ActionHistorySystem.EndCurrentGroup();
            yield return null;
        }

        public static void SelectTool(ToolType toolType) => SpriteEditorOverseerMono.Instance.Toolbox.SwitchTool(toolType);
    }
}