using System;
using System.Linq;
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
        public static event Action<InputAction, InputBindingCombination> OnRebindStartAny, OnRebindEndAny;
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
            OnRebindStartAny -= Deactivate;
            OnRebindEndAny -= Activate;
        }

        public void Construct(InputAction action, int bindingIndex, int modifier1Index = -1, int modifier2Index = -1)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(buttonIndex, action.bindings, nameof(action.bindings));
            this.action = action;
            this.modifier1Index = modifier1Index;
            this.modifier2Index = modifier2Index;
            this.buttonIndex = bindingIndex;
            this.binding = new InputBindingCombination((this.modifier1Index != -1) ? action.bindings[this.modifier1Index] : null,
                                                       (this.modifier2Index != -1) ? action.bindings[this.modifier2Index] : null,
                                                        action.bindings[buttonIndex]);
            this.lastBinding = binding;
            ui.ShowBoundInputDisplay();
            RefreshInputString();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!ui.button.interactable) return;
            if (eventData.button != PointerEventData.InputButton.Right) return;
            
            //Clear the binding
            action.Disable();
            action.ApplyBindingOverride(buttonIndex, "");
            if (modifier1Index != -1) action.ApplyBindingOverride(modifier1Index, "");
            if (modifier2Index != -1) action.ApplyBindingOverride(modifier2Index, "");
            action.Enable();
            RefreshInputString();
            OnClear?.Invoke();
        }
        
        /// <summary>
        /// Start listening for new input.
        /// </summary>
        public void StartRebinding()
        {
            action.Disable();
            ui.ShowBindingDisplay();
            OnRebindStartAny?.Invoke(action, binding);
            OnRebindStart?.Invoke();
            rebindOperation = action.PerformInteractiveRebinding(buttonIndex)
                                    .OnCancel(_ => StopRebinding())  
                                    .OnComplete(FinishRebinding)
                                    .OnMatchWaitForAnother(EditorDefaults.Instance.InputWaitForAnother)
                                    .WithTimeout(EditorDefaults.Instance.InputTimeout)
                                    .Start();

            void FinishRebinding(InputActionRebindingExtensions.RebindingOperation operation)
            {
                binding = GetBindingCombinationFrom(operation);
                Rebind(binding);

                (InputAction duplicateAction, InputBindingCombination duplicateCombination) = InputSystem.GetInstance().FindDuplicateBinding(action, binding);
                if (duplicateAction != null)
                {
                    ModalWindowBuilder.GetInstance().OpenWindow(new ModalWindowData.Builder()
                        .WithMessage($"The input is already used in {duplicateAction.name}. Want to rebind?")
                        .WithAcceptButton("Override", () => OverrideDuplicateBinding(operation, duplicateAction, duplicateCombination))
                        .WithDenyButton("Cancel", RevertBinding)
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
                OnRebindEndAny?.Invoke(action, binding);
                OnRebindEnd?.Invoke();
                ui.ShowBoundInputDisplay();
            }
            
            void OverrideDuplicateBinding(InputActionRebindingExtensions.RebindingOperation operation, InputAction duplicateAction, InputBindingCombination combo)
            {
                ActionHistorySystem.ForceGroupAllActions();
                
                //Clear the duplicate binding reader
                InputBindingReader duplicateReader = FindObjectsByType<InputBindingReader>(FindObjectsSortMode.None).FirstOrDefault(r => r.action == duplicateAction && r.Binding == combo);
                ActionHistorySystem.AddAndExecute(new UpdateInputBindingAction(duplicateReader, new InputBindingCombination(), Binding, c => RebindAction(duplicateAction, c)));
                
                ActionHistorySystem.AddAndExecute(new UpdateInputBindingAction(this, GetBindingCombinationFrom(operation), lastBinding, c => RebindAction(action, c)));
                ActionHistorySystem.ForceGroupAllActionsEnd();
            }
            
            void RevertBinding() => Rebind(lastBinding);
        }

        public void Rebind(InputBindingCombination combination)
        {
            lastBinding = binding;
            action.Disable();
            if (modifier1Index != -1) action.ApplyBindingOverride(modifier1Index, combination.Modifier1.Path);
            if (modifier2Index != -1) action.ApplyBindingOverride(modifier2Index, combination.Modifier2.Path);
            action.ApplyBindingOverride(buttonIndex, combination.Button.effectivePath);
            action.Enable();
            RefreshInputString();
            OnRebindEndAny?.Invoke(action, binding);
            OnRebindEnd?.Invoke();
        }
        
        public void SetActive(bool value) => ui.button.interactable = value;
        private void Activate(InputAction action, InputBindingCombination binding)
        {
            if (action == this.action && binding == this.binding) return;
            SetActive(true);
        }
        private void Deactivate(InputAction action, InputBindingCombination binding)
        {
            if (action == this.action && binding == this.binding) return;
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
                newBinding = new InputBindingCombination(null, null, new InputBinding(operation.candidates[0].path.FormatForBindingPath()));
                newBinding.SetButtonID(binding.Button.id);
                return newBinding;
            }
            
            newBinding = operation.candidates.Where(c => c is KeyControl).ToList().Count switch
            {
                1 => new InputBindingCombination(null, null, new InputBinding(operation.candidates[0].path.FormatForBindingPath())),
                2 => new InputBindingCombination(new InputBinding(operation.candidates[0].path.FormatForBindingPath()), null, new InputBinding(operation.candidates[1].path.FormatForBindingPath())),
                _ => new InputBindingCombination(new InputBinding(operation.candidates[0].path.FormatForBindingPath()), new InputBinding(operation.candidates[1].path.FormatForBindingPath()), new InputBinding(operation.candidates[2].path.FormatForBindingPath()))
            };
            newBinding.Modifier1.SetID(binding.Modifier1.ID);
            newBinding.Modifier2.SetID(binding.Modifier2.ID);
            newBinding.SetButtonID(binding.Button.id);
            return newBinding;
        }
        
        private static void RebindAction(InputAction action, InputBindingCombination combo)
        {
            action.Disable();
            action.ApplyBindingOverride(combo.Modifier1.Binding);
            action.ApplyBindingOverride(combo.Modifier2.Binding);
            action.ApplyBindingOverride(combo.Button);
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