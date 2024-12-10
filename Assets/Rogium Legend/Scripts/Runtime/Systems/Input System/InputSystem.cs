using System.Collections;
using RedRats.Core;
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
            EnableShortcutsMap();
        }

        public void ClearAllInput()
        {
            input = new RogiumInputActions();
            inputPlayer = new InputProfilePlayer(input);
            inputUI = new InputProfileUI(input);
            inputPause = new InputProfilePause(input);
            inputShortcuts = new InputProfileShortcuts(input);
        }

        public void EnableUIMap()
        {
            DisableAll();
            inputUI.Enable();
        }
        
        public void EnablePlayerMap()
        {
            DisableAll();
            inputPlayer.Enable();
        }
        
        public void EnablePauseMap()
        {
            DisableAll();
            inputPause.Enable();
        }
        
        public void EnableShortcutsMap()
        {
            inputShortcuts.Enable();
        }

        public void DisablePauseMap() => inputPause.Disable();

        public (InputAction, int) FindDuplicateBinding(InputAction action, int bindingIndex)
        {
            InputBinding newBinding = action.bindings[bindingIndex];
            foreach (InputBinding binding in action.actionMap.bindings)
            {
                if (binding.effectivePath.Equals(newBinding.effectivePath) && binding.id != newBinding.id)
                {
                    InputAction foundAction = input.FindAction(binding.action);
                    int foundIndex = foundAction.bindings.IndexOf(b => b.id == binding.id);
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
        
        public int GetBindingIndexByDevice(InputAction action, InputDeviceType device, bool getSecondary = false)
        {
            return device switch
            {
                InputDeviceType.Keyboard => GetBindingIndex(new InputBinding(groups: KeyboardSchemeGroup, path: default)),
                InputDeviceType.Gamepad => GetBindingIndex(new InputBinding(groups: GamepadSchemeGroup, path: default)),
                _ => throw new System.ArgumentOutOfRangeException(nameof(device), device, null)
            };

            int GetBindingIndex(InputBinding group)
            {
                ReadOnlyArray<InputBinding> bindings = action.bindings;
                bool waitForComposite = false;
                for (int i = 0; i < bindings.Count; ++i)
                {
                    InputBinding b = bindings[i];
                    if (b.isComposite) waitForComposite = false;
                    if (!group.Matches(b)) continue;
                    if (waitForComposite) continue;
                    if (getSecondary)
                    {
                        getSecondary = false;
                        if (b.isPartOfComposite) waitForComposite = true;
                        continue;
                    }
                    return i;
                }
                return -1;
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
        public InputProfilePause Pause { get => inputPause; }
        public InputProfileShortcuts ShortcutsGeneral { get => inputShortcuts; }
        private string KeyboardSchemeGroup { get => input.KeyboardMouseScheme.bindingGroup; }
        private string GamepadSchemeGroup { get => input.GamepadScheme.bindingGroup; }
    }
}