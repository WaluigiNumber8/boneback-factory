using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Rooms;
using Rogium.Systems.Toolbox;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsMenuNavigation;
using static Rogium.Tests.Core.TUtilsRoomEditor;

namespace Rogium.Tests.Systems.Shortcuts
{
    /// <summary>
    /// Tests for room editor shortcut actions.
    /// </summary>
    public class ShortcutActionsRoomEditorTests : MenuTestWithInputBase
    {
        private RoomEditorOverseerMono editor;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            yield return MenuLoader.PrepareSelectionMenu();
            yield return MenuLoader.PrepareRoomEditor(false);
            editor = RoomEditorOverseerMono.GetInstance();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator Should_Undo_WhenShortcutPressed()
        {
            yield return OpenEditor(AssetType.Room);
            yield return FillTileLayer();
            AssetData drawnCell = editor.GetCurrentGridCopy.GetAt(0, 0);
            yield return null;
            PressAndRelease(keyboard.leftCtrlKey, 0.1);
            yield return null;
            Press(keyboard.yKey);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.Not.EqualTo(drawnCell));
        }
        
        [UnityTest]
        public IEnumerator Should_SelectTheSelectTool_WhenShortcutPressed()
        {
            yield return OpenEditor(AssetType.Room);
            SelectTool(ToolType.Eraser);
            yield return null;
            Press(keyboard.sKey);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.Selection));
        }
    }
}