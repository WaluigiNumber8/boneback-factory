using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
            Refresh();
        }
        
        /// <summary>
        /// Start listening for new input.
        /// </summary>
        public void StartListening()
        {
            action.Disable();
            rebindOperation = action.PerformInteractiveRebinding()
                                    .OnComplete(operation =>
                                    {
                                        action.Enable();
                                        rebindOperation.Dispose();
                                    })
                                    .Start();   
        }
        
        public override void SetDisabled(bool isDisabled)
        {
        }

        private void Refresh() => ui.inputText.text = InputString;

        public override InputAction PropertyValue { get => action; }
        public string InputString { get => action.bindings[0].ToDisplayString(); }

        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI inputText;
        }
    }
}