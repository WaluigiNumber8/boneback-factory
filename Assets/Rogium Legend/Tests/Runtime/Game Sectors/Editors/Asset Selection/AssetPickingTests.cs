using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;
using UnityEngine.TestTools;
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
            yield return BuildAssetField();
            assetField = Object.FindFirstObjectByType<AssetField>();
            AssetCreator.AddNewPackToLibrary();
            AssetCreator.AddNewPackToLibrary();
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
    }
}