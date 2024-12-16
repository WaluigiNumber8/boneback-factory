using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsAssetCreator;
using static Rogium.Tests.Editors.AssetSelection.AssetPickingTestsU;

namespace Rogium.Tests.Editors.AssetSelection
{
    /// <summary>
    /// Tests for picking assets.
    /// </summary>
    public class AssetPickingTests : MenuTestBase
    {
        private AssetField assetField;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            yield return null;
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            yield return BuildAssetField(ExternalLibraryOverseer.Instance.Packs[1]);
            assetField = Object.FindFirstObjectByType<AssetField>();
        }
        
        [UnityTest]
        public IEnumerator Should_PickFirstAsset_WhenAssetFieldClicked()
        {
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            yield return PickAssetAndConfirm(0);
            Assert.That(assetField.Value, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0]));
        }
        
        [UnityTest]
        public IEnumerator Should_PickSecondAsset_WhenAssetFieldClicked()
        {
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            yield return PickAssetAndConfirm(1);
            Assert.That(assetField.Value, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[1]));
        }

        [UnityTest]
        public IEnumerator Should_FillAssetPickerWindowWithAssetCards_WhenOpened()
        {
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            Assert.That(Object.FindFirstObjectByType<AssetPickerWindow>().SelectorContent.childCount, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs.Count));
        }

        [UnityTest]
        public IEnumerator Should_HaveOnly1AssetSelected_WhenOpenForSingleSelection()
        {
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            yield return PickAssetAndConfirm(0);
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            yield return PickAssetAndConfirm(1);
            Assert.That(Object.FindFirstObjectByType<AssetPickerWindow>().SelectedAssetsCount, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator Should_ConfirmSelection_WhenClickOnSelectedAssetCard()
        {
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            AssetPickerWindow window = Object.FindFirstObjectByType<AssetPickerWindow>();
            AssetCardController card = window.SelectorContent.GetChild(0).GetComponent<AssetCardController>();
            card.SetToggle(true);
            yield return null;
            card.SetToggle(false);
            card.SetToggle(true);
            yield return null;
            Assert.That(assetField.Value, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0]));
        }

        [UnityTest]
        public IEnumerator Should_ToggleOnAssetCard_WhenPreselectedAssetIsSpecified()
        {
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            AssetPickerWindow window = Object.FindFirstObjectByType<AssetPickerWindow>();
            AssetCardController card = window.SelectorContent.GetChild(1).GetComponent<AssetCardController>();
            Assert.That(card.IsOn, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_SelectEmptyAsset_WhenCanSelectEmptyIsTrue()
        {
            assetField.Construct(AssetType.Pack, ExternalLibraryOverseer.Instance.Packs[0], null, true);
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            yield return PickAssetAndConfirm(0);
            Assert.That(assetField.Value, Is.EqualTo(new EmptyAsset()));
        }

        [UnityTest]
        public IEnumerator Should_AlwaysSpawnOnly1EmptyAssetButton_WhenCanSelectEmptyIsTrue()
        {
            assetField.Construct(AssetType.Pack, ExternalLibraryOverseer.Instance.Packs[0], null, true);
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            yield return PickAssetAndConfirm(0);
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            yield return PickAssetAndConfirm(1);
            Assert.That(assetField.Value, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0]));
        }
    }
}