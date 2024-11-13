using System.Collections;
using NUnit.Framework;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;
using UnityEngine.UI;
using static Rogium.Tests.UI.Interactables.InputBindingReaderTestsU;
using InputSystem = Rogium.Systems.Input.InputSystem;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Tests for the <see cref="InputBindingReader"/>.
    /// </summary>
    public class InputBindingReaderTests : MenuTestWithInputBase
    {
        private InputBindingReader inputReader;
        private InputSystem input;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            OverseerLoader.LoadThemeOverseer();
            OverseerLoader.LoadUIBuilder();
            input = InputSystem.GetInstance();
            yield return null;
            inputReader = BuildInputReader(input.Player.ButtonMain.Action);
        }
        
        [Test]
        public void Should_SetActionInputString_WhenConstructed()
        {
            Assert.That(inputReader.InputString, Is.EqualTo(input.Player.ButtonMain.Action.bindings[0].ToDisplayString()));
        }
        
        [Test]
        public void Should_SetButtonAsDisabled_WhenConstructedAndDisabled()
        {
            inputReader.SetActive(false);
            Assert.That(inputReader.GetComponentInChildren<Button>().interactable, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_ShowBindingDisplay_WhenStartListening()
        {
            inputReader.StartListening();
            yield return null;
            Assert.That(inputReader.BindingDisplay.activeSelf, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_HideBindingDisplay_WhenStopListening()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputReader.BindingDisplay.activeSelf, Is.False);
        }
        
        [UnityTest]
        public IEnumerator ShouldHideBoundInputDisplay_WhenStartListening()
        {
            inputReader.StartListening();
            yield return null;
            Assert.That(inputReader.BoundInputDisplay.activeSelf, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_ShowBoundInputDisplay_WhenStopListening()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputReader.BoundInputDisplay.activeSelf, Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_HideBindingText_WhenStopListening()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputReader.BindingDisplay.activeSelf, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_BeBoundToNewKeyboardInput_WhenClicked()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputReader.Binding, Is.Not.EqualTo(keyboard.spaceKey));
        }
        
        [UnityTest]
        public IEnumerator Should_BeBoundToDifferentInput_WhenClicked()
        {
            InputBinding original = inputReader.Binding;
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputReader.Binding, Is.Not.EqualTo(original));
        }
        
        [UnityTest]
        public IEnumerator Should_Refresh_BoundInputText_WhenRebound()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputReader.InputString, Is.EqualTo(keyboard.spaceKey.displayName));
        }
        
        private IEnumerator BindKey(KeyControl key)
        {
            inputReader.StartListening();
            yield return new WaitForSecondsRealtime(0.1f);
            Press(key);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}