using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = Rogium.Systems.Input.InputSystem;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Represents a property that allows rebinding an single-button input for both keyboard and gamepad.
    /// </summary>
    public class InteractablePropertyInputBinding : InteractablePropertyBase<InputAction>
    {
        [SerializeField] private InputBindingReader keyboardBinding;
        
        private InputAction action;
        
        public void Construct(InputAction action)
        {
            ConstructTitle(action.name);
            keyboardBinding.Construct(action, action.GetBindingIndex(InputSystem.GetInstance().KeyboardSchemeGroup));
            this.action = action;
        }
        
        public override void SetDisabled(bool isDisabled)
        {
            keyboardBinding.SetActive(!isDisabled);
        }
        
        public override InputAction PropertyValue { get => action; }

        public string KeyboardInputString { get => keyboardBinding.InputString; }
    }
}