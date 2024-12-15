using System.Collections;
using NUnit.Framework;
using RedRats.UI.MenuSwitching;
using Rogium.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Tests.Core;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsMenuNavigation;

namespace Rogium.Tests.Systems.Shortcuts
{
    /// <summary>
    /// Tests for Selection Menu shortcut actions.
    /// </summary>
    public class ShortcutActionsSelectionMenuTests : MenuTestWithInputBase
    {
        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            yield return MenuLoader.PrepareSelectionMenu();
            OverseerLoader.LoadModalWindowBuilder();
            yield return null;
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
            Assert.That(SelectionMenuOverseerMono.GetInstance().CurrentType, Is.EqualTo(AssetType.Pack));
        }
    }
}