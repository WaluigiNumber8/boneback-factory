using System.Collections;
using NUnit.Framework;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using TMPro;
using UnityEngine.TestTools;
using static Rogium.Tests.UI.Interactables.InteractablePropertyCreator;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Tests for the InputField interactable property.
    /// </summary>
    [RequiresPlayMode]
    public class IPInputFieldTests
    {
        [SetUp]
        public void Setup()
        {
            SceneLoader.LoadUIScene();
            ActionHistorySystem.ClearHistory();
        }
        
        [UnityTest]
        public IEnumerator WhenValueChanged_Should_UpdateSelfValue_WhenClicked()
        {
            InteractablePropertyInputField inputField = CreateAndInitInputField();
            
            yield return null;
            inputField.GetComponentInChildren<TMP_InputField>().onEndEdit.Invoke("Test");
            yield return null;

            Assert.That(inputField.PropertyValue, Is.EqualTo("Test"));
        }
        
        [UnityTest]
        public IEnumerator WhenValueChanged_Should_AddToActionHistory_WhenClicked()
        {
            InteractablePropertyInputField inputField = CreateAndInitInputField();
            
            yield return null;
            inputField.GetComponentInChildren<TMP_InputField>().onEndEdit.Invoke("Test");
            yield return null;
            ActionHistorySystem.ForceEndGrouping();

            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
        
        [UnityTest]
        public IEnumerator UndoLast_Should_RevertValue_WhenClicked()
        {
            InteractablePropertyInputField inputField = CreateAndInitInputField();
            
            yield return null;
            inputField.GetComponentInChildren<TMP_InputField>().onEndEdit.Invoke("Test");
            yield return null;
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            yield return null;

            Assert.That(inputField.PropertyValue, Is.EqualTo(""));
        }
    }
}