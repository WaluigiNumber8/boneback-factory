using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Editors.NewAssetSelection.UI;
using Rogium.Editors.Packs;
using Rogium.Tests.Core;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for the <see cref="PackBanner"/> that info about the currently open pack.
    /// </summary>
    public class PackBannerTests : MenuTestBase
    {
        private SelectionMenuOverseerMono selectionMenu;
        private PackBanner packBanner;
        private PackAsset currentPack;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            yield return MenuLoader.PrepareSelectionMenuV2();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
            packBanner = selectionMenu.GetComponentInChildren<PackBanner>();
            currentPack = ExternalLibraryOverseer.Instance.Packs[0];
            yield return null;
            selectionMenu.Open(AssetType.Pack);
            ((EditableAssetCardControllerV2)selectionMenu.CurrentSelector.GetCard(0)).Edit();
            yield return null;
        }

        [Test]
        public void Should_DisplayPackTitle_WhenLoaded()
        {
            Assert.That(packBanner.Title, Is.EqualTo(currentPack.Title));
        }
    }
}