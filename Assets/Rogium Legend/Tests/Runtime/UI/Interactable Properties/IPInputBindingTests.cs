using System;
using System.Collections;
using System.Linq;
using NUnit.Framework;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine.InputSystem;
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
        public IEnumerator Should_BindInputToAction_WhenClicked()
        {
            InputBinding original = GetBindingForActionMap(action, input.KeyboardSchemeGroup);
            inputBinding.StartListening();
            yield return null;
            Press(keyboard.spaceKey);
            yield return null;
            Assert.That(GetBindingForActionMap(action, input.KeyboardSchemeGroup), Is.Not.EqualTo(original));
        }
        
        private static InputBinding GetBindingForActionMap(InputAction action, string schemeGroup)
        {
            foreach (InputBinding binding in action.bindings.Where(binding => binding.groups.Contains(schemeGroup)))
            {
                return binding;
            }
            throw new Exception($"No binding found for {action.name}.");
        }
    }
}