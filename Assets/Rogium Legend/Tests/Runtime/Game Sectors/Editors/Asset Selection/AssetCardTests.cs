using System.Collections;
using NUnit.Framework;
using RedRats.Core;
using RedRats.Core.Utils;
using RedRats.Systems.Themes;
using RedRats.UI.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using Rogium.UserInterface.ModalWindows;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using static Rogium.Tests.Editors.AssetCreator;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for working with the <see cref="EditableAssetCardController"/>.
    /// </summary>
    public class AssetCardTests : MenuTestBase
    {
        private SelectionMenuOverseerMono selectionMenu;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return MenuLoader.PrepareSelectionMenu();
            selectionMenu = SelectionMenuOverseerMono.GetInstance();
            AddNewPackToLibrary();
            AddNewPackToLibrary();
        }
        
          [Test]
        public void Should_SetAssetsNameToCardTitle_WhenOpen()
        {
            selectionMenu.Open(AssetType.Pack);
            Assert.That(selectionMenu.CurrentSelector.GetCard(0).Title, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Title));
        }

        [Test]
        public void Should_SetAssetsIconToCardIcon_WhenOpen()
        {
            selectionMenu.Open(AssetType.Pack);
            Assert.That(selectionMenu.CurrentSelector.GetCard(0).Icon, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Icon));
        }

        [Test]
        public void Should_ShowInfoGroup_WhenMenuOpened()
        {
            selectionMenu.Open(AssetType.Pack);
            EditableAssetCardController card = (EditableAssetCardController)selectionMenu.CurrentSelector.GetCard(0);
            Assert.That(card.IsInfoGroupShown, Is.True);
            Assert.That(card.IsButtonGroupShown, Is.False);
        }
        
        [Test]
        public void Should_ToggleToButtonGroup_WhenCardClicked()
        {
            selectionMenu.Open(AssetType.Pack);
            EditableAssetCardController card = (EditableAssetCardController) selectionMenu.CurrentSelector.GetCard(0);
            card.SetToggle(true);
            Assert.That(card.IsInfoGroupShown, Is.False);
            Assert.That(card.IsButtonGroupShown, Is.True);
        }

        [Test]
        public void Should_ToggleToInfoGroup_WhenCardClickedTwice()
        {
            selectionMenu.Open(AssetType.Pack);
            EditableAssetCardController card = (EditableAssetCardController) selectionMenu.CurrentSelector.GetCard(0);
            card.SetToggle(true);
            card.SetToggle(false);
            Assert.That(card.IsInfoGroupShown, Is.True);
            Assert.That(card.IsButtonGroupShown, Is.False);
        }

        [Test]
        public void Should_ToggleOffOtherCards_WhenCardClicked()
        {
            selectionMenu.Open(AssetType.Pack);
            AssetCardController card1 = selectionMenu.CurrentSelector.GetCard(0);
            AssetCardController card2 = selectionMenu.CurrentSelector.GetCard(1);
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
            Toggle cardToggle = selectionMenu.CurrentSelector.GetCard(0).GetComponent<Toggle>();
            Assert.That(cardToggle.image.sprite, Is.EqualTo(ThemeOverseerMono.GetInstance().CurrentThemeData.Interactables.assetCard.normal));
        }

        [Test]
        public void Should_SetProperFontForInfoBasedOnCurrentTheme_WhenMenuOpened()
        {
            ThemeOverseerMono.GetInstance().ChangeTheme(ThemeType.Green);
            selectionMenu.Open(AssetType.Weapon);
            TextMeshProUGUI titleText = selectionMenu.CurrentSelector.GetCard(0).GetComponentInChildren<TextMeshProUGUI>();
            FontInfo fontInfo = ThemeOverseerMono.GetInstance().CurrentThemeData.Fonts.assetCardInfo;
            Assert.That(titleText.font, Is.EqualTo(fontInfo.font));
            Assert.That(titleText.fontSize, Is.EqualTo(fontInfo.size));
            Assert.That(titleText.color, Is.EqualTo(fontInfo.color));
        }

        [UnityTest]
        public IEnumerator Should_HaveProperShimmerColorOnMaterial_WhenMenuOpened()
        {
            selectionMenu.Open(AssetType.Weapon);
            yield return null;
            Color shimmerColor = selectionMenu.CurrentSelector.GetCard(0).GetComponentInChildren<MaterialExtractor>().Get().GetColor("_ShimmerColor");
            Color targetColor = ThemeOverseerMono.GetInstance().GetThemeData(ThemeType.Green).Colors.shimmerEffects;
            yield return null;
            Assert.That(shimmerColor.r.IsSameAs(targetColor.r), Is.True);
            Assert.That(shimmerColor.g.IsSameAs(targetColor.g), Is.True);
            Assert.That(shimmerColor.b.IsSameAs(targetColor.b), Is.True);
            Assert.That(shimmerColor.a.IsSameAs(targetColor.a), Is.True);
        }

        [UnityTest]
        public IEnumerator Should_HaveProperShimmerColorOnMaterial_WhenAssetPickerWindowOpened()
        {
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            yield return null;
            UIPropertyBuilder.GetInstance().BuildAssetField("Test", AssetType.Weapon, null, Object.FindFirstObjectByType<Canvas>().transform, null);
            yield return null;
            yield return AssetPickingTestsU.ClickAssetFieldToOpenAssetPickerWindow();
            Color shimmerColor = Object.FindFirstObjectByType<AssetPickerWindow>().SelectorContent.GetChild(0).GetComponentInChildren<MaterialExtractor>().Get().GetColor("_ShimmerColor");
            Color targetColor = ThemeOverseerMono.GetInstance().GetThemeData(ThemeType.Green).Colors.shimmerEffects;
            yield return null;
            Assert.That(shimmerColor.r.IsSameAs(targetColor.r), Is.True);
            Assert.That(shimmerColor.g.IsSameAs(targetColor.g), Is.True);
            Assert.That(shimmerColor.b.IsSameAs(targetColor.b), Is.True);
            Assert.That(shimmerColor.a.IsSameAs(targetColor.a), Is.True);
        }
    }
}