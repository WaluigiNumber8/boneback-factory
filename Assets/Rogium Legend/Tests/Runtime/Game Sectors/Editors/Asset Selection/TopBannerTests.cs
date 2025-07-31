using System.Collections;
using NUnit.Framework;
using RedRats.UI.MenuSwitching;
using Rogium.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using static Rogium.Tests.Core.TUtilsAssetCreator;

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
            yield return TUtilsMenuLoader.PrepareSelectionMenu();
            selectionMenu = SelectionMenuOverseerMono.Instance;
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            yield return null;
        }

        [UnityTest]
        public IEnumerator ReturnButton_Should_ReturnToMainMenu_WhenClickedOnPackSelection()
        {
            yield return TUtilsMenuLoader.PrepareMainMenu();
            selectionMenu.Open(AssetType.Pack);
            yield return null;
            Button returnButton = GameObject.Find("Top Banner").GetComponentInChildren<Button>();
            returnButton.onClick.Invoke();
            yield return null;
            Assert.That(MenuSwitcher.Instance.CurrentMenu, Is.EqualTo(MenuType.MainMenu));
        }

        [UnityTest]
        public IEnumerator ReturnButton_Should_ReturnToPackSelection_WhenClickedOnPaletteSelection()
        {
            SelectionMenuOverseerMonoTestsU.OpenPackSelectionAndEditFirstPack();
            yield return null;
            Button returnButton = GameObject.Find("Top Banner").GetComponentInChildren<Button>();
            returnButton.onClick.Invoke();
            yield return null;
            Assert.That(selectionMenu.CurrentType, Is.EqualTo(AssetType.Pack));
        }
    }
}