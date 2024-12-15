using System.Collections;
using NUnit.Framework;
using RedRats.UI.MenuSwitching;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsMenuNavigation;

namespace Rogium.Tests.Systems.Shortcuts
{
    /// <summary>
    /// Tests for Selection Menu shortcut actions.
    /// </summary>
    public class ShortcutActionsSelectionMenuTests : MenuTestWithInputBase
    {
        private SelectionMenuOverseerMono menu;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            yield return MenuLoader.PrepareSelectionMenu();
            OverseerLoader.LoadModalWindowBuilder();
            yield return null;
            menu = SelectionMenuOverseerMono.GetInstance();
            yield return OpenSelectionMenu(AssetType.Pack, 0);
        }

        [UnityTest]
        public IEnumerator Should_ReturnToMainMenu_WhenShortcutPressed()
        {
            yield return MenuLoader.PrepareMainMenu();
            i.Trigger(input.Shortcuts.Cancel.Action);
            yield return null;
            yield return null;
            Assert.That(MenuSwitcher.GetInstance().CurrentMenu, Is.EqualTo(MenuType.MainMenu));
        }

        [UnityTest]
        public IEnumerator Should_ReturnToPackSelection_WhenShortcutPressed()
        {
            yield return OpenSelectionMenu(AssetType.Room, 0);
            i.Trigger(input.Shortcuts.Cancel.Action);
            yield return null;
            Assert.That(menu.CurrentType, Is.EqualTo(AssetType.Pack));
        }

        [UnityTest]
        public IEnumerator Should_ShowCreateNewAssetDialog_WhenShortcutPressed()
        {
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.nKey);
            i.Trigger(input.Shortcuts.NewAsset.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Not.Null);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>().IsOpen, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_OpenPropertiesWindow_WhenShortcutPressed()
        {
            menu.CurrentSelector.GetCard(0).SetToggle(true);
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.eKey);
            i.Trigger(input.Shortcuts.EditProperties.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Not.Null);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>().IsOpen, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_NotOpenPropertiesWindow_WhenNoCardSelectedAndShortcutPressed()
        {
            i.Trigger(input.Shortcuts.EditProperties.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Null);
        }

        [UnityTest]
        public IEnumerator Should_EditAsset_WhenShortcutPressed()
        {
            yield return MenuLoader.PrepareRoomEditor(false);
            yield return OpenSelectionMenu(AssetType.Room, 0);
            menu.CurrentSelector.GetCard(0).SetToggle(true);
            i.Trigger(input.Shortcuts.Edit.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(MenuSwitcher.GetInstance().CurrentMenu, Is.EqualTo(MenuType.RoomEditor));
        }

        [UnityTest]
        public IEnumerator Should_NotEditAsset_WhenNoCardSelectedAndShortcutPressed()
        {
            i.Trigger(input.Shortcuts.Edit.Action);
            yield return null;
            Assert.That(MenuSwitcher.GetInstance().CurrentMenu, Is.EqualTo(MenuType.AssetSelection));
        }

        [UnityTest]
        public IEnumerator Should_ShowDeleteDialog_WhenShortcutPressed()
        {
            menu.CurrentSelector.GetCard(0).SetToggle(true);
            i.Trigger(input.Shortcuts.Delete.Action);
            yield return null;
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Not.Null);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>().IsOpen, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_NotShowDeleteDialog_WhenNoCardSelectedAndShortcutPressed()
        {
            i.Trigger(input.Shortcuts.Delete.Action);
            yield return null;
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Null);
        }
    }
}