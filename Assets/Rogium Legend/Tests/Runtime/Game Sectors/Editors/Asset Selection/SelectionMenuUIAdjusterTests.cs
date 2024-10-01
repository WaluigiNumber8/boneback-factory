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
    /// Tests for working with the Asset Selection Menu.
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
    }
}