using System.Collections;
using RedRats.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Rogium.Systems.Input
{
    /// <summary>
    /// Overseers all input profiles and deals with their switching.
    /// </summary>
    public class InputSystem : PersistentMonoSingleton<InputSystem>
    {
        private EventSystem eventSystem;
        private RogiumInputActions input;
        private InputProfilePlayer inputPlayer;
        private InputProfileUI inputUI;
        
        private Vector2 pointerPosition;

        protected override void Awake()
        {
            base.Awake();
            input = new RogiumInputActions();
            inputPlayer = new InputProfilePlayer(input);
            inputUI = new InputProfileUI(input);
            SceneManager.sceneLoaded += (_, __) => eventSystem = FindFirstObjectByType<EventSystem>();
            inputUI.PointerPosition.OnPressed += UpdatePointerPosition;
        }

        /// <summary>
        /// Enables the UI Action Map.
        /// </summary>
        public void EnableUIMap()
        {
            DisableAll();
            inputUI.Enable();
        }
        
        /// <summary>
        /// Enables the Player Action Map.
        /// </summary>
        public void EnablePlayerMap()
        {
            DisableAll();
            inputPlayer.Enable();
        }
        
        public (InputAction, int) FindDuplicateBinding(InputAction action, int bindingIndex)
        {
            InputBinding newBinding = action.bindings[bindingIndex];
            foreach (InputBinding binding in action.actionMap.bindings)
            {
                if (binding.effectivePath.Equals(newBinding.effectivePath) && newBinding.id != binding.id)
                {
                    InputAction foundAction = input.FindAction(binding.action);
                    int foundIndex = foundAction.bindings.IndexOf(b => b == binding);
                    return (foundAction, foundIndex);
                }
            }
            return (null, -1);
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
        
        /// <summary>
        /// Disables all Action Maps except UI.
        /// </summary>
        private void DisableAll()
        {
            inputPlayer.Disable();
        }
        
        private void UpdatePointerPosition(Vector2 value) => pointerPosition = value;

        public Vector2 PointerPosition { get => pointerPosition; }
        public InputProfilePlayer Player { get => inputPlayer; }
        public InputProfileUI UI { get => inputUI; }
        public string KeyboardSchemeGroup { get => input.KeyboardMouseScheme.bindingGroup; }
        public string GamepadSchemeGroup { get => input.GamepadScheme.bindingGroup; }
    }
}