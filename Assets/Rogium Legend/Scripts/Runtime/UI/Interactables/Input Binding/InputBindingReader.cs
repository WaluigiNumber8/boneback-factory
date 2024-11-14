using System;
using RedRats.Safety;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core.Defaults;
using Rogium.UserInterface.ModalWindows;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using InputSystem = Rogium.Systems.Input.InputSystem;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Reads user inputs from devices and binds them to actions.
    /// </summary>
    public class InputBindingReader : MonoBehaviour
    {
        [SerializeField] private UIInfo ui;
        
        private InputAction action;
        private int bindingIndex;
        private InputActionRebindingExtensions.RebindingOperation rebindOperation;

        private void Awake() => ui.button.onClick.AddListener(StartRebinding);

        public void Construct(InputAction action, int bindingIndex)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(bindingIndex, action.bindings, nameof(action.bindings));
            this.action = action;
            this.bindingIndex = bindingIndex;
            ui.ShowBoundInputDisplay();
            RefreshInputString();
        }
        
        /// <summary>
        /// Start listening for new input.
        /// </summary>
        public void StartRebinding()
        {
            action.Disable();
            ui.ShowBindingDisplay();
            rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
                                    .OnCancel(_ => StopRebinding())  
                                    .OnComplete(FinishRebinding)
                                    .WithTimeout(EditorDefaults.Instance.InputTimeout)
                                    .Start();

            void FinishRebinding(InputActionRebindingExtensions.RebindingOperation operation)
            {
                (InputAction duplicateAction, int duplicateIndex) = InputSystem.GetInstance().FindDuplicateBinding(action, bindingIndex);
                if (duplicateAction != null)
                {
                    ModalWindowBuilder.GetInstance().OpenWindow(new ModalWindowData.Builder()
                        .WithMessage($"The input is already used in {duplicateAction.name}. Want to rebind?")
                        .WithAcceptButton("Yes", () =>
                        {
                            duplicateAction.Disable();
                            duplicateAction.ApplyBindingOverride(duplicateIndex, "");
                            duplicateAction.Enable();
                            operation.Complete();
                        })
                        .WithDenyButton("No", operation.Cancel)
                        .Build());
                }
                StopRebinding();
            }
            
            void StopRebinding()
            {
                action.Enable();
                rebindOperation.Dispose();
                rebindOperation = null;
                RefreshInputString();
                ui.ShowBoundInputDisplay();
            }
        }
        
        public void SetActive(bool value) => ui.button.interactable = value;

        private void RefreshInputString() => ui.inputText.text = InputString;

        public InputBinding Binding { get => action.bindings[bindingIndex]; }
        public string InputString { get => action.bindings[bindingIndex].ToDisplayString(); }
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