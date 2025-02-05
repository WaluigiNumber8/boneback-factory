using System;
using RedRats.Safety;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using InputSystem = Rogium.Systems.Input.InputSystem;

namespace Rogium.Core.Shortcuts
{
    [Serializable]
    public class ShortcutData
    {
        private string GroupTitle() => (trigger == null) ? "None" : $"{trigger.action.actionMap.name}/{trigger.action.name}";

        [Required, FoldoutGroup("$GroupTitle")]
        public InputActionReference trigger;

        [Required, FoldoutGroup("$GroupTitle")]
        public UnityEvent action;

        private InputAction inputAction;

        public ShortcutData(InputAction trigger, UnityEvent action)
        {
            this.trigger = InputActionReference.Create(trigger);
            this.action = action;
            RefreshInput();
        }

        public void RefreshInput()
        {
            SafetyNet.EnsureIsNotNull(trigger, "Trigger Action");
            inputAction = InputSystem.GetInstance().GetAction(trigger);
        }

        public void Link()
        {
            if (inputAction == null) return;
            inputAction.performed += Press;
        }

        public void Unlink()
        {
            if (inputAction == null) return;
            inputAction.performed -= Press;
        }

        private void Press(InputAction.CallbackContext ctx) => action.Invoke();

        public override string ToString() => $"{inputAction.name} -> {action.GetPersistentMethodName(0)}()";
    }
}