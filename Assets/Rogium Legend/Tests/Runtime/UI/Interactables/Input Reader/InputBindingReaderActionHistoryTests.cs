using System.Collections;
using NUnit.Framework;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.TestTools;
using static Rogium.Tests.UI.Interactables.InputReader.InputBindingReaderTestsU;

namespace Rogium.Tests.UI.Interactables.InputReader
{
    /// <summary>
    /// Tests for interactions between <see cref="InputBindingReader"/> and <see cref="ActionHistorySystem"/>.
    /// </summary>
    public class InputBindingReaderActionHistoryTests : MenuTestWithInputBase
    {
        private InputBindingReader reader;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            TUtilsOverseerLoader.LoadThemeOverseer();
            TUtilsOverseerLoader.LoadUIBuilder();
            TUtilsOverseerLoader.LoadModalWindowBuilder();
            yield return null;
            reader = BuildInputReader(input.Player.ButtonMain.Action);
        }
        
        [UnityTest]
        public IEnumerator Should_AddToActionHistory_WhenRebound()
        {
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator Should_UndoRebinding_WhenUndone()
        {
            string originalKey = reader.Binding.DisplayString;
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();
            Assert.That(reader.Binding.DisplayString, Is.EqualTo(originalKey));
        }

        [UnityTest]
        public IEnumerator Should_NotEqualToNewKey_WhenUndone()
        {
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            string reboundKey = reader.Binding.DisplayString;
            ActionHistorySystem.Undo();
            Assert.That(reader.Binding.DisplayString, Is.Not.EqualTo(reboundKey));
        }

        [UnityTest]
        public IEnumerator Should_RedoRebinding_WhenRedone()
        {
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            string reboundKey = reader.Binding.DisplayString;
            ActionHistorySystem.Undo();
            ActionHistorySystem.Redo();
            Assert.That(reader.Binding.DisplayString, Is.EqualTo(reboundKey));
        }

        [UnityTest]
        public IEnumerator Should_NotEqualToOriginalKey_WhenRedone()
        {
            string originalKey = reader.Binding.DisplayString;
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();
            ActionHistorySystem.Redo();
            Assert.That(reader.Binding.DisplayString, Is.Not.EqualTo(originalKey));
        }

        [UnityTest]
        public IEnumerator Should_RefreshDisplayedInputString_WhenUndone()
        {
            string originalSymbol = reader.InputString;
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.Undo();
            Assert.That(reader.InputString, Is.EqualTo(originalSymbol));
        }

        [UnityTest]
        public IEnumerator Should_RevertActiveReader_WhenDuplicateOverridenAndUndone()
        {
            BuildInputReader(input.Player.ButtonMainAlt.Action);
            yield return BindKey(mouse.rightButton);
            Object.FindFirstObjectByType<ModalWindow>().OnAccept();
            yield return null;
            ActionHistorySystem.Undo();
            Assert.That(reader.InputString, Is.EqualTo("LMB"));
        }
        
        [UnityTest]
        public IEnumerator Should_RevertDuplicateReader_WhenDuplicateOverridenAndUndone()
        {
            InputBindingReader reader2 = BuildInputReader(input.Player.ButtonMainAlt.Action);
            yield return BindKey(mouse.rightButton);
            Object.FindFirstObjectByType<ModalWindow>().OnAccept();
            yield return null;
            ActionHistorySystem.Undo();
            Assert.That(reader2.InputString, Is.EqualTo("RMB"));
        }

        [UnityTest]
        public IEnumerator Should_RevertActionBinding_WhenReaderIsInactiveAndUndone()
        {
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            reader.gameObject.SetActive(false);
            ActionHistorySystem.Undo();
            yield return null;
            Assert.That(reader.InputString, Is.EqualTo("LMB"));
        }
        
        [UnityTest]
        public IEnumerator Should_NotKeepNewAction_WhenReaderIsInactiveAndUndone()
        {
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            reader.gameObject.SetActive(false);
            ActionHistorySystem.Undo();
            yield return null;
            Assert.That(reader.InputString, Is.Not.EqualTo("L"));
        }

        [UnityTest]
        public IEnumerator Should_NotAddToActionHistory_WhenDuplicateFoundAndRevertNewAction()
        {
            BuildInputReader(input.Player.ButtonMainAlt.Action);
            yield return BindKey(mouse.rightButton);
            Object.FindFirstObjectByType<ModalWindow>().OnDeny();
            yield return null;
            ActionHistorySystem.ForceEndGrouping();
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(0));
        }
        
        private IEnumerator BindKey(ButtonControl key)
        {
            reader.StartRebinding();
            yield return new WaitForSecondsRealtime(0.01f);
            i.Press(key);
            yield return new WaitForSecondsRealtime(EditorDefaults.Instance.InputWaitForAnother);
        }
    }
}