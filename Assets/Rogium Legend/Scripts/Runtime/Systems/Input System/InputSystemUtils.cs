using System;
using Rogium.Core.Shortcuts;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Rogium.Systems.Input
{
    /// <summary>
    /// Utility functions for the input system.
    /// </summary>
    public static class InputSystemUtils
    {
        /// <summary>
        /// Gets the index of the binding for the given action and device.
        /// </summary>
        /// <param name="action">The action to get the binding of.</param>
        /// <param name="device">The input device to get the binding for.</param>
        /// <param name="getSecondary">Get the second binding found.</param>
        /// <returns>The index of the binding.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when device is unknown.</exception>
        public static int GetBindingIndexByDevice(InputAction action, InputDeviceType device, bool getSecondary = false)
        {
            return device switch
            {
                InputDeviceType.Keyboard => GetBindingIndex(new InputBinding(groups: new RogiumInputActions().KeyboardMouseScheme.bindingGroup, path: default)),
                InputDeviceType.Gamepad => GetBindingIndex(new InputBinding(groups: new RogiumInputActions().GamepadScheme.bindingGroup, path: default)),
                _ => throw new ArgumentOutOfRangeException(nameof(device), device, null)
            };

            int GetBindingIndex(InputBinding group)
            {
                ReadOnlyArray<InputBinding> bindings = action.bindings;
                bool waitForComposite = false;
                for (int i = 0; i < bindings.Count; ++i)
                {
                    InputBinding b = bindings[i];
                    if (b.isComposite) waitForComposite = false;
                    if (!group.Matches(b)) continue;
                    if (waitForComposite) continue;
                    if (getSecondary)
                    {
                        getSecondary = false;
                        if (b.isPartOfComposite) waitForComposite = true;
                        continue;
                    }

                    return i;
                }

                return -1;
            }
        }

        /// <summary>
        /// Returns the input for a given shortcut.
        /// </summary>
        /// <param name="shortcut">The shortcut to get the input for.</param>
        /// <returns>A <see cref="InputButton"/> to subscribe actions to.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when Shortcut is not implemented.</exception>
        public static InputButton GetInput(ShortcutType shortcut)
        {
            InputSystem input = InputSystem.GetInstance();
            InputButton shortcutEvent = shortcut switch
            {
                ShortcutType.Undo => input.Shortcuts.Undo,
                ShortcutType.Redo => input.Shortcuts.Redo,
                ShortcutType.Save => input.Shortcuts.Save,
                ShortcutType.Cancel => input.Shortcuts.Cancel,
                ShortcutType.NewAsset => input.Shortcuts.NewAsset,
                ShortcutType.Edit => input.Shortcuts.Edit,
                ShortcutType.EditProperties => input.Shortcuts.EditProperties,
                ShortcutType.Delete => input.Shortcuts.Delete,
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
                ShortcutType.ChangePalette => input.Shortcuts.ChangePalette,
                ShortcutType.SwitchLeft => input.Shortcuts.SwitchLeft,
                ShortcutType.SwitchRight => input.Shortcuts.SwitchRight,
                ShortcutType.RefreshCurrent => input.Shortcuts.RefreshCurrent,
                ShortcutType.RefreshAll => input.Shortcuts.RefreshAll,
                ShortcutType.SwitchToPalettes => input.Shortcuts.ShowPalettes,
                ShortcutType.SwitchToSprites => input.Shortcuts.ShowSprites,
                ShortcutType.SwitchToWeapons => input.Shortcuts.ShowWeapons,
                ShortcutType.SwitchToProjectiles => input.Shortcuts.ShowProjectiles,
                ShortcutType.SwitchToEnemies => input.Shortcuts.ShowEnemies,
                ShortcutType.SwitchToRooms => input.Shortcuts.ShowRooms,
                ShortcutType.SwitchToTiles => input.Shortcuts.ShowTiles,
                ShortcutType.ResetToDefault => input.Shortcuts.ResetToDefault,
                ShortcutType.SelectAll => input.Shortcuts.SelectAll,
                ShortcutType.DeselectAll => input.Shortcuts.DeselectAll,
                ShortcutType.SelectRandom => input.Shortcuts.SelectRandom,
                _ => throw new ArgumentOutOfRangeException($"Shortcut {shortcut} not implemented.")
            };
            return shortcutEvent;
        }
    }
}