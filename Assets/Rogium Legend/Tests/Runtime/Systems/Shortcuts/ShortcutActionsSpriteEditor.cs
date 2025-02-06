using System.Collections;
using NUnit.Framework;
using RedRats.UI.MenuSwitching;
using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Sprites;
using Rogium.Systems.Toolbox;
using Rogium.Tests.Core;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsMenuNavigation;
using static Rogium.Tests.Core.TUtilsModalWindow;
using static Rogium.Tests.Core.TUtilsSpriteEditor;

namespace Rogium.Tests.Systems.Shortcuts
{
    /// <summary>
    /// Tests for the Sprite Editor shortcut actions.
    /// </summary>
    public class ShortcutActionsSpriteEditor : MenuTestWithInputBase
    {
        private SpriteEditorOverseerMono editor;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            yield return TUtilsMenuLoader.PrepareSelectionMenu();
            yield return TUtilsMenuLoader.PrepareSpriteEditor(false);
            TUtilsOverseerLoader.LoadModalWindowBuilder();
            editor = SpriteEditorOverseerMono.GetInstance();
            yield return null;
            yield return OpenEditor(AssetType.Sprite);
        }
        
        [UnityTest]
        public IEnumerator Should_Undo_WhenShortcutPressed()
        {
            yield return FillCanvas();
            int drawnCell = editor.GetCurrentGridCopy.GetAt(0, 0);
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.yKey);
            i.Trigger(input.Shortcuts.Undo.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.Not.EqualTo(drawnCell));
        }

        [UnityTest]
        public IEnumerator Should_Redo_WhenShortcutPressed()
        {
            yield return FillCanvas();
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.zKey);
            i.Trigger(input.Shortcuts.Redo.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.Not.EqualTo(EditorDefaults.EmptyColorID));
        }

        [UnityTest]
        public IEnumerator Should_SaveChanges_WhenShortcutPressed()
        {
            int originalData = PackEditorOverseer.Instance.CurrentPack.Sprites[0].SpriteData.GetAt(0, 0);
            yield return FillCanvas();
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.sKey);
            i.Trigger(input.Shortcuts.Save.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Sprites[0].SpriteData.GetAt(0, 0), Is.Not.EqualTo(originalData));
        }

        [UnityTest]
        public IEnumerator Should_CancelChanges_WhenShortcutPressed()
        {
            yield return FillCanvas();
            int drawnCell = editor.GetCurrentGridCopy.GetAt(0, 0);
            i.Trigger(input.UI.Cancel.Action);
            yield return null;
            yield return null;
            yield return WindowAccept();
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.EqualTo(drawnCell));
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
            yield return FillCanvas();
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.cKey);
            i.Trigger(input.Shortcuts.ClearCanvas.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetCurrentGridCopy.GetAt(0, 0), Is.EqualTo(EditorDefaults.EmptyColorID));
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
        public IEnumerator Should_OpenAssetPickerForPalettes_WhenShortcutPressed()
        {
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.pKey);
            i.Trigger(input.Shortcuts.ChangePalette.Action);
            yield return new WaitForSeconds(0.1f);
            Assert.That(Object.FindFirstObjectByType<AssetPickerWindow>(), Is.Not.Null);
            Assert.That(Object.FindFirstObjectByType<AssetPickerWindow>().CurrentType, Is.EqualTo(AssetType.Palette));
        }

        [UnityTest]
        public IEnumerator Should_CloseAssetPickerForPalettes_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.ChangePalette.Action);
            yield return new WaitForSeconds(0.1f);
            i.Trigger(input.UI.Cancel.Action);
            yield return new WaitForSeconds(0.1f);
            Assert.That(Object.FindFirstObjectByType<AssetPickerWindow>(FindObjectsInactive.Include).IsOpen, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_CloseAssetPickerForPalettesWithoutLEavingSpriteEditor_WhenShortcutPressed()
        {
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.pKey);
            i.Trigger(input.Shortcuts.ChangePalette.Action);
            yield return new WaitForSeconds(0.1f);
            i.Trigger(input.UI.Cancel.Action);
            yield return new WaitForSeconds(0.1f);
            Assert.That(MenuSwitcher.GetInstance().CurrentMenu, Is.EqualTo(MenuType.SpriteEditor));
        }
    }
}