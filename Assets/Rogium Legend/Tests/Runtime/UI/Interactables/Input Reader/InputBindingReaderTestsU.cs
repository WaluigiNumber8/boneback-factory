using Rogium.Systems.Input;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rogium.Tests.UI.Interactables.InputReader
{
    public static class InputBindingReaderTestsU
    {
        public static InputBindingReader BuildInputReader(InputAction action)
        {
            Transform canvas = Object.FindFirstObjectByType<Canvas>().transform;
            UIPropertyBuilder.GetInstance().BuildInputBinding(action, InputDeviceType.Keyboard, canvas);
            return canvas.GetChild(canvas.childCount-1).GetComponentInChildren<InputBindingReader>();
        }
    }
}