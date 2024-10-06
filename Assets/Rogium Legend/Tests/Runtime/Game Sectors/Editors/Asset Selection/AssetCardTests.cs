using System.Collections;
using NUnit.Framework;
using RedRats.Systems.Themes;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Tests.Core;
using UnityEngine.UI;
using static Rogium.Tests.Editors.AssetCreator;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for working with the <see cref="EditableAssetCardControllerV2"/>.
    /// </summary>
    public class AssetCardTests : MenuTestBase
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
        public void Should_SetAssetsNameToCardTitle_WhenOpen()
        {
            selectionMenu.Open(AssetType.Pack);
            Assert.That(selectionMenu.CurrentSelector.Content.GetChild(1).GetComponent<EditableAssetCardControllerV2>().Title, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Title));
        }

        [Test]
        public void Should_SetAssetsIconToCardIcon_WhenOpen()
        {
            selectionMenu.Open(AssetType.Pack);
            Assert.That(selectionMenu.CurrentSelector.Content.GetChild(1).GetComponent<EditableAssetCardControllerV2>().Icon, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Icon));
        }

        [Test]
        public void Should_ShowInfoGroup_WhenMenuOpened()
        {
            selectionMenu.Open(AssetType.Pack);
            EditableAssetCardControllerV2 card = selectionMenu.CurrentSelector.Content.GetChild(1).GetComponent<EditableAssetCardControllerV2>();
            Assert.That(card.IsInfoGroupShown, Is.True);
            Assert.That(card.IsButtonGroupShown, Is.False);
        }
        
        [Test]
        public void Should_ToggleToButtonGroup_WhenCardClicked()
        {
            selectionMenu.Open(AssetType.Pack);
            EditableAssetCardControllerV2 card = selectionMenu.CurrentSelector.Content.GetChild(1).GetComponent<EditableAssetCardControllerV2>();
            card.SetToggle(true);
            Assert.That(card.IsInfoGroupShown, Is.False);
            Assert.That(card.IsButtonGroupShown, Is.True);
        }

        [Test]
        public void Should_ToggleToInfoGroup_WhenCardClickedTwice()
        {
            selectionMenu.Open(AssetType.Pack);
            EditableAssetCardControllerV2 card = selectionMenu.CurrentSelector.Content.GetChild(1).GetComponent<EditableAssetCardControllerV2>();
            card.SetToggle(true);
            card.SetToggle(false);
            Assert.That(card.IsInfoGroupShown, Is.True);
            Assert.That(card.IsButtonGroupShown, Is.False);
        }

        [Test]
        public void Should_ToggleOffOtherCards_WhenCardClicked()
        {
            selectionMenu.Open(AssetType.Pack);
            EditableAssetCardControllerV2 card1 = selectionMenu.CurrentSelector.Content.GetChild(1).GetComponent<EditableAssetCardControllerV2>();
            EditableAssetCardControllerV2 card2 = selectionMenu.CurrentSelector.Content.GetChild(2).GetComponent<EditableAssetCardControllerV2>();
            card1.SetToggle(true);
            card2.SetToggle(true);
            card1.SetToggle(true);
            Assert.That(card1.IsOn, Is.True);
            Assert.That(card2.IsOn, Is.False);
        }

        [Test]
        public void Should_SetProperToggleSpritesBasedOnCurrentTheme_WhenMenuOpened()
        {
            ThemeOverseerMono.GetInstance().ChangeTheme(ThemeType.Green);
            selectionMenu.Open(AssetType.Weapon);
            Toggle cardToggle = selectionMenu.CurrentSelector.Content.GetChild(1).GetComponent<Toggle>();
            Assert.That(cardToggle.image.sprite, Is.EqualTo(ThemeOverseerMono.GetInstance().CurrentThemeData.Interactables.assetCard.normal));
        }
    }
}