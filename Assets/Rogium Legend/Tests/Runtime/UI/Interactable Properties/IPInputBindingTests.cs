using System.Collections;
using NUnit.Framework;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using static Rogium.Tests.UI.Interactables.InteractablesCreator;
using InputSystem = Rogium.Systems.Input.InputSystem;

namespace Rogium.Tests.UI.Interactables
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
            action = input.UI.Click.Action.Clone();
            inputBinding = BuildInputBinding(action);
        }

        [Test]
        public void Should_SetActionNameAsTitle_WhenConstructed()
        {
            Assert.That(inputBinding.Title, Is.EqualTo(action.name));
        }

        [UnityTest]
        public IEnumerator Should_BindInputToAction_WhenClicked()
        {
            inputBinding.StartListening();
            yield return null;
            Press(keyboard.spaceKey);
            yield return null;
            foreach (InputBinding binding in action.bindings) Debug.Log(binding);
            Assert.That(action.bindings[0], Is.True);
        }
    }
}