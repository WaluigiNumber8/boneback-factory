using System.Collections;
using NUnit.Framework;
using RedRats.UI.MenuSwitching;
using Rogium.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Editors.NewAssetSelection.UI;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Editors.AssetCreator;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for the top banner of the Asset Selection Menu.
    /// </summary>
    public class TopBannerTests : MenuTestBase
    {
        private SelectionMenuOverseerMono selectionMenu;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return MenuLoader.PrepareSelectionMenu();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            yield return null;
        }

        [UnityTest]
        public IEnumerator ReturnButton_Should_ReturnToMainMenu_WhenClickedOnPackSelection()
        {
            yield return MenuLoader.PrepareMainMenu();
            selectionMenu.Open(AssetType.Pack);
            yield return null;
            SelectionMenuReturnButton returnButton = Object.FindFirstObjectByType<SelectionMenuReturnButton>();
            returnButton.Click();
            yield return null;
            Assert.That(MenuSwitcher.GetInstance().CurrentMenu, Is.EqualTo(MenuType.MainMenu));
        }

        [UnityTest]
        public IEnumerator ReturnButton_Should_ReturnToPackSelection_WhenClickedOnPaletteSelection()
        {
            SelectionMenuOverseerMonoTestsU.OpenPackSelectionAndEditFirstPack();
            yield return null;
            SelectionMenuReturnButton returnButton = Object.FindFirstObjectByType<SelectionMenuReturnButton>();
            returnButton.Click();
            yield return null;
            Assert.That(selectionMenu.CurrentType, Is.EqualTo(AssetType.Pack));
        }
    }
}