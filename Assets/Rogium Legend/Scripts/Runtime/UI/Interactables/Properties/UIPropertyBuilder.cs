using BoubakProductions.Core;
using Rogium.Systems.ThemeSystem;
using System;
using System.Collections.Generic;
using BoubakProductions.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using TMPro;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Builds Properties.
    /// </summary>
    public class UIPropertyBuilder : MonoSingleton<UIPropertyBuilder>
    {
        [Header("Property prefabs")] 
        [SerializeField] private InteractablePropertyHeader headerProperty;
        [SerializeField] private InteractablePropertyPlainText plainTextProperty;
        [SerializeField] private InteractablePropertyInputField inputFieldProperty;
        [SerializeField] private InteractablePropertyInputField inputFieldAreaProperty;
        [SerializeField] private InteractablePropertyAssetField assetFieldProperty;
        [SerializeField] private InteractablePropertyToggle toggleProperty;
        [SerializeField] private InteractablePropertyDropdown dropdownProperty;
        [SerializeField] private InteractablePropertySlider sliderProperty;

        /// <summary>
        /// Builds the Header Property.
        /// </summary>
        /// <param name="headerText">The text of the header.</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyHeader BuildHeader(string headerText, Transform parent)
        {
            InteractablePropertyHeader header = Instantiate(headerProperty, parent);
            header.Construct(headerText);
            ThemeUpdaterRogium.UpdateHeader(header);
            return header;
        }

        /// <summary>
        /// Builds the Input Field Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenValueChange">What happens when the property changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <param name="characterValidation">The validation to use for inputted symbols.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyInputField BuildInputField(string title, string value, Transform parent, Action<string> whenValueChange, bool isDisabled = false, TMP_InputField.CharacterValidation characterValidation = TMP_InputField.CharacterValidation.None)
        {
            InteractablePropertyInputField inputField = Instantiate(inputFieldProperty, parent);
            inputField.Construct(title, value, whenValueChange, characterValidation);
            inputField.ChangeDisabledStatus(isDisabled);
            ThemeUpdaterRogium.UpdateInputField(inputField);
            return inputField;
        }

        /// <summary>
        /// Builds the Input Field Area Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenValueChange">What happens when the property changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <param name="characterValidation">The validation to use for inputted symbols.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyInputField BuildInputFieldArea(string title, string value, Transform parent, Action<string> whenValueChange, bool isDisabled = false, TMP_InputField.CharacterValidation characterValidation = TMP_InputField.CharacterValidation.None)
        {
            InteractablePropertyInputField inputField = Instantiate(inputFieldAreaProperty, parent);
            inputField.Construct(title, value, whenValueChange, characterValidation);
            inputField.ChangeDisabledStatus(isDisabled);
            ThemeUpdaterRogium.UpdateInputField(inputField);
            return inputField;
        }

        /// <summary>
        /// Builds the Toggle Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenValueChange">What happens when the property changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyToggle BuildToggle(string title, bool value, Transform parent, Action<bool> whenValueChange, bool isDisabled = false)
        {
            InteractablePropertyToggle toggle = Instantiate(toggleProperty, parent).GetComponent<InteractablePropertyToggle>();
            toggle.Construct(title, value, whenValueChange);
            toggle.ChangeDisabledStatus(isDisabled);
            ThemeUpdaterRogium.UpdateToggle(toggle);
            return toggle;
        }

        /// <summary>
        /// Builds the Dropdown Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="options">The list of options the dropdown will be filled with.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenValueChange">What happens when the property changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyDropdown BuildDropdown(string title, IList<string> options, int value, Transform parent, Action<int> whenValueChange, bool isDisabled = false)
        {
            InteractablePropertyDropdown dropdown = Instantiate(dropdownProperty, parent);
            dropdown.Construct(title, options, value, whenValueChange);
            dropdown.ChangeDisabledStatus(isDisabled);
            ThemeUpdaterRogium.UpdateDropdown(dropdown);
            return dropdown;
        }

        /// <summary>
        /// Builds the Plain Text Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyPlainText BuildPlainText(string title, string value, Transform parent)
        {
            InteractablePropertyPlainText plainText = Instantiate(plainTextProperty, parent);
            plainText.Construct(title, value);
            ThemeUpdaterRogium.UpdatePlainText(plainText);
            return plainText;
        }

        /// <summary>
        /// Builds the Asset Field Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="type">The type of asset to work with.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="whenValueChange">The method that runs when the asset is changed.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <param name="theme">The theme for the Asset Picker Window.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyAssetField BuildAssetField(string title, AssetType type, IAsset value, Transform parent, Action<IAsset> whenValueChange, bool isDisabled = false, ThemeType theme = ThemeType.NoTheme)
        {
            InteractablePropertyAssetField assetField = Instantiate(assetFieldProperty, parent);
            assetField.Construct(title, type, value, whenValueChange, theme);
            assetField.ChangeDisabledStatus(isDisabled);
            ThemeUpdaterRogium.UpdateAssetField(assetField);
            return assetField;
        }

        /// <summary>
        /// Builds the Slider Property.
        /// </summary>
        /// <param name="title">The text of the property title.</param>
        /// <param name="minValue">Minimum allowed value on the slider.</param>
        /// <param name="maxValue">Maximum allowed value on the slider.</param>
        /// <param name="startingValue">Starting value of the slider.</param>
        /// <param name="parent">The parent under which to instantiate the property.</param>
        /// <param name="whenValueChange">Method that will run when the slider changes value.</param>
        /// <param name="isDisabled">Initialize the property as a non-interactable.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertySlider BuildSlider(string title, float minValue, float maxValue, float startingValue, Transform parent, Action<float> whenValueChange, bool isDisabled = false)
        {
            InteractablePropertySlider slider = Instantiate(sliderProperty, parent);
            slider.Construct(title, minValue, maxValue, startingValue, whenValueChange);
            slider.ChangeDisabledStatus(isDisabled);
            ThemeUpdaterRogium.UpdateSlider(slider);
            return slider;
        }
    }
}