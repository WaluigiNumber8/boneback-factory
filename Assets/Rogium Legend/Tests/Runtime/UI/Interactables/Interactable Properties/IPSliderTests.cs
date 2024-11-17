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
    /// Tests for the Slider interactable property.
    /// </summary>
    public class IPSliderTests : MenuTestBase
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
            InteractablePropertySlider slider = CreateAndInitSlider(0f);
            
            yield return null;
            slider.GetComponentInChildren<Slider>().onValueChanged.Invoke(0.5f * 100); //  * 100 because the default decimalMultiplier is 100
            yield return null;

            Assert.That(slider.PropertyValue, Is.EqualTo(0.5f));
        }
        
        [UnityTest]
        public IEnumerator WhenValueChanged_Should_AddToActionHistory_WhenClicked()
        {
            InteractablePropertySlider slider = CreateAndInitSlider(0f);
            
            yield return null;
            slider.GetComponentInChildren<Slider>().onValueChanged.Invoke(0.5f);
            yield return null;
            ActionHistorySystem.ForceEndGrouping();

            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }
        
        [UnityTest]
        public IEnumerator UndoLast_Should_RevertValue_WhenClicked()
        {
            InteractablePropertySlider slider = CreateAndInitSlider(0f);
            
            yield return null;
            slider.GetComponentInChildren<Slider>().onValueChanged.Invoke(0.5f);
            yield return null;
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            yield return null;

            Assert.That(slider.PropertyValue, Is.EqualTo(0f));
        }
    }
}