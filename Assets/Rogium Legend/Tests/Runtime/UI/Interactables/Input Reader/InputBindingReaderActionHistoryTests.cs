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
        
        private IEnumerator BindKey(KeyControl key)
        {
            inputReader.StartRebinding();
            yield return new WaitForSecondsRealtime(0.1f);
            Press(key);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}