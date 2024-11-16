using System.Collections;
using NUnit.Framework;
using Rogium.Systems.Input;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Tests for the <see cref="InputBindingReader"/> that deals with composite actions.
    /// </summary>
    public class InputBindingReaderCompositeTests : MenuTestWithInputBase
    {
        private InputSystem input;
        private Transform bindingParent;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            OverseerLoader.LoadThemeOverseer();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadModalWindowBuilder();
            input = InputSystem.GetInstance();
            input.ClearAllInput();
            yield return null;
            bindingParent = new GameObject("Input Bindings").transform;
            bindingParent.SetParent(Object.FindFirstObjectByType<Canvas>().transform);
        }
        
        [UnityTest]
        public IEnumerator Should_InstantiateAllPartsOfComposite_WhenCompositeBindingUsed()
        {
            UIPropertyBuilder.GetInstance().BuildInputBinding(input.Player.Movement.Action, InputDeviceType.Keyboard, bindingParent);
            yield return null;
            Assert.That(bindingParent.childCount, Is.EqualTo(4));
        }
    }
}