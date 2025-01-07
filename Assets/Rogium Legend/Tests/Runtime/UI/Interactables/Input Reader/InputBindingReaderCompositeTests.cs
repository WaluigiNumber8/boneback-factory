using System.Collections;
using NUnit.Framework;
using Rogium.Systems.Input;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Tests for the <see cref="InputBindingReader"/> that deals with composite actions.
    /// </summary>
    public class InputBindingReaderCompositeTests : MenuTestWithInputBase
    {
        private Transform bindingParent;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            OverseerLoader.LoadThemeOverseer();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadModalWindowBuilder();
            yield return null;
            bindingParent = new GameObject("Input Bindings").transform;
            bindingParent.SetParent(Object.FindFirstObjectByType<Canvas>().transform);
            UIPropertyBuilder.GetInstance().BuildInputBinding(input.Player.Movement.Action, InputDeviceType.Keyboard, bindingParent);
            yield return null;
        }
        
        [Test]
        public void Should_InstantiateAllPartsOfComposite_WhenCompositeBindingUsed()
        {
            Assert.That(bindingParent.childCount, Is.EqualTo(4));
        }

        [UnityTest]
        public IEnumerator Should_RebindCompositePart_WhenStartListening()
        {
            InputBindingReader reader = bindingParent.GetComponentInChildren<InputBindingReader>();
            yield return BindKey(reader, keyboard.gKey);
            Assert.That(input.Player.Movement.Action.bindings[1].effectivePath, Is.EqualTo(keyboard.gKey.path));
        }

        [Test]
        public void Should_ShowCompositePartInTitle_WhenConstructed()
        {
            InteractablePropertyInputBinding binding = bindingParent.GetComponentInChildren<InteractablePropertyInputBinding>();
            Assert.That(binding.Title, Is.EqualTo("Move Up"));
        }
        
        private IEnumerator BindKey(InputBindingReader reader, KeyControl key)
        {
            reader.StartRebinding();
            yield return new WaitForSecondsRealtime(0.1f);
            i.Press(key);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}