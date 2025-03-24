using System.Collections;
using NUnit.Framework;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using TMPro;
using UnityEngine.TestTools;
using static Rogium.Tests.UI.Interactables.Properties.InteractablesCreator;

namespace Rogium.Tests.UI.Interactables.Properties
{
    /// <summary>
    /// Tests for the Dropdown interactable property.
    /// </summary>
    public class IPDropdownTests : MenuTestBase
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
            IPDropdown dropdown = CreateAndInitDropdown();
            
            yield return null;
            dropdown.GetComponentInChildren<TMP_Dropdown>().onValueChanged.Invoke(1);
            yield return null;

            Assert.That(dropdown.PropertyValue, Is.EqualTo(1));
        }
        
        [UnityTest]
        public IEnumerator WhenValueChanged_Should_AddToActionHistory_WhenClicked()
        {
            IPDropdown dropdown = CreateAndInitDropdown();
            
            yield return null;
            dropdown.GetComponentInChildren<TMP_Dropdown>().onValueChanged.Invoke(1);
            yield return null;
            ActionHistorySystem.ForceEndGrouping();

            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
        
        [UnityTest]
        public IEnumerator UndoLast_Should_RevertValue_WhenClicked()
        {
            IPDropdown dropdown = CreateAndInitDropdown();
            
            yield return null;
            dropdown.GetComponentInChildren<TMP_Dropdown>().onValueChanged.Invoke(1);
            yield return null;
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();
            yield return null;

            Assert.That(dropdown.PropertyValue, Is.EqualTo(0));
        }
    }
}