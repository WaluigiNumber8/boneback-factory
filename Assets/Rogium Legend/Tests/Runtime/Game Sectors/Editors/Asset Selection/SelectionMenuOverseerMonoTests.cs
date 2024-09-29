using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Tests.Core;
using static Rogium.Tests.Editors.AssetCreator;

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
        public void Should_SetAssetsNameToCardTitle_WhenOpen()
        {
            selectionMenu.Open(AssetType.Pack);
            Assert.That(selectionMenu.CurrentSelector.Content.GetChild(0).GetComponent<AssetCardControllerV2>().Title, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Title));
        }

        [Test]
        public void Should_SetAssetsIconToCardIcon_WhenOpen()
        {
            selectionMenu.Open(AssetType.Pack);
            Assert.That(selectionMenu.CurrentSelector.Content.GetChild(0).GetComponent<AssetCardControllerV2>().Icon, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Icon));
        }

        [Test]
        public void Should_ShowInfoGroup_WhenMenuOpened()
        {
            selectionMenu.Open(AssetType.Pack);
            AssetCardControllerV2 card = selectionMenu.CurrentSelector.Content.GetChild(0).GetComponent<AssetCardControllerV2>();
            Assert.That(card.IsInfoGroupShown, Is.True);
            Assert.That(card.IsButtonGroupShown, Is.False);
        }
        
        [Test]
        public void Should_ToggleToButtonGroup_WhenCardClicked()
        {
            selectionMenu.Open(AssetType.Pack);
            AssetCardControllerV2 card = selectionMenu.CurrentSelector.Content.GetChild(0).GetComponent<AssetCardControllerV2>();
            card.SetToggle(true);
            Assert.That(card.IsInfoGroupShown, Is.False);
            Assert.That(card.IsButtonGroupShown, Is.True);
        }

        [Test]
        public void Should_ToggleToInfoGroup_WhenCardClickedTwice()
        {
            selectionMenu.Open(AssetType.Pack);
            AssetCardControllerV2 card = selectionMenu.CurrentSelector.Content.GetChild(0).GetComponent<AssetCardControllerV2>();
            card.SetToggle(true);
            card.SetToggle(false);
            Assert.That(card.IsInfoGroupShown, Is.True);
            Assert.That(card.IsButtonGroupShown, Is.False);
        }
    }
}