using System;
using UnityEngine;

namespace Rogium.Global.Input
{
    public class UIInput : MonoBehaviour
    {
        private RogiumInputActions inputProfile;
        public DisableData disableControls;
        
        [HideInInspector] public bool canBeReset;

        private Vector2 pointerPosition;

        private bool clickHeld;
        private bool clickPressed;
        private bool clickReleased;

        private void Awake()
        {
            inputProfile = new RogiumInputActions();

        }

        //Enable ActionMap.
        private void OnEnable()
        {
            inputProfile.UI.Point.performed += ctx => pointerPosition = ctx.ReadValue<Vector2>();

            inputProfile.UI.Click.performed += ctx => PressClick();
            inputProfile.UI.Click.canceled += ctx => ReleaseClick();
            inputProfile.UI.Enable();
        }
        
        //Reset Input
        private void Update()
        {
            if (canBeReset) Invoke(nameof(ResetInput), 0.01f);
        }

        //Allow Reset
        private void FixedUpdate()
        {
            canBeReset = true;
        }

        //Reset Input Function
        public void ResetInput()
        {
            clickPressed = false;
            clickReleased = false;

            canBeReset = false;
        }

        private void PressClick()
        {
            if (disableControls.click) return;
            clickPressed = true;
            clickHeld = true;
        }

        private void ReleaseClick()
        {
            clickReleased = true;
            clickHeld = false;
        }

        //Disable ActionMap.
        private void OnDisable()
        {
            inputProfile.UI.Disable();
        }

        public Vector2 PointerPosition { get => pointerPosition; }
        public bool ClickPressed { get => clickPressed; }
        public bool ClickReleased { get => clickReleased; }
        public bool ClickHold { get => clickHeld; }
    }

    public struct DisableData
    {
        public bool click;

        public void All()
        {
            click = true;
        }
        public void Clear()
        {
            click = false;
        }
    }
}