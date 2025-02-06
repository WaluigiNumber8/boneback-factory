using System.Collections;
using NUnit.Framework;
using RedRats.UI.ModalWindows;
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
            yield return TUtilsMenuLoader.PrepareSelectionMenu();
            yield return TUtilsMenuLoader.PrepareRoomEditor(false);
            TUtilsOverseerLoader.LoadModalWindowBuilder();
            editor = RoomEditorOverseerMono.GetInstance();
            yield return null;
            yield return OpenEditor(AssetType.Room);
        }
        
        [UnityTest]
        public IEnumerator Should_Undo_WhenShortcutPressed()
        {
            yield return FillTileLayer();
            AssetData drawnCell = editor.GetCurrentGridCopy.GetAt(0, 0);
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.yKey);
            i.Trigger(input.Shortcuts.Undo.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.Not.EqualTo(drawnCell));
        }

        [UnityTest]
        public IEnumerator Should_Redo_WhenShortcutPressed()
        {
            yield return FillTileLayer();
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.zKey);
            i.Trigger(input.Shortcuts.Redo.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.Not.EqualTo(new AssetData()));
        }

        [UnityTest]
        public IEnumerator Should_SaveChanges_WhenShortcutPressed()
        {
            AssetData originalData = PackEditorOverseer.Instance.CurrentPack.Rooms[0].TileGrid.GetAt(0, 0);
            yield return FillTileLayer();
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.sKey);
            i.Trigger(input.Shortcuts.Save.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Rooms[0].TileGrid.GetAt(0, 0), Is.Not.EqualTo(originalData));
        }

        [UnityTest]
        public IEnumerator Should_CancelChanges_WhenShortcutPressed()
        {
            yield return FillTileLayer();
            AssetData drawnCell = editor.GetCurrentGridCopy.GetAt(0, 0);
            i.Trigger(input.UI.Cancel.Action);
            yield return null;
            yield return null;
            yield return WindowAccept();
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.EqualTo(drawnCell));
        }
        
        [UnityTest]
        public IEnumerator Should_SelectSelectionTool_WhenShortcutPressed()
        {
            SelectTool(ToolType.Eraser);
            i.Trigger(input.Shortcuts.SelectionTool.Action);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.Selection));
        }

        [UnityTest]
        public IEnumerator Should_SelectBrushTool_WhenShortcutPressed()
        {
            SelectTool(ToolType.Eraser);
            i.Trigger(input.Shortcuts.BrushTool.Action);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.Brush));
        }

        [UnityTest]
        public IEnumerator Should_SelectEraseTool_WhenShortcutPressed()
        {
            SelectTool(ToolType.Brush);
            i.Trigger(input.Shortcuts.EraserTool.Action);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.Eraser));
        }

        [UnityTest]
        public IEnumerator Should_SelectFillTool_WhenShortcutPressed()
        {
            SelectTool(ToolType.Brush);
            i.Trigger(input.Shortcuts.FillTool.Action);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.Fill));
        }

        [UnityTest]
        public IEnumerator Should_SelectPickerTool_WhenShortcutPressed()
        {
            SelectTool(ToolType.Brush);
            i.Trigger(input.Shortcuts.PickerTool.Action);
            yield return null;
            Assert.That(editor.Toolbox.CurrentTool, Is.EqualTo(ToolType.ColorPicker));
        }

        [UnityTest]
        public IEnumerator Should_ClearCanvas_WhenShortcutPressed()
        {
            yield return FillTileLayer();
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.cKey);
            i.Trigger(input.Shortcuts.ClearCanvas.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.EqualTo(new AssetData()));
        }

        [UnityTest]
        public IEnumerator Should_ToggleGrid_WhenShortcutPressed()
        {
            GameObject grid = GameObject.Find("Gridlines");
            bool startActiveStatus = grid.activeSelf;
            i.Trigger(input.Shortcuts.ToggleGrid.Action);
            yield return null;
            Assert.That(grid.activeSelf, Is.Not.EqualTo(startActiveStatus));
        }

        [UnityTest]
        public IEnumerator Should_CloseModalWindow_WhenShortcutPressed()
        {
            yield return FillTileLayer();
            i.Trigger(input.UI.Cancel.Action);
            yield return null;
            yield return new WaitForSecondsRealtime(2f);
            yield return null;
            i.Trigger(input.UI.Cancel.Action);
            yield return new WaitForSecondsRealtime(2f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(FindObjectsInactive.Include).transform.GetChild(0).gameObject.activeSelf, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_ShowTilesLayer_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.TilesLayer.Action);
            yield return null;
            Assert.That(editor.ActiveLayerIndex, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator Should_ShowDecorLayer_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.DecorLayer.Action);
            yield return null;
            Assert.That(editor.ActiveLayerIndex, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator Should_ShowObjectsLayer_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.ObjectsLayer.Action);
            yield return null;
            Assert.That(editor.ActiveLayerIndex, Is.EqualTo(2));
        }

        [UnityTest]
        public IEnumerator Should_ShowEnemyLayer_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.EnemiesLayer.Action);
            yield return null;
            Assert.That(editor.ActiveLayerIndex, Is.EqualTo(3));
        }
    }
}