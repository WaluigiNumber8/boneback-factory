using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using Rogium.UserInterface.ModalWindows;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using static Rogium.Tests.UI.Interactables.InteractablePropertyCreator;

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
        
        [UnitySetUp]
        public IEnumerator Setup()
        {
            SceneLoader.LoadUIScene();
            ActionHistorySystem.ClearHistory();
            Object.Instantiate(internalLibraryPrefab);
            yield return null;
            Object.Instantiate(modalWindowBuilderPrefab);
            Object.Instantiate(themeOverseerPrefab);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator OnPointerClick_Should_OpenAssetPickerMenu()
        {
            InteractablePropertyAssetField assetField = CreateAndInitAssetField(AssetType.Weapon);
            
            yield return null;
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(new PointerEventData(EventSystem.current));
            yield return null;
            AssetPickerWindow assetPickerWindow = Object.FindFirstObjectByType<AssetPickerWindow>(FindObjectsInactive.Include);
            
            Assert.That(assetPickerWindow.gameObject.activeSelf, Is.True);
        }
        
        [UnityTest]
        public IEnumerator WhenValueChanged_Should_AddToActionHistory_WhenClicked()
        {
            InteractablePropertyAssetField assetField = CreateAndInitAssetField(AssetType.Weapon);
            
            yield return null;
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(new PointerEventData(EventSystem.current));
            yield return null;
            ActionHistorySystem.ForceEndGrouping();

            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
        
        [UnityTest]
        public IEnumerator UndoLast_Should_RevertValue_WhenClicked()
        {
            InteractablePropertyAssetField assetField = CreateAndInitAssetField(AssetType.Weapon);
            
            yield return null;
            assetField.GetComponentInChildren<AssetField>().OnPointerClick(new PointerEventData(EventSystem.current));
            yield return null;
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            yield return null;

            Assert.That(assetField.PropertyValue, Is.Null);
        }
    }
}