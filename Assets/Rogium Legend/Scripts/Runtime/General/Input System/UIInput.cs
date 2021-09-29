using System;
using UnityEngine;

namespace Rogium.Global.Input
{
    /// <summary>
    /// Provides access to UI Input Actions.
    /// </summary>
    public class UIInput : MonoBehaviour
    {
        private RogiumInputActions inputActions;

        private Vector2 pointerPosition;

        private void Awake()
        {
            inputActions = new RogiumInputActions();
            inputActions.UI.Enable();

            inputActions.UI.Point.performed += (ctx) => pointerPosition = ctx.ReadValue<Vector2>();
        }

        private void OnEnable()
        {
            inputActions.UI.Enable();
        }

        private void OnDisable()
        {
            inputActions.UI.Disable();
        }

        public Vector2 PointerPosition { get => pointerPosition;}
    }
}
