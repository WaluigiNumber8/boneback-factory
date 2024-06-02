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
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using static Rogium.Tests.UI.Interactables.InteractablesCreator;
using static Rogium.Tests.UI.Interactables.InteractableUtils;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Tests for the AssetField interactable property.
    /// </summary>
    [RequiresPlayMode]
    public class IPAssetFieldTests
    {
        private readonly GameObject internalLibraryPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/pref_InternalLibrary.prefab");
        private readonly ModalWindowBuilder modalWindowBuilderPrefab = AssetDatabase.LoadAssetAtPath<ModalWindowBuilder>("Assets/Rogium Legend/Prefabs/Global/Builders/pref_Builder_ModalWindows.prefab");
        private readonly GameObject themeOverseerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/Overseers/pref_Overseer_Themes.prefab");
        
        private InteractablePropertyAssetField assetField;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            SceneLoader.LoadUIScene();
            ActionHistorySystem.ClearHistory();
            Object.Instantiate(internalLibraryPrefab);
            AssetCreator.CreateAndAssignPack();
            yield return null;
            Object.Instantiate(modalWindowBuilderPrefab);
            Object.Instantiate(themeOverseerPrefab);
            yield return null;
            assetField = CreateAndInitAssetField(AssetType.Weapon);
            yield return null;
        }
        
        [Test]
        public void OnPointerClick_Should_OpenAssetPickerMenu()
        {
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(new PointerEventData(EventSystem.current));
            AssetPickerWindow assetPickerWindow = FindFirstAssetPickerWindow();
            
            Assert.That(assetPickerWindow.gameObject.activeSelf, Is.True);
        }
        
        [UnityTest]
        public IEnumerator WhenValueChanged_Should_ClearValue_WhenCleared()
        {
            yield return FillAssetField();
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(new PointerEventData(EventSystem.current) {button = PointerEventData.InputButton.Right});
            
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
            ActionHistorySystem.UndoLast();

            Assert.That(assetField.PropertyValue, Is.EqualTo(new EmptyAsset()));
        }

        [UnityTest]
        public IEnumerator WhenValueChanged_Should_AddToActionHistory_WhenCleared()
        {
            yield return FillAssetField();
            ActionHistorySystem.ForceEndGrouping();
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(new PointerEventData(EventSystem.current) {button = PointerEventData.InputButton.Right});
            ActionHistorySystem.ForceEndGrouping();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(2));
        }

        /// <summary>
        /// Fills the asset field with a value.
        /// </summary>
        private IEnumerator FillAssetField()
        {
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(new PointerEventData(EventSystem.current));
            AssetPickerWindow assetPickerWindow = FindFirstAssetPickerWindow();
            yield return null;
            assetPickerWindow.ConfirmSelection();
        }
    }
}