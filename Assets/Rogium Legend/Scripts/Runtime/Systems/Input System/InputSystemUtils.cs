using System;
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
    }
}