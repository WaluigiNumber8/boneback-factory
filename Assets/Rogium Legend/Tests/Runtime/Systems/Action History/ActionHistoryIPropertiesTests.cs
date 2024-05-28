using System.Collections;
using NUnit.Framework;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using UnityEngine.UI;
using static Rogium.Tests.UI.Interactables.InteractablePropertyCreator;

namespace Rogium.Tests.Systems.ActionHistory
{
    /// <summary>
    /// Tests for when Interactive Properties interact with the Action History System.
    /// </summary>
    [RequiresPlayMode]
    public class ActionHistoryIPropertiesTests
    {
        [SetUp]
        public void Setup()
        {
            SceneLoader.LoadUIScene();
            ActionHistorySystem.ClearHistory();
        }

        [UnityTest]
        public IEnumerator Toggle_WhenValueChanged_Should_AddToActionHistory_WhenClicked()
        {
            InteractablePropertyToggle toggle = CreateAndInitToggle();
            
            yield return null;
            toggle.GetComponentInChildren<Toggle>().onValueChanged.Invoke(true);
            yield return null;
            ActionHistorySystem.ForceEndGrouping();

            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator UndoLast_Should_RevertToggleValue_WhenClicked()
        {
            InteractablePropertyToggle toggle = CreateAndInitToggle();
            
            yield return null;
            toggle.GetComponentInChildren<Toggle>().onValueChanged.Invoke(true);
            yield return null;
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            yield return null;

            Assert.That(toggle.PropertyValue, Is.False);
        }
    }
}