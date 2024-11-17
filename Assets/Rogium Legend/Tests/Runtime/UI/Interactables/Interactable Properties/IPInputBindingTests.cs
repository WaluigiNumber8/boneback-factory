using System.Collections;
using NUnit.Framework;
using RedRats.Systems.Themes;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Rogium.Tests.UI.Interactables.Properties.InteractablesCreator;
using InputSystem = Rogium.Systems.Input.InputSystem;

namespace Rogium.Tests.UI.Interactables.Properties
{
    /// <summary>
    /// Tests for the Input Binding interactable property.
    /// </summary>
    public class IPInputBindingTests : MenuTestWithInputBase
    {
        private InteractablePropertyInputBinding inputProperty;
        private InputAction action;
        private InputSystem input;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            OverseerLoader.LoadThemeOverseer();
            OverseerLoader.LoadUIBuilder();
            input = InputSystem.GetInstance();
            yield return null;
            action = input.Player.ButtonMainAlt.Action.Clone();
            inputProperty = BuildInputBinding(action);
        }

        [Test]
        public void Should_SetActionInputStringKeyboard_WhenConstructed()
        {
            Assert.That(inputProperty.KeyboardInputString, Is.EqualTo(action.bindings[0].ToDisplayString()));
        }
        
        [Test]
        public void Should_SetButtonsAsDisabled_WhenConstructed()
        {
            inputProperty.SetDisabled(true);
            foreach (Button button in inputProperty.GetComponentsInChildren<Button>())
            {
                Assert.That(button.interactable, Is.False);
            }
        }

        [Test]
        public void Should_SetProperTheme_WhenConstructed()
        {
            ThemeOverseerMono.GetInstance().ChangeTheme(ThemeType.Red);
            inputProperty = BuildInputBinding(action);
            Assert.That(inputProperty.GetComponentInChildren<Button>().image.sprite, Is.EqualTo(ThemeOverseerMono.GetInstance().GetThemeData(ThemeType.Red).Interactables.inputBinding.normal));
        }
        
        [Test]
        public void Should_AddSpacesToTitle_WhenConstructed()
        {
            Assert.That(inputProperty.Title, Is.EqualTo("Main Alt"));
        }
    }
}