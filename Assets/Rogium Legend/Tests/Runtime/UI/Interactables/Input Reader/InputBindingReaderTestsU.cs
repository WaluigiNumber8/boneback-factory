using Rogium.UserInterface.Interactables;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rogium.Tests.UI.Interactables
{
    public static class InputBindingReaderTestsU
    {
        private static readonly InputBindingReader inputReaderProperty = AssetDatabase.LoadAssetAtPath<InputBindingReader>("Assets/Rogium Legend/Prefabs/UI/Interactables/Input Binding Readers/pref_InputBindingReader_Blue.prefab");

        public static InputBindingReader BuildInputReader(InputAction action)
        {
            InputBindingReader reader = Object.Instantiate(inputReaderProperty, Object.FindFirstObjectByType<Canvas>().transform);
            reader.Construct(action);
            return reader;
        }
    }
}