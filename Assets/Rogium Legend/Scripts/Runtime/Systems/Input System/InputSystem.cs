using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RedRats.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

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

        public (InputAction, InputBindingCombination, int) FindDuplicateBinding(InputAction action, InputBindingCombination bindingCombo)
        {
            bool usesModifiers = bindingCombo.Modifier1.effectivePath != "" || bindingCombo.Modifier2.effectivePath != "";
            for (int i = 0; i < action.actionMap.bindings.Count; i++)
            {
                InputBinding binding = action.actionMap.bindings[i];
                if (!binding.effectivePath.Equals(bindingCombo.Button.effectivePath) || binding.id == bindingCombo.Button.id) continue;
                if (HasNoModifiersButIsModifierComposite(binding, i - 2)) continue;
                if (HasNoModifiersButIsModifierComposite(binding, i - 1)) continue;
                if (HasModifiersButNotSame(bindingCombo.Modifier1, i - 2)) continue;
                if (HasModifiersButNotSame(bindingCombo.Modifier2, i - 1)) continue;

                InputAction foundAction = input.FindAction(binding.action);
                InputBindingCombination foundCombination = new InputBindingCombination.Builder().WithLinkedBindings(foundAction.actionMap.bindings[i], (usesModifiers) ? foundAction.actionMap.bindings[i-2] : new InputBinding(""), (usesModifiers) ? foundAction.actionMap.bindings[i-1] : new InputBinding("")).Build();
                int foundIndex = foundAction.GetBindingIndexWithEmptySupport(binding);
                foundIndex = (foundIndex == -1) ? 1 : foundIndex; //MUST BE HERE. Rebinding the cancel binding's alt and then using same button for another binding NEVER removes the one from cancel.
                return (foundAction, foundCombination, foundIndex);
            }

            return (null, new InputBindingCombination.Builder().AsEmpty(), -1);

            bool HasNoModifiersButIsModifierComposite(InputBinding binding, int indexOffset) => !usesModifiers && binding.IsTwoOptionalModifiersComposite() && !string.IsNullOrEmpty(action.actionMap.bindings[indexOffset].effectivePath);
            bool HasModifiersButNotSame(InputBinding other, int indexOffset) => usesModifiers && (!action.actionMap.bindings[indexOffset].effectivePath.Equals(other.effectivePath) || action.actionMap.bindings[indexOffset].id == other.id);
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

        public void RemoveAllEmptyBindings()
        {
            Stopwatch s = Stopwatch.StartNew();
            foreach (InputActionMap map in input.asset.actionMaps)
            {
                foreach (InputAction action in map.actions)
                {
                    bool isInsideComposite = false;
                    int bindingsInComposite = 0;
                    IList<int> emptyIndexes = new List<int>();

                    for (int i = action.bindings.Count - 1; i >= 0; i--)
                    {
                        InputBinding binding = action.bindings[i];

                        //If is composite header
                        if (binding.isComposite)
                        {
                            if (isInsideComposite)
                            {
                                isInsideComposite = false;

                                //Reach last part of composite
                                if (bindingsInComposite == emptyIndexes.Count)
                                {
                                    //All parts of composite are empty
                                    action.ChangeBinding(i).Erase();
                                    continue;
                                }
                                else
                                {
                                    //Not all parts of composite are empty
                                    foreach (int idx in emptyIndexes)
                                    {
                                        action.ChangeBinding(idx).Erase();
                                    }
                                    continue;
                                }
                            }
                            else
                            {
                                //First composite found, can be ignored
                                continue;
                            }
                        }

                        //If is composite part
                        if (binding.isPartOfComposite)
                        {
                            if (isInsideComposite)
                            {
                                //Every other part of composite
                                bindingsInComposite++;
                                if (binding.effectivePath == "") emptyIndexes.Add(i);
                                continue;
                            }
                            else
                            {
                                //First part of composite
                                isInsideComposite = true;
                                bindingsInComposite = 0;
                                emptyIndexes.Clear();

                                bindingsInComposite++;
                                if (binding.effectivePath == "") emptyIndexes.Add(i);
                                continue;
                            }
                        }


                        //If is normal binding
                        EraseBinding();
                        continue;

                        void EraseBinding()
                        {
                            if (binding.effectivePath != "") return;
                            action.ChangeBinding(i).Erase();
                        }
                    }
                }
            }

            s.Stop();
            Debug.Log($"Remove empty: {s.ElapsedMilliseconds}ms.");
        }
        
        public InputAction GetAction(InputAction action) => input.FindAction(action.name);
        public InputAction GetAction(InputBinding binding) => input.FindAction(binding.action);
        
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