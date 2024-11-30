using System;
using System.Linq;
using RedRats.Safety;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.ActionHistory;
using Rogium.UserInterface.ModalWindows;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using InputSystem = Rogium.Systems.Input.InputSystem;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Reads user inputs from devices and binds them to actions.
    /// </summary>
    public class InputBindingReader : MonoBehaviour, IPointerClickHandler
    {
        public static event Action<InputAction, int> OnRebindStartAny, OnRebindEndAny;
        public event Action OnRebindStart, OnRebindEnd;
        public event Action OnClear;
        
        [SerializeField] private UIInfo ui;
        
        private InputAction action;
        private int bindingIndex;
        private InputActionRebindingExtensions.RebindingOperation rebindOperation;
        private InputBinding lastBinding;

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

        public void Construct(InputAction action, int bindingIndex)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(bindingIndex, action.bindings, nameof(action.bindings));
            this.action = action;
            this.bindingIndex = bindingIndex;
            this.lastBinding = action.bindings[bindingIndex];
            ui.ShowBoundInputDisplay();
            RefreshInputString();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!ui.button.interactable) return;
            if (eventData.button != PointerEventData.InputButton.Right) return;
            
            //Clear the binding
            action.Disable();
            action.ApplyBindingOverride(bindingIndex, "");
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
            OnRebindStartAny?.Invoke(action, bindingIndex);
            OnRebindStart?.Invoke();
            rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
                                    .OnCancel(_ => StopRebinding())  
                                    .OnComplete(FinishRebinding)
                                    .WithTimeout(EditorDefaults.Instance.InputTimeout)
                                    .Start();

            void FinishRebinding(InputActionRebindingExtensions.RebindingOperation operation)
            {
                (InputAction duplicateAction, int _) = InputSystem.GetInstance().FindDuplicateBinding(action, bindingIndex);
                if (duplicateAction != null)
                {
                    ModalWindowBuilder.GetInstance().OpenWindow(new ModalWindowData.Builder()
                        .WithMessage($"The input is already used in {duplicateAction.name}. Want to rebind?")
                        .WithAcceptButton("Override", () => OverrideDuplicateBinding(operation))
                        .WithDenyButton("Cancel", RevertBinding)
                        .Build());
                }
                else ActionHistorySystem.AddAndExecute(new UpdateInputBindingAction(this, operation.action.bindings[bindingIndex].effectivePath, lastBinding.effectivePath, Rebind));
                StopRebinding();
            }
            
            void StopRebinding()
            {
                action.Enable();
                rebindOperation.Dispose();
                rebindOperation = null;
                RefreshInputString();
                OnRebindEndAny?.Invoke(action, bindingIndex);
                OnRebindEnd?.Invoke();
                ui.ShowBoundInputDisplay();
            }
            
            void OverrideDuplicateBinding(InputActionRebindingExtensions.RebindingOperation operation)
            {
                ActionHistorySystem.ForceGroupAllActions();
                ActionHistorySystem.AddAndExecute(new UpdateInputBindingAction(this, operation.action.bindings[bindingIndex].effectivePath, lastBinding.effectivePath, Rebind));
                
                //Clear the duplicate binding
                InputBindingReader duplicateReader = FindObjectsByType<InputBindingReader>(FindObjectsSortMode.None).FirstOrDefault(r => r.InputString == InputString);
                if (duplicateReader != null) ActionHistorySystem.AddAndExecute(new UpdateInputBindingAction(duplicateReader, "", duplicateReader.Binding.effectivePath, duplicateReader.Rebind));
                ActionHistorySystem.ForceGroupAllActionsEnd();
            }
            
            void RevertBinding() => Rebind(lastBinding.effectivePath);
        }

        public void Rebind(string path)
        {
            lastBinding = action.bindings[bindingIndex];
            action.Disable();
            action.ApplyBindingOverride(bindingIndex, path);
            action.Enable();
            RefreshInputString();
            OnRebindEndAny?.Invoke(action, bindingIndex);
            OnRebindEnd?.Invoke();
        }
        
        public void SetActive(bool value) => ui.button.interactable = value;
        private void Activate(InputAction action, int bindingIndex)
        {
            if (action == this.action && bindingIndex == this.bindingIndex) return;
            SetActive(true);
        }
        private void Deactivate(InputAction action, int bindingIndex)
        {
            if (action == this.action && bindingIndex == this.bindingIndex) return;
            SetActive(false);
        }
        
        private void RefreshInputString()
        {
            string inputText = action.bindings[bindingIndex].ToDisplayString();
            ui.inputText.text = (string.IsNullOrEmpty(inputText)) ? EditorDefaults.Instance.InputEmptyText : inputText;
        }

        public InputAction Action { get => action; }
        public InputBinding Binding { get => action.bindings[bindingIndex]; }
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