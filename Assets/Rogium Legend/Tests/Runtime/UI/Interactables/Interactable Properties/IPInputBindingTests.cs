using System.Collections;
using NUnit.Framework;
using RedRats.Systems.Themes;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Input;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;
using UnityEngine.UI;
using static Rogium.Tests.Core.TUtilsModalWindow;
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
        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            OverseerLoader.LoadThemeOverseer();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadModalWindowBuilder();
            yield return null;
            action = input.Player.ButtonMain.Action;
            inputProperty = BuildInputBinding(action);
            yield return null;
        }

        [Test]
        public void Should_SetActionInputString_WhenConstructed()
        {
            Assert.That(inputProperty.InputString, Is.EqualTo(action.bindings[0].ToDisplayString()));
        }

        [Test]
        public void Should_SetActionInputStringAlt_WhenConstructed()
        {
            Assert.That(inputProperty.InputStringAlt, Is.EqualTo(action.bindings[1].ToDisplayString()));
        }
        
        [Test]
        public void Should_SetActionInputStringGamepadAlt_WhenConstructed()
        {
            inputProperty = BuildInputBinding(input.Player.ButtonMain.Action, true, InputDeviceType.Gamepad);
            string inputString = (inputProperty.InputStringAlt == EditorDefaults.Instance.InputEmptyText) ? string.Empty : inputProperty.InputStringAlt;
            Assert.That(inputString, Is.EqualTo(action.bindings[3].ToDisplayString()));
        }

        [Test]
        public void Should_SetActionInputStringAltAsEmpty_WhenConstructedAndAltNotUsed()
        {
            inputProperty = BuildInputBinding(action, false);
            Assert.That(inputProperty.InputStringAlt, Is.Empty);
        }

        [Test]
        public void Should_SetInputReaderAltAsInactive_WhenConstructedAndAltNotUsed()
        {
            inputProperty = BuildInputBinding(action, false);
            Assert.That(!inputProperty.GetComponentsInChildren<InputBindingReader>(true)[1].gameObject.activeSelf, Is.True);
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
            inputProperty = BuildInputBinding(input.Player.ButtonMainAlt.Action);
            Assert.That(inputProperty.Title, Is.EqualTo("Main Alt"));
        }

        [Test]
        public void Should_CreateAlternativeComposite_WhenConstructedAndAltIsUsed()
        {
            inputProperty = BuildInputBinding(input.Player.Movement.Action);
            InputBindingReader[] readers = Object.FindObjectsByType<InteractablePropertyInputBinding>(FindObjectsSortMode.InstanceID)[1].GetComponentsInChildren<InputBindingReader>(true);
            Assert.That(readers[1].InputString, Is.EqualTo(input.Player.Movement.Action.bindings[8].ToDisplayString()));
        }
        
        [UnityTest]
        public IEnumerator Should_RevertBinding_WhenOverrideDeniedForAlt()
        {
            string original = inputProperty.InputStringAlt;
            yield return BindKey(inputProperty.InputReaderAlt, keyboard.bKey);
            yield return null;
            yield return BindKey(inputProperty.InputReader, keyboard.bKey);
            yield return null;
            yield return WindowCancel();
            Assert.That(inputProperty.InputString, Is.EqualTo(original));
        }
        
        [UnityTest]
        public IEnumerator Should_RevertBinding_WhenOverrideDeniedForMain()
        {
            string original = inputProperty.InputString;
            yield return BindKey(inputProperty.InputReader, keyboard.bKey);
            yield return null;
            yield return BindKey(inputProperty.InputReaderAlt, keyboard.bKey);
            yield return null;
            yield return WindowCancel();
            Assert.That(inputProperty.InputStringAlt, Is.EqualTo(original));
        }

        [UnityTest]
        public IEnumerator Should_SetBindingToMainReader_WhenOverridenAndExistsInAlt()
        {
            yield return BindKey(inputProperty.InputReaderAlt, keyboard.bKey);
            yield return null;
            yield return BindKey(inputProperty.InputReader, keyboard.bKey);
            yield return null;
            yield return WindowAccept();
            Assert.That(inputProperty.InputString, Is.EqualTo(keyboard.bKey.displayName.FormatForBindingPath()));
        }
        
        [UnityTest]
        public IEnumerator Should_SetBindingToAltReader_WhenOverridenAndExistsInMain()
        {
            yield return BindKey(inputProperty.InputReader, keyboard.bKey);
            yield return null;
            yield return BindKey(inputProperty.InputReaderAlt, keyboard.bKey);
            yield return null;
            yield return WindowAccept();
            Assert.That(inputProperty.InputStringAlt, Is.EqualTo(keyboard.bKey.displayName.FormatForBindingPath()));
        }
        
        private IEnumerator BindKey(InputBindingReader reader, ButtonControl key)
        {
            reader.StartRebinding();
            yield return new WaitForSecondsRealtime(0.01f);
            i.Press(key);
            yield return new WaitForSecondsRealtime(EditorDefaults.Instance.InputWaitForAnother);
        }
    }
}