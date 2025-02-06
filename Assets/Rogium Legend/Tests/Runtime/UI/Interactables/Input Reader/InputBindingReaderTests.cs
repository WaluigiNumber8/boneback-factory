using System.Collections;
using NUnit.Framework;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Input;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;
using UnityEngine.UI;
using static Rogium.Tests.Core.TUtilsModalWindow;
using static Rogium.Tests.UI.Interactables.InputReader.InputBindingReaderTestsU;

namespace Rogium.Tests.UI.Interactables.InputReader
{
    /// <summary>
    /// Tests for the <see cref="InputBindingReader"/>.
    /// </summary>
    public class InputBindingReaderTests : MenuTestWithInputBase
    {
        private InputBindingReader inputReader;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            TUtilsOverseerLoader.LoadThemeOverseer();
            TUtilsOverseerLoader.LoadUIBuilder();
            TUtilsOverseerLoader.LoadModalWindowBuilder();
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
            inputReader.StartRebinding();
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
            inputReader.StartRebinding();
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
        public IEnumerator Should_BeBoundToNewInput_WhenClicked()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputReader.Binding.DisplayString, Is.EqualTo(keyboard.spaceKey.displayName));
        }
        
        [UnityTest]
        public IEnumerator Should_BeBoundToDifferentInput_WhenClicked()
        {
            InputBinding original = inputReader.Binding.Button;
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputReader.Binding, Is.Not.EqualTo(original));
        }
        
        [UnityTest]
        public IEnumerator Should_Refresh_BoundInputText_WhenRebound()
        {
            yield return BindKey(keyboard.spaceKey);
            Assert.That(inputReader.InputString, Is.EqualTo(keyboard.spaceKey.displayName));
        }

        [UnityTest]
        public IEnumerator Should_Timeout_WhenNotBoundInTime()
        {
            InputBinding original = inputReader.Binding.Button;
            inputReader.StartRebinding();
            yield return new WaitForSecondsRealtime(EditorDefaults.Instance.InputTimeout);
            Assert.That(inputReader.Binding.DisplayString, Is.EqualTo(original.ToDisplayString()));
            Assert.That(inputReader.BindingDisplay.activeSelf, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_ShowMessage_WhenBindingIsAlreadyUsed()
        {
            yield return BindKey(keyboard.spaceKey);
            yield return new WaitForSecondsRealtime(1f);
            Assert.That(IsModalWindowActive(), Is.True);
        }
        
        [UnityTest]
        public IEnumerator Should_NotShowMessage_WhenBindingIsNotUsed()
        {
            yield return BindKey(keyboard.oKey);
            Assert.That(IsModalWindowActive(), Is.True);
        }

        [UnityTest]
        public IEnumerator Should_RemoveBindingFromDuplicate_WhenOverride()
        {
            yield return BindKey(keyboard.spaceKey);
            yield return WindowAccept();
            yield return null;
            Assert.That(input.Player.ButtonDash.Action.bindings[0].ToDisplayString(), Is.EqualTo(""));
        }
        
        [UnityTest]
        public IEnumerator Should_SetBindingToEditedOne_WhenOverride()
        {
            yield return BindKey(keyboard.spaceKey);
            yield return WindowAccept();
            yield return null;
            Assert.That(input.Player.ButtonMain.Action.bindings[0].ToDisplayString(), Is.EqualTo("Space"));
        }
        
        [UnityTest]
        public IEnumerator Should_RemoveSameBindingFromAlt_WhenOverride()
        {
            InputBindingReader altReader = inputReader.transform.parent.GetChild(inputReader.transform.parent.childCount-1).GetComponent<InputBindingReader>();
            altReader.Rebind(new InputBindingCombination.Builder().WithButton("<Mouse>/leftButton").Build());
            yield return null;
            yield return BindKey(mouse.leftButton);
            yield return WindowAccept();
            yield return null;
            Assert.That(input.Player.ButtonMain.Action.bindings[1].ToDisplayString(), Is.EqualTo(""));
        }
        
        [UnityTest]
        public IEnumerator Should_RevertBinding_WhenOverrideDenied()
        {
            InputBindingReader inputReader2 = BuildInputReader(input.Player.ButtonDash.Action);
            yield return null;
            string original = inputReader.InputString;
            string original2 = inputReader2.InputString;
            yield return BindKey(keyboard.spaceKey);
            yield return WindowCancel();
            yield return null;
            Assert.That(inputReader.InputString, Is.EqualTo(original));
            Assert.That(inputReader2.InputString, Is.EqualTo(original2));
        }
        
        [UnityTest]
        public IEnumerator Should_RevertBinding_WhenOverrideDeniedAfterSuccessfulRebind()
        {
            InputBindingReader inputReader2 = BuildInputReader(input.Player.ButtonDash.Action);
            yield return null;
            yield return BindKey(keyboard.iKey);
            string original = inputReader.InputString;
            string original2 = inputReader2.InputString;
            yield return BindKey(keyboard.spaceKey);
            Object.FindFirstObjectByType<ModalWindow>().OnDeny();
            yield return null;
            Assert.That(inputReader.InputString, Is.EqualTo(original));
            Assert.That(inputReader2.InputString, Is.EqualTo(original2));
        }

        [UnityTest]
        public IEnumerator Should_DisableAllOtherInputReaders_WhenStartListening()
        {
            InputBindingReader inputReader2 = BuildInputReader(input.Player.ButtonDash.Action);
            yield return null;
            inputReader.StartRebinding();
            yield return null;
            Assert.That(inputReader2.GetComponentInChildren<Button>().interactable, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_EnableAllOtherInputReaders_WhenStopListening()
        {
            InputBindingReader inputReader2 = BuildInputReader(input.Player.ButtonDash.Action);
            yield return null;
            yield return BindKey(keyboard.iKey);
            Assert.That(inputReader2.GetComponentInChildren<Button>().interactable, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_ShowEmptySymbol_WhenNoBindingAssigned()
        {
            InputBindingReader inputReader2 = BuildInputReader(input.Player.ButtonDash.Action);
            yield return null;
            yield return BindKey(keyboard.spaceKey);
            yield return WindowAccept();
            Assert.That(inputReader2.InputString, Is.EqualTo(EditorDefaults.Instance.InputEmptyText));
        }
        
        [UnityTest]
        public IEnumerator Should_ClearBinding_WhenCleared()
        {
            inputReader.OnPointerClick(TUtilsPointerDataCreator.RightClick());
            yield return null;
            Assert.That(inputReader.InputString, Is.EqualTo(EditorDefaults.Instance.InputEmptyText));
        }

        [UnityTest]
        public IEnumerator Should_ShowOverrideDialog_WhenOverrideFromLinkedActionMap()
        {
            inputReader = BuildInputReader(input.Shortcuts.RefreshCurrent.Action);
            yield return null;
            yield return BindKey(keyboard.eKey);
            Assert.That(IsModalWindowActive(), Is.True);
        }

        private IEnumerator BindKey(ButtonControl key)
        {
            inputReader.StartRebinding();
            yield return new WaitForSecondsRealtime(0.01f);
            i.Press(key);
            yield return new WaitForSecondsRealtime(EditorDefaults.Instance.InputWaitForAnother);
        }
    }
}