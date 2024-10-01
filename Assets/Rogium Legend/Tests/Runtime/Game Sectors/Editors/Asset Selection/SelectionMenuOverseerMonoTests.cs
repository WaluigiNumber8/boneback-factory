using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Tests.Core;
using static Rogium.Tests.Editors.AssetCreator;
using static Rogium.Tests.Editors.AssetSelection.SelectionMenuOverseerMonoTestsU;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for working with the Asset Selection Menu.
    /// </summary>
    public class SelectionMenuOverseerMonoTests : MenuTestBase
    {
        private SelectionMenuOverseerMono selectionMenu;

        public override IEnumerator Setup()
        {
            ExternalLibraryOverseer.Instance.ClearPacks();
            yield return base.Setup();
            yield return MenuLoader.PrepareSelectionMenuV2();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
            AddNewPackToLibrary();
            AddNewPackToLibrary();
        }

        [Test]
        public void Should_FillWithPackAssetCards_WhenOpenForPacks()
        {
            selectionMenu.Open(AssetType.Pack);
            Assert.That(selectionMenu.CurrentSelector.Content.childCount, Is.EqualTo(3));
        }

        [Test]
        public void Should_OpenSelectionMenuForPalettes_WhenClickEditOnPackCard()
        {
            OpenPackSelectionAndEditFirstPack();
            Assert.That(selectionMenu.CurrentType, Is.EqualTo(AssetType.Palette));
        }

        [Test]
        public void Should_FillWithPaletteAssetCards_WhenClickEditOnPackCard()
        {
            OpenPackSelectionAndEditFirstPack();
            Assert.That(selectionMenu.CurrentSelector.Content.childCount, Is.EqualTo(1));
        }
        
        [Test]
        public void Should_NotDuplicatePaletteAssetCards_WhenEditPackLeaveAndEditAgain()
        {
            OpenPackSelectionAndEditFirstPack();
            OpenPackSelectionAndEditFirstPack();
            Assert.That(selectionMenu.CurrentSelector.Content.childCount, Is.EqualTo(1));
        }
    }
}