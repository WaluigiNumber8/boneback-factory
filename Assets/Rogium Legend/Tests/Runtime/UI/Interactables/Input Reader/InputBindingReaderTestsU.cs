using Rogium.Systems.Input;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rogium.Tests.UI.Interactables
{
    public static class InputBindingReaderTestsU
    {
        public static InputBindingReader BuildInputReader(InputAction action)
        {
            UIPropertyBuilder.GetInstance().BuildInputBinding(action, InputDeviceType.Keyboard, Object.FindFirstObjectByType<Canvas>().transform);
            return Object.FindFirstObjectByType<InputBindingReader>();
        }
    }
}