using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Properties
{
    public class InteractablePropertyInputBinding : InteractablePropertyBase<InputAction>
    {
        [SerializeField] private UIInfo ui;
        
        private InputAction action;
        private InputActionRebindingExtensions.RebindingOperation rebindOperation;

        public void Construct(InputAction action)
        {
            this.action = action;
            ConstructTitle(action.name);
            ui.ShowBoundInputDisplay();
            Refresh();
        }
        
        /// <summary>
        /// Start listening for new input.
        /// </summary>
        public void StartListening()
        {
            action.Disable();
            ui.ShowBindingDisplay();
            rebindOperation = action.PerformInteractiveRebinding()
                                    .OnComplete(operation =>
                                    {
                                        ui.ShowBoundInputDisplay();
                                        action.Enable();
                                        rebindOperation.Dispose();
                                    })
                                    .Start();   
        }
        
        public override void SetDisabled(bool isDisabled) => ui.button.interactable = !isDisabled;

        private void Refresh() => ui.inputText.text = InputString;

        public override InputAction PropertyValue { get => action; }
        public string InputString { get => action.bindings[0].ToDisplayString(); }
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