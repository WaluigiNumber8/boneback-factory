using System;
using Rogium.Systems.Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rogium.Core.Shortcuts
{
    /// <summary>
    /// Triggers a selectable via a button shortcut.
    /// </summary>
    public class ShortcutTrigger : MonoBehaviour
    {
        [SerializeField] private ShortcutType shortcut;

        private IPointerClickHandler selectable;
        private InputButton shortcutEvent;
        
        private void Awake()
        {
            selectable = GetComponent<IPointerClickHandler>();
            InputSystem input = InputSystem.GetInstance();
            shortcutEvent = shortcut switch
            {
                ShortcutType.Undo => input.ShortcutsGeneral.Undo,
                ShortcutType.SelectionTool => input.ShortcutsGeneral.SelectionTool,
                _ => throw new ArgumentOutOfRangeException($"Shortcut {shortcut} not implemented.")
            };
        }

        private void OnEnable() => shortcutEvent.OnPress += Click;
        private void OnDisable() => shortcutEvent.OnPress -= Click;
        
        private void Click() => selectable.OnPointerClick((new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left }));
    }
}