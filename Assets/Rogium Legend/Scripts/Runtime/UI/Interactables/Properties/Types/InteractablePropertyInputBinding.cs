using UnityEngine;
using UnityEngine.InputSystem;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Represents a property that allows rebinding an single-button input for both keyboard and gamepad.
    /// </summary>
    public class InteractablePropertyInputBinding : InteractablePropertyBase<InputAction>
    {
        [SerializeField] private InputBindingReader inputReader;
        
        private InputAction action;
        
        public void Construct(InputAction action, int bindingIndex)
        {
            ConstructTitle(action.name);
            inputReader.Construct(action, bindingIndex);
            this.action = action;
        }
        
        public override void SetDisabled(bool isDisabled)
        {
            inputReader.SetActive(!isDisabled);
        }
        
        public override InputAction PropertyValue { get => inputReader.Action; }

        public string KeyboardInputString { get => inputReader.InputString; }
    }
}