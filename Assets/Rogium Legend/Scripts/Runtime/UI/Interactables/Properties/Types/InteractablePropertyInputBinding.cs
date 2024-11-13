using UnityEngine;
using UnityEngine.InputSystem;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Represents a property that allows rebinding an single-button input for both keyboard and gamepad.
    /// </summary>
    public class InteractablePropertyInputBinding : InteractablePropertyBase<InputAction>
    {
        [SerializeField] private InputBindingReader keyboardBinding;
        [SerializeField] private InputBindingReader gamepadBinding;
        
        private InputAction action;
        
        public void Construct(InputAction action)
        {
            ConstructTitle(action.name);
            keyboardBinding.Construct(action);
            gamepadBinding.Construct(action);
            this.action = action;
        }
        
        public override void SetDisabled(bool isDisabled)
        {
            keyboardBinding.SetActive(!isDisabled);
            gamepadBinding.SetActive(!isDisabled);
        }
        
        public void StartListeningToKeyboard() => keyboardBinding.StartListening();
        public void StartListeningToGamepad() => gamepadBinding.StartListening();

        public override InputAction PropertyValue { get => action; }

        public string KeyboardInputString { get => keyboardBinding.InputString; }
        public string GamepadInputString { get => gamepadBinding.InputString; }
    }
}