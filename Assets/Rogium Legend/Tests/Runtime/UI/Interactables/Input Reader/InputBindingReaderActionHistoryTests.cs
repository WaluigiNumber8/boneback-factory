using System.Collections;
using NUnit.Framework;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.ActionHistory;
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
    /// Tests for interactions between <see cref="InputBindingReader"/> and <see cref="ActionHistorySystem"/>.
    /// </summary>
    public class InputBindingReaderActionHistoryTests : MenuTestWithInputBase
    {
        private InputBindingReader inputReader;
        private InputSystem input;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            OverseerLoader.LoadThemeOverseer();
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadModalWindowBuilder();
            input = InputSystem.GetInstance();
            input.ClearAllInput();
            yield return null;
            inputReader = BuildInputReader(input.Player.ButtonMain.Action);
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
            string originalKey = inputReader.Binding.effectivePath;
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            Assert.That(inputReader.Binding.effectivePath, Is.EqualTo(originalKey));
        }

        [UnityTest]
        public IEnumerator Should_NotEqualToNewKey_WhenUndone()
        {
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            string reboundKey = inputReader.Binding.effectivePath;
            ActionHistorySystem.UndoLast();
            Assert.That(inputReader.Binding.effectivePath, Is.Not.EqualTo(reboundKey));
        }

        [UnityTest]
        public IEnumerator Should_RedoRebinding_WhenRedone()
        {
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            string reboundKey = inputReader.Binding.effectivePath;
            ActionHistorySystem.UndoLast();
            ActionHistorySystem.RedoLast();
            Assert.That(inputReader.Binding.effectivePath, Is.EqualTo(reboundKey));
        }

        [UnityTest]
        public IEnumerator Should_NotEqualToOriginalKey_WhenRedone()
        {
            string originalKey = inputReader.Binding.effectivePath;
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            ActionHistorySystem.RedoLast();
            Assert.That(inputReader.Binding.effectivePath, Is.Not.EqualTo(originalKey));
        }

        [UnityTest]
        public IEnumerator Should_RefreshDisplayedInputString_WhenUndone()
        {
            string originalSymbol = inputReader.InputString;
            yield return BindKey(keyboard.lKey);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            Assert.That(inputReader.InputString, Is.EqualTo(originalSymbol));
        }
        
        private IEnumerator BindKey(KeyControl key)
        {
            inputReader.StartRebinding();
            yield return new WaitForSecondsRealtime(0.1f);
            Press(key);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}