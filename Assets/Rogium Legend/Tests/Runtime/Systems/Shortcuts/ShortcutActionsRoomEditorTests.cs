using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Systems.Toolbox;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsMenuNavigation;
using static Rogium.Tests.Core.TUtilsModalWindow;
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
            yield return OpenEditor(AssetType.Room);
        }
        
        [UnityTest]
        public IEnumerator Should_Undo_WhenShortcutPressed()
        {
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
        public IEnumerator Should_Redo_WhenShortcutPressed()
        {
            yield return FillTileLayer();
            PressAndRelease(keyboard.leftCtrlKey, 0.1);
            yield return null;
            Press(keyboard.zKey);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.EqualTo(new AssetData()));
        }

        [UnityTest]
        public IEnumerator Should_SaveChanges_WhenShortcutPressed()
        {
            AssetData originalData = PackEditorOverseer.Instance.CurrentPack.Rooms[0].TileGrid.GetAt(0, 0);
            yield return FillTileLayer();
            PressAndRelease(keyboard.leftCtrlKey, 0.1);
            yield return null;
            Press(keyboard.sKey);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Rooms[0].TileGrid.GetAt(0, 0), Is.Not.EqualTo(originalData));
        }

        [UnityTest]
        public IEnumerator Should_CancelChanges_WhenShortcutPressed()
        {
            yield return FillTileLayer();
            AssetData drawnCell = editor.GetCurrentGridCopy.GetAt(0, 0);
            yield return null;
            Press(keyboard.escapeKey, 0.1);
            yield return new WaitForSecondsRealtime(0.1f);
            yield return WindowAccept();
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.EqualTo(drawnCell));
        }
        
        [UnityTest]
        public IEnumerator Should_SelectSelectionTool_WhenShortcutPressed()
        {
            SelectTool(ToolType.Eraser);
            yield return null;
            Press(keyboard.sKey);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.Selection));
        }

        [UnityTest]
        public IEnumerator Should_SelectBrushTool_WhenShortcutPressed()
        {
            SelectTool(ToolType.Eraser);
            yield return null;
            Press(keyboard.bKey);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.Brush));
        }

        [UnityTest]
        public IEnumerator Should_SelectEraseTool_WhenShortcutPressed()
        {
            SelectTool(ToolType.Brush);
            yield return null;
            Press(keyboard.eKey);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.Eraser));
        }

        [UnityTest]
        public IEnumerator Should_SelectFillTool_WhenShortcutPressed()
        {
            SelectTool(ToolType.Brush);
            yield return null;
            Press(keyboard.fKey);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.Fill));
        }

        [UnityTest]
        public IEnumerator Should_SelectPickerTool_WhenShortcutPressed()
        {
            SelectTool(ToolType.Brush);
            yield return null;
            Press(keyboard.pKey);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.ColorPicker));
        }

        [UnityTest]
        public IEnumerator Should_ClearCanvas_WhenShortcutPressed()
        {
            yield return FillTileLayer();
            yield return null;
            PressAndRelease(keyboard.leftCtrlKey, 0.1);
            yield return null;
            Press(keyboard.cKey);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.EqualTo(new AssetData()));
        }

        [UnityTest]
        public IEnumerator Should_ToggleGrid_WhenShortcutPressed()
        {
            GameObject grid = GameObject.Find("Gridlines");
            bool startActiveStatus = grid.activeSelf;
            yield return null;
            Press(keyboard.semicolonKey);
            yield return null;
            Assert.That(grid.activeSelf, Is.Not.EqualTo(startActiveStatus));
        }
        
    }
}