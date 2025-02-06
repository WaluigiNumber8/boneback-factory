using System.Collections;
using NUnit.Framework;
using RedRats.UI.MenuSwitching;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.InputSystem;
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
            yield return TUtilsMenuLoader.PrepareSelectionMenu();
            TUtilsOverseerLoader.LoadModalWindowBuilder();
            yield return null;
            menu = SelectionMenuOverseerMono.GetInstance();
            yield return OpenSelectionMenu(AssetType.Pack, 0);
        }

        [UnityTest]
        public IEnumerator Should_ReturnToMainMenu_WhenShortcutPressed()
        {
            yield return TUtilsMenuLoader.PrepareMainMenu();
            i.Trigger(input.UI.Cancel.Action);
            yield return null;
            yield return null;
            Assert.That(MenuSwitcher.GetInstance().CurrentMenu, Is.EqualTo(MenuType.MainMenu));
        }

        [UnityTest]
        public IEnumerator Should_ReturnToPackSelection_WhenShortcutPressed()
        {
            yield return OpenSelectionMenu(AssetType.Room, 0);
            i.Trigger(input.UI.Cancel.Action);
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
        public IEnumerator Should_NotOpenPropertiesWindow_WhenNoCardSelectedAndShortcutPressed()
        {
            i.Trigger(input.Shortcuts.EditProperties.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Null);
        }

        [UnityTest]
        public IEnumerator Should_EditAsset_WhenShortcutPressed()
        {
            yield return TUtilsMenuLoader.PrepareRoomEditor(false);
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

        [UnityTest]
        public IEnumerator Should_ShowPaletteSelection_WhenShortcutPressed()
        {
            yield return OpenSelectionMenu(AssetType.Enemy, 0);
            i.Trigger(input.Shortcuts.ShowPalettes.Action);
            yield return null;
            Assert.That(menu.CurrentType, Is.EqualTo(AssetType.Palette));
        }

        [UnityTest]
        public IEnumerator Should_ShowSpriteSelection_WhenShortcutPressed()
        {
            yield return OpenSelectionMenu(AssetType.Enemy, 0);
            i.Trigger(input.Shortcuts.ShowSprites.Action);
            yield return null;
            Assert.That(menu.CurrentType, Is.EqualTo(AssetType.Sprite));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowWeaponSelection_WhenShortcutPressed()
        {
            yield return OpenSelectionMenu(AssetType.Enemy, 0);
            i.Trigger(input.Shortcuts.ShowWeapons.Action);
            yield return null;
            Assert.That(menu.CurrentType, Is.EqualTo(AssetType.Weapon));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowProjectileSelection_WhenShortcutPressed()
        {
            yield return OpenSelectionMenu(AssetType.Enemy, 0);
            i.Trigger(input.Shortcuts.ShowProjectiles.Action);
            yield return null;
            Assert.That(menu.CurrentType, Is.EqualTo(AssetType.Projectile));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowEnemySelection_WhenShortcutPressed()
        {
            yield return OpenSelectionMenu(AssetType.Room, 0);
            i.Trigger(input.Shortcuts.ShowEnemies.Action);
            yield return null;
            Assert.That(menu.CurrentType, Is.EqualTo(AssetType.Enemy));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowRoomSelection_WhenShortcutPressed()
        {
            yield return OpenSelectionMenu(AssetType.Enemy, 0);
            i.Trigger(input.Shortcuts.ShowRooms.Action);
            yield return null;
            Assert.That(menu.CurrentType, Is.EqualTo(AssetType.Room));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowTileSelection_WhenShortcutPressed()
        {
            yield return OpenSelectionMenu(AssetType.Enemy, 0);
            i.Trigger(input.Shortcuts.ShowTiles.Action);
            yield return null;
            Assert.That(menu.CurrentType, Is.EqualTo(AssetType.Tile));
        }

        [UnityTest]
        public IEnumerator Should_NotShowSpriteSelection_WhenInPackSelectionAndShortcutPressed()
        {
            i.Trigger(input.Shortcuts.ShowSprites.Action);
            yield return null;
            Assert.That(menu.CurrentType, Is.EqualTo(AssetType.Pack));
        }
    }
}