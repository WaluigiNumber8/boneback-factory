using System;
using System.Linq;
using RedRats.Core;
using RedRats.Safety;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.Input;
using Rogium.UserInterface.ModalWindows;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using InputSystem = Rogium.Systems.Input.InputSystem;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Reads user inputs from devices and binds them to actions.
    /// </summary>
    public class InputBindingReader : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<InputAction, InputBindingCombination, int> OnRebindStartAny, OnRebindEndAny;
        public event Action OnRebindStart, OnRebindEnd;
        public event Action OnClear;
        
        [SerializeField] private UIInfo ui;
        
        private InputAction action;
        private InputActionRebindingExtensions.RebindingOperation rebindOperation;
        private InputBindingCombination binding;
        private InputBindingCombination lastBinding;
        private int modifier1Index;
        private int modifier2Index;
        private int buttonIndex;

        private void Awake() => ui.button.onClick.AddListener(StartRebinding);

        private void OnEnable()
        {
            OnRebindStartAny += Deactivate;
            OnRebindEndAny += Activate;
        }

        private void OnDisable()
        {
            rebindOperation?.Dispose();
            OnRebindStartAny -= Deactivate;
            OnRebindEndAny -= Activate;
        }

        public void Construct(InputAction action, int bindingIndex, int modifier1Index = -1, int modifier2Index = -1)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(bindingIndex, action.bindings, nameof(action.bindings));
            if (modifier1Index != -1) SafetyNet.EnsureIndexWithingCollectionRange(modifier1Index, action.bindings, nameof(action.bindings));
            if (modifier2Index != -1) SafetyNet.EnsureIndexWithingCollectionRange(modifier2Index, action.bindings, nameof(action.bindings));
            
            this.action = action;
            this.modifier1Index = modifier1Index;
            this.modifier2Index = modifier2Index;
            this.buttonIndex = bindingIndex;
            this.binding = new InputBindingCombination.Builder().WithLinkedBindings(action, buttonIndex, modifier1Index, modifier2Index).Build();
            this.lastBinding = binding;
            ui.ShowBoundInputDisplay();
            RefreshInputString();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!ui.button.interactable) return;
            if (eventData.button != PointerEventData.InputButton.Right) return;
            
            //Clear the binding
            Rebind(new InputBindingCombination.Builder().From(binding).AsEmpty());
            OnClear?.Invoke();
        }
        
        /// <summary>
        /// Start listening for new input.
        /// </summary>
        public void StartRebinding()
        {
            action.Disable();
            ui.ShowBindingDisplay();
            OnRebindStartAny?.Invoke(action, binding, buttonIndex);
            OnRebindStart?.Invoke();
            rebindOperation = action.PerformInteractiveRebinding(buttonIndex)
                                    .OnCancel(_ => StopRebinding())  
                                    .OnComplete(FinishRebinding)
                                    .OnMatchWaitForAnother((modifier1Index != -1 || modifier2Index != -1) ? EditorDefaults.Instance.InputWaitForAnother : 0)
                                    .WithTimeout(EditorDefaults.Instance.InputTimeout)
                                    .Start();

            void FinishRebinding(InputActionRebindingExtensions.RebindingOperation operation)
            {
                InputBindingCombination c = GetBindingCombinationFrom(operation);
                Rebind(c);

                (InputAction duplicateAction, InputBindingCombination duplicateCombination, int duplicateIndex) = InputSystem.GetInstance().FindDuplicateBinding(action, binding);
                if (duplicateAction != null)
                {
                    ModalWindowBuilder.GetInstance().OpenWindow(new ModalWindowData.Builder()
                        .WithMessage($"The input is already used in <style=\"Important\">{duplicateAction.name.WithSpacesBeforeCapitals()}</style>.")
                        .WithAcceptButton("Override", () => OverrideDuplicateBinding(duplicateAction, duplicateCombination, duplicateIndex))
                        .WithDenyButton("Revert", RevertBinding)
                        .Build());
                }
                else ActionHistorySystem.AddAndExecute(new UpdateInputBindingAction(this, binding, lastBinding, Rebind));
                StopRebinding();
            }
            
            void StopRebinding()
            {
                action.Enable();
                rebindOperation.Dispose();
                rebindOperation = null;
                RefreshInputString();
                OnRebindEndAny?.Invoke(action, binding, buttonIndex);
                OnRebindEnd?.Invoke();
                ui.ShowBoundInputDisplay();
            }
            
            void OverrideDuplicateBinding(InputAction duplicateAction, InputBindingCombination combo, int duplicateIndex)
            {
                ActionHistorySystem.ForceGroupAllActions();
                
                //Clear the duplicate binding reader
                InputBindingReader duplicateReader = null;
                foreach (InputBindingReader r in FindObjectsByType<InputBindingReader>(FindObjectsSortMode.None))
                {
                    if (r.action.id != duplicateAction.id) continue;
                    if (!r.Binding.HasSameInputs(combo)) continue;
                    if (r.buttonIndex != duplicateIndex) continue;
                    duplicateReader = r;
                    break;
                }

                if (duplicateReader != null) ActionHistorySystem.AddAndExecute(new UpdateInputBindingAction(duplicateReader, new InputBindingCombination.Builder().From(duplicateReader.Binding).AsEmpty(), duplicateReader.Binding, c => RebindAction(duplicateAction, c, duplicateReader.modifier1Index, duplicateReader.modifier2Index, duplicateReader.buttonIndex)));
                ActionHistorySystem.AddAndExecute(new UpdateInputBindingAction(this, binding, lastBinding, c => RebindAction(action, c, modifier1Index, modifier2Index, buttonIndex)));
                ActionHistorySystem.ForceGroupAllActionsEnd();
            }
            
            void RevertBinding() => Rebind(lastBinding);
        }

        public void Rebind(InputBindingCombination combination)
        {
            lastBinding = binding;
            binding = combination;
            action.Disable();
            if (modifier1Index != -1) action.ApplyBindingOverride(modifier1Index, binding.Modifier1.effectivePath);
            if (modifier2Index != -1) action.ApplyBindingOverride(modifier2Index, binding.Modifier2.effectivePath);
            action.ApplyBindingOverride(buttonIndex, binding.Button.effectivePath);
            action.Enable();
            RefreshInputString();
            OnRebindEndAny?.Invoke(action, binding, buttonIndex);
            OnRebindEnd?.Invoke();
        }
        
        public void SetActive(bool value) => ui.button.interactable = value;
        private void Activate(InputAction action, InputBindingCombination binding, int index)
        {
            if (action == this.action && binding == this.binding && this.buttonIndex == index) return;
            SetActive(true);
        }
        private void Deactivate(InputAction action, InputBindingCombination binding, int index)
        {
            if (action == this.action && binding == this.binding && this.buttonIndex == index) return;
            SetActive(false);
        }
        
        private void RefreshInputString()
        {
            string inputText = binding.DisplayString;
            ui.inputText.text = (string.IsNullOrEmpty(inputText)) ? EditorDefaults.Instance.InputEmptyText : inputText;
        }

        private InputBindingCombination GetBindingCombinationFrom(InputActionRebindingExtensions.RebindingOperation operation)
        {
            InputBindingCombination newBinding;
            
            //If modifiers are not allowed, return the first binding
            if (modifier1Index == -1 || modifier2Index == -1)
            {
                newBinding = new InputBindingCombination.Builder().From(binding).WithButton(operation.candidates[0].path.FormatForBindingPath()).Build();
                return newBinding;
            }
            
            newBinding = operation.candidates.Where(c => c is KeyControl).ToList().Count switch
            {
                1 => new InputBindingCombination.Builder().From(binding).ClearPaths().WithButton(operation.candidates[0].path.FormatForBindingPath()).Build(),
                2 => new InputBindingCombination.Builder().From(binding).ClearPaths().WithModifier1(operation.candidates[0].path.FormatForBindingPath()).WithButton(operation.candidates[1].path.FormatForBindingPath()).Build(),
                _ => new InputBindingCombination.Builder().From(binding).ClearPaths().WithModifier1(operation.candidates[0].path.FormatForBindingPath()).WithModifier2(operation.candidates[1].path.FormatForBindingPath()).WithButton(operation.candidates[2].path.FormatForBindingPath()).Build()
            };
            return newBinding;
        }
        
        private static void RebindAction(InputAction action, InputBindingCombination combo, int modifier1Index, int modifier2Index, int buttonIndex)
        {
            action.Disable();
            if (modifier1Index != -1) action.ApplyBindingOverride(modifier1Index, combo.Modifier1.effectivePath);
            if (modifier2Index != -1) action.ApplyBindingOverride(modifier2Index, combo.Modifier2.effectivePath);
            action.ApplyBindingOverride(buttonIndex, combo.Button.effectivePath);
            action.Enable();
        }

        public InputAction Action { get => action; }
        public InputBindingCombination Binding { get => binding; }
        public string InputString { get => ui.inputText.text ; }
        public GameObject BindingDisplay { get => ui.bindingDisplay; }
        public GameObject BoundInputDisplay { get => ui.boundInputDisplay; }

        [Serializable]
        public struct UIInfo
        {
            public Button button;
            public TextMeshProUGUI inputText;
            public GameObject boundInputDisplay;
            public GameObject bindingDisplay;
            
            public void ShowBoundInputDisplay()
            {
                bindingDisplay.SetActive(false);
                boundInputDisplay.SetActive(true);
            }
            
            public void ShowBindingDisplay()
            {
                boundInputDisplay.SetActive(false);
                bindingDisplay.SetActive(true);
            }
        }
    }
}