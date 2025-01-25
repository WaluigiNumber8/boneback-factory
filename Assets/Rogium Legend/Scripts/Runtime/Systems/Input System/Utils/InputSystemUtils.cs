using System;
using System.Text;
using System.Text.RegularExpressions;
using Rogium.Systems.Shortcuts;
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
                InputDeviceType.Keyboard => GetBindingIndex(new InputBinding(groups: InputSystem.GetInstance().KeyboardBindingGroup, path: null)),
                InputDeviceType.Gamepad => GetBindingIndex(new InputBinding(groups: InputSystem.GetInstance().GamepadBindingGroup, path: null)),
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
        /// Formats a <see cref="InputControl"/> path to a <see cref="InputBinding"/> path style.
        /// </summary>
        /// <param name="path">The path to format</param>
        public static string FormatForBindingPath(this string path)
        {
            string formatForBindingPath = Regex.Replace(path, @"^/(\w+)", "<$1>");
            formatForBindingPath = Regex.Replace(formatForBindingPath, "^/", "");
            return formatForBindingPath;
        }

        /// <summary>
        /// Get a human-readable path for a specific action.
        /// </summary>
        /// <param name="action">The action to get the binding path from.</param>
        /// <param name="device">For which device to take the path for</param>
        /// <param name="useAlt">Use the main or alt binding?</param>
        /// <param name="indexOverride">Use custom index instead of detecting by device.</param>
        /// <returns>A human-readable path.</returns>
        public static string GetPath(InputAction action, InputDeviceType device, bool useAlt = false, int indexOverride = -1)
        {
            const string mouseDevice = "<Mouse>/";
            string devicePath =(device == InputDeviceType.Gamepad) ? "<Gamepad>/" : "<Keyboard>/";
            int index = (indexOverride > -1) ? indexOverride : GetBindingIndexByDevice(action, device, useAlt);
            
            if (action.bindings[index].isPartOfComposite)
            {
                if (action.bindings[index - 1].path == nameof(TwoOptionalModifiersComposite).Replace("Composite", ""))
                {
                    StringBuilder path = new();
                    path.Append($"{action.bindings[index].effectivePath.Replace(devicePath, "").Replace(mouseDevice, "")}");     //Modifier 1
                    path.Append((action.bindings[index].effectivePath == "") ? "" : "+");                                        //Plus
                    path.Append($"{action.bindings[index + 1].effectivePath.Replace(devicePath, "").Replace(mouseDevice, "")}"); //Modifier 2
                    path.Append((action.bindings[index + 1].effectivePath == "") ? "" : "+");                                    //Plus
                    path.Append($"{action.bindings[index + 2].effectivePath.Replace(devicePath, "").Replace(mouseDevice, "")}"); //Button
                    return path.ToString();
                }
            }
            return action.bindings[index].effectivePath.Replace(devicePath, "").Replace(mouseDevice, "");
        }

        /// <summary>
        /// Converts a human-readable path and overrides a binding for a specific action.
        /// </summary>
        /// <param name="path">The human-readable path to use for the override.</param>
        /// <param name="action">The action to affect.</param>
        /// <param name="device">The device, the binding belongs to.</param>
        /// <param name="useAlt">Use the main or alt binding.</param>
        /// <param name="indexOverride">Use custom index instead of detecting by device.</param>
        public static void ApplyBindingOverride(string path, InputAction action, InputDeviceType device, bool useAlt = false, int indexOverride = -1)
        {
            const string mouseDevice = "<Mouse>/";
            int index = (indexOverride > -1) ? indexOverride : GetBindingIndexByDevice(action, device, useAlt);
            string devicePath = (device == InputDeviceType.Gamepad) ? "<Gamepad>/" : "<Keyboard>/";
            string finalDevice = "";
            
            if (action.bindings[index].isPartOfComposite)
            {
                if (action.bindings[index - 1].path == nameof(TwoOptionalModifiersComposite).Replace("Composite", ""))
                {
                    string[] paths = path.Split('+');
                    for (int i = 0; i < paths.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(paths[i])) finalDevice = (device == InputDeviceType.Keyboard && IsForMouse(paths[i])) ? mouseDevice : devicePath;
                        paths[i] = $"{finalDevice}{paths[i]}";
                    }

                    switch (paths.Length)
                    {
                        case 1:
                            action.ApplyBindingOverride(index + 2, paths[0]);
                            return;
                        case 2:
                            action.ApplyBindingOverride(index, paths[0]);
                            action.ApplyBindingOverride(index + 2, paths[1]);
                            return;
                        case 3:
                            action.ApplyBindingOverride(index, paths[0]);
                            action.ApplyBindingOverride(index + 1, paths[1]);
                            action.ApplyBindingOverride(index + 2, paths[2]);
                            return;
                    }
                }
            }
            if (!string.IsNullOrEmpty(path)) finalDevice = (device == InputDeviceType.Keyboard && IsForMouse(path)) ? mouseDevice : devicePath;
            action.ApplyBindingOverride(index, $"{finalDevice}{path}");
         
            bool IsForMouse(string humanReadablePath) => (humanReadablePath is "leftButton" or "rightButton" or "middleButton" or "forwardButton" or "backButton");
        }
    }
}