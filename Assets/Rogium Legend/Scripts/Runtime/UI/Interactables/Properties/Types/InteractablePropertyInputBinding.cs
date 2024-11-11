using UnityEngine.InputSystem;

namespace Rogium.UserInterface.Interactables.Properties
{
    public class InteractablePropertyInputBinding : InteractablePropertyBase<InputAction>
    {
        private InputAction action;
        private InputActionRebindingExtensions.RebindingOperation rebindOperation;

        public void Construct(InputAction action)
        {
            this.action = action;
            ConstructTitle(action.name);
        }
        
        /// <summary>
        /// Start listening for new input.
        /// </summary>
        public void StartListening()
        {
            rebindOperation = action.PerformInteractiveRebinding();
        }
        
        public override void SetDisabled(bool isDisabled)
        {
        }

        public override InputAction PropertyValue { get => action; }
    }
}