using System.Collections;
using NUnit.Framework;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine.TestTools;
using UnityEngine.UI;
using static Rogium.Tests.UI.Interactables.InteractablePropertyCreator;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Tests for the interactable properties.
    /// </summary>
    public class InteractablePropertiesTests
    {
        [UnityTest]
        public IEnumerator Toggle_WhenValueChanged_Should_UpdateSelfValue_WhenClicked()
        {
            InteractablePropertyToggle toggle = CreateAndInitToggle();
            
            yield return null;
            toggle.GetComponentInChildren<Toggle>().onValueChanged.Invoke(true);
            yield return null;

            Assert.That(toggle.PropertyValue, Is.True);
        }
    }
}