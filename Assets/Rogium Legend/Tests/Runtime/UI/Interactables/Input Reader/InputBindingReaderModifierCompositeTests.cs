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
    /// Tests for the <see cref="InputBindingReader"/> and it's setup for accepting optional modifiers.
    /// </summary>
    public class InputBindingReaderModifierCompositeTests : MenuTestWithInputBase
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
            UIPropertyBuilder.GetInstance().BuildInputBinding(input.Shortcuts.Undo.Action, InputDeviceType.Keyboard, bindingParent);
            yield return null;
        }

        [Test]
        public void Should_InstantiateOnly1Reader_WhenCreated()
        {
            Assert.That(bindingParent.childCount, Is.EqualTo(1));
        }
    }
}