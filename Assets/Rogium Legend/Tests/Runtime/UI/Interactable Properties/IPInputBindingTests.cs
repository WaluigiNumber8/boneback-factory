using System.Collections;
using NUnit.Framework;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;
using UnityEngine.UI;
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
            action = input.Player.ButtonMain.Action.Clone();
            inputBinding = BuildInputBinding(action);
        }

        [Test]
        public void Should_SetActionNameAsTitle_WhenConstructed()
        {
            Assert.That(inputBinding.Title, Is.EqualTo(action.name));
        }

        [Test]
        public void Should_SetActionInputString_WhenConstructed()
        {
            Assert.That(inputBinding.InputString, Is.EqualTo(action.bindings[0].ToDisplayString()));
        }

        [Test]
        public void Should_SetButtonAsDisabled_WhenConstructed()
        {
            inputBinding.SetDisabled(true);
            Assert.That(inputBinding.GetComponentInChildren<Button>().interactable, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_BeBoundToNewInput_WhenClicked()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputBinding.PropertyValue.bindings[0], Is.Not.EqualTo(keyboard.spaceKey));
        }
        
        [UnityTest]
        public IEnumerator Should_BeBoundToDifferentInput_WhenClicked()
        {
            InputBinding original = inputBinding.PropertyValue.bindings[0];
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputBinding.PropertyValue.bindings[0], Is.Not.EqualTo(original));
        }

        [UnityTest]
        public IEnumerator Should_ShowBindingDisplay_WhenStartListening()
        {
            inputBinding.StartListening();
            yield return null;
            Assert.That(inputBinding.BindingDisplay.activeSelf, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_HideBindingDisplay_WhenStopListening()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputBinding.BindingDisplay.activeSelf, Is.False);
        }
        
        [UnityTest]
        public IEnumerator ShouldHideBoundInputDisplay_WhenStartListening()
        {
            inputBinding.StartListening();
            yield return null;
            Assert.That(inputBinding.BoundInputDisplay.activeSelf, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_ShowBoundInputDisplay_WhenStopListening()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputBinding.BoundInputDisplay.activeSelf, Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_HideBindingText_WhenStopListening()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputBinding.BindingDisplay.activeSelf, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_Refresh_BoundInputText_WhenRebound()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputBinding.InputString, Is.EqualTo(keyboard.spaceKey.displayName));
        }

        private IEnumerator BindKey(KeyControl key)
        {
            inputBinding.StartListening();
            yield return new WaitForSecondsRealtime(0.1f);
            Press(key);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}