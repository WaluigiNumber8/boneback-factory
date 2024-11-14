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
        [SerializeField] private InputBindingReader gamepadBinding;
        
        private InputAction action;
        
        public void Construct(InputAction action)
        {
            ConstructTitle(action.name);
            keyboardBinding.Construct(action, action.GetBindingIndex(InputSystem.GetInstance().KeyboardSchemeGroup));
            gamepadBinding.Construct(action, action.GetBindingIndex(InputSystem.GetInstance().GamepadSchemeGroup));
            this.action = action;
        }
        
        public override void SetDisabled(bool isDisabled)
        {
            keyboardBinding.SetActive(!isDisabled);
            gamepadBinding.SetActive(!isDisabled);
        }
        
        public void StartListeningToKeyboard() => keyboardBinding.StartRebinding();
        public void StartListeningToGamepad() => gamepadBinding.StartRebinding();

        public override InputAction PropertyValue { get => action; }

        public string KeyboardInputString { get => keyboardBinding.InputString; }
        public string GamepadInputString { get => gamepadBinding.InputString; }
    }
}