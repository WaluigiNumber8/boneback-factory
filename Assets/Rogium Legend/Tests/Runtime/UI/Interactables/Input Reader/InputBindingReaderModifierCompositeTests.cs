using System.Collections;
using NUnit.Framework;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Input;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;

namespace Rogium.Tests.UI.Interactables.InputReader
{
    /// <summary>
    /// Tests for the <see cref="InputBindingReader"/> and it's setup for accepting optional modifiers.
    /// </summary>
    public class InputBindingReaderModifierCompositeTests : MenuTestWithInputBase
    {
        private Transform bindingParent;
        private InputBindingReader inputReader;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            TUtilsOverseerLoader.LoadThemeOverseer();
            TUtilsOverseerLoader.LoadUIBuilder();
            TUtilsOverseerLoader.LoadModalWindowBuilder();
            yield return null;
            bindingParent = new GameObject("Input Bindings").transform;
            bindingParent.SetParent(Object.FindFirstObjectByType<Canvas>().transform);
            UIPropertyBuilder.GetInstance().BuildInputBinding(input.Shortcuts.Undo.Action, InputDeviceType.Keyboard, bindingParent);
            inputReader = bindingParent.GetComponentInChildren<InputBindingReader>();
            yield return null;
        }

        [Test]
        public void Should_InstantiateOnly1Reader_WhenCreated()
        {
            Assert.That(bindingParent.childCount, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator Should_BindFirstModifier_WhenCombinationPressed()
        {
            yield return BindKey(keyboard.leftAltKey, null, keyboard.gKey);
            Assert.That(inputReader.Action.bindings[1].effectivePath, Is.EqualTo(keyboard.leftAltKey.path.FormatForBindingPath()));
        }

        [UnityTest]
        public IEnumerator Should_BindSecondModifier_WhenCombinationPressed()
        {
            yield return BindKey(keyboard.leftAltKey, keyboard.leftCtrlKey, keyboard.gKey);
            Assert.That(inputReader.Action.bindings[2].effectivePath, Is.EqualTo(keyboard.leftCtrlKey.path.FormatForBindingPath()));
        }

        [UnityTest]
        public IEnumerator Should_BindButton_WhenCombinationPressed()
        {
            yield return BindKey(keyboard.leftAltKey, keyboard.leftCtrlKey, keyboard.gKey);
            Assert.That(inputReader.Action.bindings[3].effectivePath, Is.EqualTo(keyboard.gKey.path.FormatForBindingPath()));
        }
        
        [UnityTest]
        public IEnumerator Should_BindButton_WhenNoModifiersPressed()
        {
            yield return BindKey(null, null, keyboard.gKey);
            Assert.That(inputReader.Action.bindings[3].effectivePath, Is.EqualTo(keyboard.gKey.path.FormatForBindingPath()));
        }

        [UnityTest]
        public IEnumerator Should_ShowMessage_WhenBindingWithModifiersIsAlreadyUsed()
        {
            yield return BindKey(keyboard.leftCtrlKey, null, keyboard.zKey);
            yield return new WaitForSecondsRealtime(1f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>().transform.GetChild(0).gameObject.activeSelf, Is.True);
        }
        
        private IEnumerator BindKey(KeyControl modifier1, KeyControl modifier2, KeyControl key)
        {
            inputReader.StartRebinding();
            yield return new WaitForSecondsRealtime(0.01f);
            if (modifier1 != null) i.Press(modifier1);
            yield return null;
            if (modifier2 != null) i.Press(modifier2);
            yield return null;
            i.Press(key);
            yield return new WaitForSecondsRealtime(EditorDefaults.Instance.InputWaitForAnother);
        }
    }
}