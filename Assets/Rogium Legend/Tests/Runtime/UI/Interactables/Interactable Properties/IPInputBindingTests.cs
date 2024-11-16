using System.Collections;
using NUnit.Framework;
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
        private InteractablePropertyInputBinding inputBinding;
        private InputAction action;
        private InputSystem input;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            OverseerLoader.LoadThemeOverseer();
            OverseerLoader.LoadUIBuilder();
            input = InputSystem.GetInstance();
            yield return null;
            action = input.Player.ButtonMain.Action.Clone();
            inputBinding = BuildInputBinding(action);
        }

        [Test]
        public void Should_SetActionNameAsTitle_WhenConstructed()
        {
            Assert.That(inputBinding.Title, Is.EqualTo(action.name));
        }

        [Test]
        public void Should_SetActionInputStringKeyboard_WhenConstructed()
        {
            Assert.That(inputBinding.KeyboardInputString, Is.EqualTo(action.bindings[0].ToDisplayString()));
        }
        
        [Test]
        public void Should_SetButtonsAsDisabled_WhenConstructed()
        {
            inputBinding.SetDisabled(true);
            foreach (Button button in inputBinding.GetComponentsInChildren<Button>())
            {
                Assert.That(button.interactable, Is.False);
            }
        }
    }
}