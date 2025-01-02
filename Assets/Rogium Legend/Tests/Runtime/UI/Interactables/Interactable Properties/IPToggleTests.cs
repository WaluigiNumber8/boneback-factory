using System.Collections;
using NUnit.Framework;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine.TestTools;
using UnityEngine.UI;
using static Rogium.Tests.UI.Interactables.Properties.InteractablesCreator;

namespace Rogium.Tests.UI.Interactables.Properties
{
    /// <summary>
    /// Tests for the Toggle interactable property.
    /// </summary>
    public class IPToggleTests : MenuTestBase
    {
        [UnitySetUp]
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            ActionHistorySystem.ClearHistory();
        }
        
        [UnityTest]
        public IEnumerator WhenValueChanged_Should_UpdateSelfValue_WhenClicked()
        {
            InteractablePropertyToggle toggle = CreateAndInitToggle();
            
            yield return null;
            toggle.GetComponentInChildren<Toggle>().onValueChanged.Invoke(true);
            yield return null;

            Assert.That(toggle.PropertyValue, Is.True);
        }
        
        [UnityTest]
        public IEnumerator WhenValueChanged_Should_AddToActionHistory_WhenClicked()
        {
            InteractablePropertyToggle toggle = CreateAndInitToggle();
            
            yield return null;
            toggle.GetComponentInChildren<Toggle>().onValueChanged.Invoke(true);
            yield return null;
            ActionHistorySystem.ForceEndGrouping();

            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator UndoLast_Should_RevertValue_WhenClicked()
        {
            InteractablePropertyToggle toggle = CreateAndInitToggle();
            
            yield return null;
            toggle.GetComponentInChildren<Toggle>().onValueChanged.Invoke(true);
            yield return null;
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();
            yield return null;

            Assert.That(toggle.PropertyValue, Is.False);
        }
    }
}