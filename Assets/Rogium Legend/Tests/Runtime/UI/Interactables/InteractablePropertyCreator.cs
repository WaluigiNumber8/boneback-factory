using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables.Properties;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Rogium.Tests.UI.Interactables
{
    /// <summary>
    /// Creates interactable properties for testing.
    /// </summary>
    public static class InteractablePropertyCreator
    {
        private static readonly InteractablePropertyToggle toggleProperty = AssetDatabase.LoadAssetAtPath<InteractablePropertyToggle>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_Toggle_Blue.prefab");
        private static readonly InteractablePropertySlider sliderProperty = AssetDatabase.LoadAssetAtPath<InteractablePropertySlider>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_Slider_Horizontal_Input_Blue.prefab");
        private static readonly InteractablePropertyInputField inputFieldProperty = AssetDatabase.LoadAssetAtPath<InteractablePropertyInputField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_InputField_Blue.prefab");
        private static readonly InteractablePropertyDropdown dropdownProperty = AssetDatabase.LoadAssetAtPath<InteractablePropertyDropdown>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_Dropdown_Blue.prefab");
        private static readonly InteractablePropertyAssetField assetFieldProperty = AssetDatabase.LoadAssetAtPath<InteractablePropertyAssetField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_AssetField_Blue.prefab");
        private static readonly InteractablePropertyAssetField assetFieldTextProperty = AssetDatabase.LoadAssetAtPath<InteractablePropertyAssetField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_AssetField_Text_Blue.prefab");
        private static readonly InteractablePropertyColorField colorFieldProperty = AssetDatabase.LoadAssetAtPath<InteractablePropertyColorField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_ColorField_Blue.prefab");
        private static readonly InteractablePropertySoundField soundFieldProperty = AssetDatabase.LoadAssetAtPath<InteractablePropertySoundField>("Assets/Rogium Legend/Prefabs/UI/Interactables/Properties/Horizontal/pref_Property_SoundField_Blue.prefab");

        public static InteractablePropertyToggle CreateAndInitToggle(bool value = false)
        {
            InteractablePropertyToggle toggle = Object.Instantiate(toggleProperty, Vector3.zero, Quaternion.identity);
            toggle.Construct("Test Toggle", value, null);
            return toggle;
        }

        public static InteractablePropertySlider CreateAndInitSlider(float value = 0)
        {
            InteractablePropertySlider slider = Object.Instantiate(sliderProperty, Vector3.zero, Quaternion.identity);
            slider.Construct("Test Slider", 0f, 1f, value, null);
            return slider;
        }

        public static InteractablePropertySlider CreateAndInitSlider(int value = 0)
        {
            InteractablePropertySlider slider = Object.Instantiate(sliderProperty, Vector3.zero, Quaternion.identity);
            slider.Construct("Test Slider", 0, 1, value, null);
            return slider;
        }

        public static InteractablePropertyInputField CreateAndInitInputField(string value = "")
        {
            InteractablePropertyInputField inputField = Object.Instantiate(inputFieldProperty, Vector3.zero, Quaternion.identity);
            inputField.Construct("Test InputField", value, null, TMP_InputField.CharacterValidation.Alphanumeric, 0f, 100f);
            return inputField;
        }

        public static InteractablePropertyDropdown CreateAndInitDropdown(int value = 0)
        {
            InteractablePropertyDropdown dropdown = Object.Instantiate(dropdownProperty, Vector3.zero, Quaternion.identity);
            dropdown.Construct("Test Dropdown", new[] {"Option 1", "Option 2", "Option 3"}, value, null);
            return dropdown;
        }

        public static InteractablePropertyAssetField CreateAndInitAssetField(AssetType type, IAsset value = null)
        {
            InteractablePropertyAssetField assetField = Object.Instantiate(assetFieldProperty, Vector3.zero, Quaternion.identity);
            assetField.Construct("Test AssetField", type, value, null);
            return assetField;
        }

        public static InteractablePropertyAssetField CreateAndInitAssetFieldText(AssetType type = AssetType.None, IAsset value = null)
        {
            InteractablePropertyAssetField assetField = Object.Instantiate(assetFieldTextProperty, Vector3.zero, Quaternion.identity);
            assetField.Construct("Test AssetField", type, value, null);
            return assetField;
        }

        public static InteractablePropertyColorField CreateAndInitColorField(Color value = new())
        {
            InteractablePropertyColorField colorField = Object.Instantiate(colorFieldProperty, Vector3.zero, Quaternion.identity);
            colorField.Construct("Test ColorField", value, null);
            return colorField;
        }

        public static InteractablePropertySoundField CreateAndInitSoundField(AssetData value = null)
        {
            InteractablePropertySoundField soundField = Object.Instantiate(soundFieldProperty, Vector3.zero, Quaternion.identity);
            soundField.Construct("Test SoundField", value, null);
            return soundField;
        }
    }
}