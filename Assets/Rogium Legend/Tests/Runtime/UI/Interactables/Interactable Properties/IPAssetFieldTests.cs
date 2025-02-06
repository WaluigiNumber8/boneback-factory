using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using Rogium.Tests.Editors;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using Rogium.UserInterface.ModalWindows;
using UnityEngine.TestTools;
using static Rogium.Tests.UI.Interactables.Properties.InteractablesCreator;
using static Rogium.Tests.UI.Interactables.Properties.InteractableUtils;

namespace Rogium.Tests.UI.Interactables.Properties
{
    /// <summary>
    /// Tests for the AssetField interactable property.
    /// </summary>
    public class IPAssetFieldTests : MenuTestBase
    {
        private InteractablePropertyAssetField assetField;

        [UnitySetUp]
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            ActionHistorySystem.ClearHistory();
            TUtilsOverseerLoader.LoadInternalLibrary();
            
            yield return null;
            yield return TUtilsAssetCreator.CreateAndAssignPack();
            TUtilsOverseerLoader.LoadModalWindowBuilder();
            TUtilsOverseerLoader.LoadThemeOverseer();
            
            yield return null;
            assetField = CreateAndInitAssetField(AssetType.Weapon);
            yield return null;
        }
        
        [Test]
        public void OnPointerClick_Should_OpenAssetPickerMenu()
        {
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(TUtilsPointerDataCreator.LeftClick());
            AssetPickerWindow assetPickerWindow = FindFirstAssetPickerWindow();
            
            Assert.That(assetPickerWindow.gameObject.activeSelf, Is.True);
        }
        
        [UnityTest]
        public IEnumerator WhenValueChanged_Should_ClearValue_WhenCleared()
        {
            yield return FillAssetField();
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(TUtilsPointerDataCreator.RightClick());
            
            Assert.That(assetField.PropertyValue, Is.EqualTo(new EmptyAsset()));
        }
        
        [UnityTest]
        public IEnumerator WhenValueChanged_Should_AddToActionHistory_WhenClicked()
        {
            yield return FillAssetField();
            ActionHistorySystem.ForceEndGrouping();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
        
        [UnityTest]
        public IEnumerator UndoLast_Should_RevertValue_WhenClicked()
        {
            yield return FillAssetField();
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();

            Assert.That(assetField.PropertyValue, Is.EqualTo(new EmptyAsset()));
        }

        [UnityTest]
        public IEnumerator WhenValueChanged_Should_AddToActionHistory_WhenCleared()
        {
            yield return FillAssetField();
            ActionHistorySystem.ForceEndGrouping();
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(TUtilsPointerDataCreator.RightClick());
            ActionHistorySystem.ForceEndGrouping();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(2));
        }

        /// <summary>
        /// Fills the asset field with a value.
        /// </summary>
        private IEnumerator FillAssetField()
        {
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(TUtilsPointerDataCreator.LeftClick());
            AssetPickerWindow assetPickerWindow = FindFirstAssetPickerWindow();
            yield return null;
            assetPickerWindow.ConfirmSelection();
        }
    }
}