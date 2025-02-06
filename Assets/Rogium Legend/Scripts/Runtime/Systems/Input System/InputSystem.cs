using System;
using System.Collections;
using System.Collections.Generic;
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
    [DefaultExecutionOrder(-50)]
    public class InputSystem : PersistentMonoSingleton<InputSystem>
    {
        [SerializeField] private LinkedActionMapsAsset linkedActionMaps;
        
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
            UI.PointerPosition.OnPressed += UpdatePointerPosition;
            linkedActionMaps?.RefreshDictionary();
        }

        public void ClearAllInput()
        {
            UI?.Disable();
            Player?.Disable();
            Pause?.Disable();
            Shortcuts?.Disable();
            input = new RogiumInputActions();
            inputPlayer = new InputProfilePlayer(input);
            inputUI = new InputProfileUI(input);
            inputPause = new InputProfilePause(input);
            inputShortcuts = new InputProfileShortcuts(input);
            SwitchToMenuMaps();
        }

        public void SwitchToGameplayMaps()
        {
            Shortcuts.Disable();
            UI.Disable();
            Player.Enable();
        }
        
        public void SwitchToMenuMaps()
        {
            Player.Disable();
            UI.Enable();
            Shortcuts.Enable();
        }

        /// <summary>
        /// Tries to find a duplicate binding combination in the same action map.
        /// </summary>
        /// <param name="action">The action containing the binding combination of interest.</param>
        /// <param name="bindingCombo">The binding combination to find duplicate for</param>
        /// <returns>The duplicate combination, the action it's a part of and the binding's index.</returns>
        public (InputAction, InputBindingCombination, int) FindDuplicateBinding(InputAction action, InputBindingCombination bindingCombo)
        {
            bool usesModifiers = bindingCombo.Modifier1.effectivePath != "" || bindingCombo.Modifier2.effectivePath != "";
            
            //Get all action maps of interest
            ISet<InputActionMap> actionMaps = new HashSet<InputActionMap>();
            actionMaps.Add(action.actionMap);
            if (linkedActionMaps != null && linkedActionMaps.GetLinkedMaps.TryGetValue(action.actionMap.name, out ISet<string> maps))
            {
                foreach (string mapName in maps)
                {
                    InputActionMap map = input.asset.FindActionMap(mapName);
                    if (map != null) actionMaps.Add(map);
                }
            }

            foreach (InputActionMap map in actionMaps)
            {
                for (int i = 0; i < map.bindings.Count; i++)
                {
                    InputBinding binding = map.bindings[i];
                    if (!binding.effectivePath.Equals(bindingCombo.Button.effectivePath) || binding.id == bindingCombo.Button.id) continue;
                    if (HasNoModifiersButIsModifierComposite(binding, map, i - 2)) continue;
                    if (HasNoModifiersButIsModifierComposite(binding, map, i - 1)) continue;
                    if (HasModifiersButNotSame(bindingCombo.Modifier1, map, i - 2)) continue;
                    if (HasModifiersButNotSame(bindingCombo.Modifier2, map, i - 1)) continue;

                    InputAction foundAction = input.FindAction(binding.action);
                    InputBindingCombination foundCombination = new InputBindingCombination.Builder().WithLinkedBindings(map.bindings[i], (usesModifiers) ? map.bindings[i-2] : new InputBinding(""), (usesModifiers) ? map.bindings[i-1] : new InputBinding("")).Build();
                    int foundIndex = foundAction.GetBindingIndexWithEmptySupport(binding);
                    return (foundAction, foundCombination, foundIndex);
                }
            }

            return (null, new InputBindingCombination.Builder().AsEmpty(), -1);

            bool HasNoModifiersButIsModifierComposite(InputBinding binding, InputActionMap map, int indexOffset) => !usesModifiers && binding.IsTwoOptionalModifiersComposite() && indexOffset > -1 && !string.IsNullOrEmpty(map.bindings[indexOffset].effectivePath);
            bool HasModifiersButNotSame(InputBinding other, InputActionMap map, int indexOffset) => usesModifiers &&  indexOffset > -1 && (!map.bindings[indexOffset].effectivePath.Equals(other.effectivePath) || map.bindings[indexOffset].id == other.id);
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
        /// Replaces all TwoOptionalModifiersComposite bindings with their respective composite bindings.
        /// </summary>
        public void ReplaceAllBindings() => BindingReplacer.ReplaceBindings(input);

        public InputAction GetAction(InputAction action) => input.FindAction(action.name);

        public InputAction GetAction(InputBinding binding)
        {
            InputAction a = input.FindAction(binding.action);
            if (input == null) throw new NullReferenceException($"InputAction {binding.action} not found.");
            return a;
        }

        private void UpdatePointerPosition(Vector2 value) => pointerPosition = value;
        
        public Vector2 PointerPosition { get => pointerPosition; }
        public InputProfilePlayer Player { get => inputPlayer; }
        public InputProfileUI UI { get => inputUI; }
        public InputProfilePause Pause { get => inputPause; }
        public InputProfileShortcuts Shortcuts { get => inputShortcuts; }
        public string KeyboardBindingGroup { get => input.KeyboardMouseScheme.bindingGroup; }
        public string GamepadBindingGroup { get => input.GamepadScheme.bindingGroup;}
    }
}