using System.Collections;
using RedRats.Core;
using Rogium.UserInterface.Interactables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;

namespace Rogium.Systems.Input
{
    /// <summary>
    /// Overseers all input profiles and deals with their switching.
    /// </summary>
    [DefaultExecutionOrder(-50)]
    public class InputSystem : PersistentMonoSingleton<InputSystem>
    {
        private EventSystem eventSystem;
        private RogiumInputActions input;
        
        private InputProfilePlayer inputPlayer;
        private InputProfileUI inputUI;
        private InputProfilePause inputPause;
        private InputProfileShortcuts inputShortcuts;
        
        private Vector2 pointerPosition;

        protected override void Awake()
        {
            base.Awake();
            ClearAllInput();
            SceneManager.sceneLoaded += (_, __) => eventSystem = FindFirstObjectByType<EventSystem>();
            inputUI.PointerPosition.OnPressed += UpdatePointerPosition;
            inputPause.Disable();
        }

        public void ClearAllInput()
        {
            Shortcuts?.Disable();
            input = new RogiumInputActions();
            inputPlayer = new InputProfilePlayer(input);
            inputUI = new InputProfileUI(input);
            inputPause = new InputProfilePause(input);
            inputShortcuts = new InputProfileShortcuts(input);
            Shortcuts.Enable();
        }

        public void SwitchToGameplayMaps()
        {
            UI.Disable();
            Shortcuts.Disable();
            Player.Enable();
        }
        
        public void SwitchToMenuMaps()
        {
            Player.Disable();
            Shortcuts.Enable();
            UI.Enable();
        }

        public (InputAction, InputBindingCombination) FindDuplicateBinding(InputAction action, InputBindingCombination bindingCombo)
        {
            bool usesModifier1 = bindingCombo.Modifier1.HasValue;
            bool usesModifer2 = bindingCombo.Modifier2.HasValue;
            for (int i = 0; i < action.actionMap.bindings.Count; i++)
            {
                InputBinding binding = action.actionMap.bindings[i];
                if (!binding.effectivePath.Equals(bindingCombo.Button.effectivePath) || binding.id == bindingCombo.Button.id) continue;
                if (usesModifier1 && (!action.actionMap.bindings[i - 2].effectivePath.Equals(bindingCombo.Modifier1.Path) || action.actionMap.bindings[i - 2].id == bindingCombo.Modifier1.ID)) continue;
                if (usesModifer2 && (!action.actionMap.bindings[i - 1].effectivePath.Equals(bindingCombo.Modifier2.Path) || action.actionMap.bindings[i - 1].id == bindingCombo.Modifier2.ID)) continue;

                InputAction foundAction = input.FindAction(binding.action);
                InputBindingCombination foundCombination = new((usesModifier1) ? action.actionMap.bindings[i-2] : new InputBinding(""), 
                                                               (usesModifer2) ? action.actionMap.bindings[i-1] : new InputBinding(""), 
                                                               action.actionMap.bindings[i]);
                return (foundAction, foundCombination);
            }

            return (null, new InputBindingCombination());
        }

        /// <summary>
        /// Disables all input for a specified amount of time.
        /// </summary>
        /// <param name="caller">The <see cref="MonoBehaviour"/> that called for this method.</param>
        /// <param name="delay">How long to suspend all input for.</param>
        public void DisableInput(MonoBehaviour caller, float delay)
        {
            caller.StartCoroutine(DisableAllCoroutine(delay));
            IEnumerator DisableAllCoroutine(float delayTime)
            {
                eventSystem.sendNavigationEvents = false;
                yield return new WaitForSecondsRealtime(delayTime);
                eventSystem.sendNavigationEvents = true;
            }
        }
        
        public InputAction GetAction(InputAction action) => input.FindAction(action.name);
        
        private void UpdatePointerPosition(Vector2 value) => pointerPosition = value;
        
        public Vector2 PointerPosition { get => pointerPosition; }
        public InputProfilePlayer Player { get => inputPlayer; }
        public InputProfileUI UI { get => inputUI; }
        public InputProfilePause Pause { get => inputPause; }
        public InputProfileShortcuts Shortcuts { get => inputShortcuts; }
    }
}