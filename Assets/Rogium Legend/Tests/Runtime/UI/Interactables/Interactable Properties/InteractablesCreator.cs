using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Systems.Input;
using Rogium.UserInterface.Interactables.Properties;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Rogium.Tests.UI.Interactables.Properties
{
    /// <summary>
    /// Creates interactable properties for testing.
    /// </summary>
    public static class InteractablesCreator
    {
        private static readonly IPToggle toggleProperty = AssetDatabase.LoadAssetAtPath<IPToggle>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_Toggle_Blue.prefab");
        private static readonly IPSlider sliderProperty = AssetDatabase.LoadAssetAtPath<IPSlider>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_Slider_Horizontal_Input_Blue.prefab");
        private static readonly IPInputField inputFieldProperty = AssetDatabase.LoadAssetAtPath<IPInputField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_InputField_Blue.prefab");
        private static readonly IPDropdown dropdownProperty = AssetDatabase.LoadAssetAtPath<IPDropdown>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_Dropdown_Blue.prefab");
        private static readonly IPAssetField assetFieldProperty = AssetDatabase.LoadAssetAtPath<IPAssetField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_AssetField_Blue.prefab");
        private static readonly IPAssetField assetFieldTextProperty = AssetDatabase.LoadAssetAtPath<IPAssetField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_AssetField_Text_Blue.prefab");
        private static readonly IPColorField colorFieldProperty = AssetDatabase.LoadAssetAtPath<IPColorField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_ColorField_Blue.prefab");
        private static readonly IPSoundField soundFieldProperty = AssetDatabase.LoadAssetAtPath<IPSoundField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_SoundField_Blue.prefab");
        private static readonly IPSoundField assetEmblemListProperty = AssetDatabase.LoadAssetAtPath<IPSoundField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_AssetEmblemList_Blue.prefab");
        private static readonly IPInputBinding inputBindingProperty = AssetDatabase.LoadAssetAtPath<IPInputBinding>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_InputBinding_Blue.prefab");
        
        public static IPToggle CreateAndInitToggle(bool value = false)
        {
            IPToggle toggle = Object.Instantiate(toggleProperty, Vector3.zero, Quaternion.identity);
            toggle.Construct("Test Toggle", value, null);
            return toggle;
        }

        public static IPSlider CreateAndInitSlider(float value = 0)
        {
            IPSlider slider = Object.Instantiate(sliderProperty, Vector3.zero, Quaternion.identity);
            slider.Construct("Test Slider", 0f, 1f, value, null);
            return slider;
        }

        public static IPSlider CreateAndInitSlider(int value = 0)
        {
            IPSlider slider = Object.Instantiate(sliderProperty, Vector3.zero, Quaternion.identity);
            slider.Construct("Test Slider", 0, 1, value, null);
            return slider;
        }

        public static IPInputField CreateAndInitInputField(string value = "")
        {
            IPInputField inputField = Object.Instantiate(inputFieldProperty, Vector3.zero, Quaternion.identity);
            inputField.Construct("Test InputField", value, null, TMP_InputField.CharacterValidation.Alphanumeric, 0f, 100f);
            return inputField;
        }

        public static IPDropdown CreateAndInitDropdown(int value = 0)
        {
            IPDropdown dropdown = Object.Instantiate(dropdownProperty, Vector3.zero, Quaternion.identity);
            dropdown.Construct("Test Dropdown", new[] {"Option 1", "Option 2", "Option 3"}, value, null);
            return dropdown;
        }

        public static IPAssetField CreateAndInitAssetField(AssetType type, IAsset value = null)
        {
            IPAssetField assetField = Object.Instantiate(assetFieldProperty, Vector3.zero, Quaternion.identity);
            assetField.Construct("Test AssetField", type, value ?? new EmptyAsset(), null, () => { });
            return assetField;
        }

        public static IPAssetField CreateAndInitAssetFieldText(AssetType type = AssetType.None, IAsset value = null)
        {
            IPAssetField assetField = Object.Instantiate(assetFieldTextProperty, Vector3.zero, Quaternion.identity);
            assetField.Construct("Test AssetField", type, value, null);
            return assetField;
        }

        public static IPColorField CreateAndInitColorField(Color value = new())
        {
            IPColorField colorField = Object.Instantiate(colorFieldProperty, Vector3.zero, Quaternion.identity);
            colorField.Construct("Test ColorField", value, null);
            return colorField;
        }

        public static IPSoundField CreateAndInitSoundField(AssetData value = null)
        {
            IPSoundField soundField = Object.Instantiate(soundFieldProperty, Vector3.zero, Quaternion.identity);
            soundField.Construct("Test SoundField", value, null);
            return soundField;
        }
        
        public static IPInputBinding BuildInputBinding(InputAction action, bool useAlt = true, InputDeviceType device = InputDeviceType.Keyboard)
        {
            UIPropertyBuilder.Instance.BuildInputBinding(action, device, Object.FindFirstObjectByType<Canvas>().transform, useAlt);
            return Object.FindFirstObjectByType<IPInputBinding>();
        }
    }
}