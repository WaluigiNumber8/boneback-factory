using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Editors.AssetCreator;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for the <see cref="SelectionMenuUIAdjuster"/> that adjusts the UI of the Selection Menu.
    /// </summary>
    public class SelectionMenuUIAdjusterTests : MenuTestBase
    {
        private SelectionMenuOverseerMono selectionMenu;
        private SelectionMenuUIAdjuster adjuster;

        public override IEnumerator Setup()
        {
            ExternalLibraryOverseer.Instance.ClearPacks();
            yield return base.Setup();
            yield return MenuLoader.PrepareSelectionMenuV2();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            yield return null;
            adjuster = Object.FindFirstObjectByType<SelectionMenuUIAdjuster>();
        }

        [UnityTest]
        public IEnumerator Should_DisablePackBanner_WhenSelectionMenuIsOpenForPacks()
        {
            selectionMenu.Open(AssetType.Pack);
            yield return null;
            Assert.That(adjuster.PackBannerActive, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_DisableCategoryTabs_WhenSelectionMenuIsOpenForPacks()
        {
            selectionMenu.Open(AssetType.Pack);
            yield return null;
            Assert.That(adjuster.CategoryTabsActive, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_DisableOtherAssetRects_WhenSelectionMenuIsOpenForPacks()
        {
            selectionMenu.Open(AssetType.Pack);
            yield return null;
            Assert.That(adjuster.OtherAssetScrollRectsActive, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_EnablePackScrollRect_WhenSelectionMenuIsOpenForPacks()
        {
            selectionMenu.Open(AssetType.Pack);
            yield return null;
            Assert.That(adjuster.PackScrollRectActive, Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_EnablePackBanner_WhenSelectionMenuIsOpenForPalettes()
        {
            selectionMenu.Open(AssetType.Palette);
            yield return null;
            Assert.That(adjuster.PackBannerActive, Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_EnableCategoryTabs_WhenSelectionMenuIsOpenForPalettes()
        {
            selectionMenu.Open(AssetType.Palette);
            yield return null;
            Assert.That(adjuster.CategoryTabsActive, Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_EnableOtherAssetRects_WhenSelectionMenuIsOpenForPalettes()
        {
            selectionMenu.Open(AssetType.Palette);
            yield return null;
            Assert.That(adjuster.OtherAssetScrollRectsActive, Is.True);
        }
    }
}