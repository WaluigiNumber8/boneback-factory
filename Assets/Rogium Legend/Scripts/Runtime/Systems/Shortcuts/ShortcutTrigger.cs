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
                ShortcutType.Undo => input.Shortcuts.Undo,
                ShortcutType.Redo => input.Shortcuts.Redo,
                ShortcutType.Save => input.Shortcuts.Save,
                ShortcutType.Cancel => input.Shortcuts.Cancel,
                ShortcutType.SelectionTool => input.Shortcuts.SelectionTool,
                ShortcutType.BrushTool => input.Shortcuts.BrushTool,
                ShortcutType.EraserTool => input.Shortcuts.EraserTool,
                ShortcutType.FillTool => input.Shortcuts.FillTool,
                ShortcutType.PickerTool => input.Shortcuts.PickerTool,
                ShortcutType.ClearCanvas => input.Shortcuts.ClearCanvas,
                ShortcutType.ToggleGrid => input.Shortcuts.ToggleGrid,
                ShortcutType.LayerTiles => input.Shortcuts.TilesLayer,
                ShortcutType.LayerDecors => input.Shortcuts.DecorLayer,
                ShortcutType.LayerObjects => input.Shortcuts.ObjectsLayer,
                ShortcutType.LayerEnemies => input.Shortcuts.EnemiesLayer,
                _ => throw new ArgumentOutOfRangeException($"Shortcut {shortcut} not implemented.")
            };
        }

        private void OnEnable() => shortcutEvent.OnPress += Click;
        private void OnDisable() => shortcutEvent.OnPress -= Click;
        
        private void Click() => selectable.OnPointerClick((new PointerEventData(EventSystem.current) { button = PointerEventData.InputButton.Left }));
    }
}