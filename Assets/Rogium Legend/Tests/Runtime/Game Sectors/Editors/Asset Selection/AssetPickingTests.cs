using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.NewAssetSelection;
using Rogium.Tests.Core;
using Rogium.UserInterface.Editors.AssetSelection.PickerVariant;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
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
        }
        
        [UnityTest]
        public IEnumerator Should_PickAsset_WhenAssetFieldClicked()
        {
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            yield return PickFirstAssetAndConfirm();
            Assert.That(assetField.Value, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0]));
        }

        [UnityTest]
        public IEnumerator Should_FillAssetPickerWindowWithAssetCards_WhenOpened()
        {
            yield return ClickAssetFieldToOpenAssetPickerWindow();
            Assert.That(Object.FindFirstObjectByType<AssetPickerWindow>().SelectorContent.childCount, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs.Count));
        }
    }
}