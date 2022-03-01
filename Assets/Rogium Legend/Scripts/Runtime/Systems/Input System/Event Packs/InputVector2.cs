using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rogium.Systems.Input
{
    /// <summary>
    /// Contains events for a vector2 type input.
    /// </summary>
    public class InputVector2 : IInputType
    {
        public event Action<Vector2> OnPressed;
        public event Action OnReleased;

        private readonly InputAction action;

        public InputVector2(InputAction action) => this.action = action;

        public void Enable()
        {
            action.performed += CallPress;
            action.canceled += CallRelease;
        }

        public void Disable()
        {
            action.performed -= CallPress;
            action.canceled -= CallRelease;
        }
        
        private void CallPress(InputAction.CallbackContext ctx) => OnPressed?.Invoke(ctx.ReadValue<Vector2>());
        private void CallRelease(InputAction.CallbackContext ctx) => OnReleased?.Invoke();
        
    }
}