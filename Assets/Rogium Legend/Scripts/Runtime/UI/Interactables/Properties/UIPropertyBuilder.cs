using BoubakProductions.Core;
using Rogium.Systems.ThemeSystem;
using System;
using System.Collections.Generic;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Builds Properties.
    /// </summary>
    public class UIPropertyBuilder : MonoSingleton<UIPropertyBuilder>
    {
        [Header("Property prefabs")]
        [SerializeField] private InteractablePropertyPlainText plainTextProperty;
        [SerializeField] private InteractablePropertyInputField inputFieldProperty;
        [SerializeField] private InteractablePropertyInputField inputFieldAreaProperty;
        [SerializeField] private InteractablePropertyAssetField assetProperty;
        [SerializeField] private InteractablePropertyToggle toggleProperty;
        [SerializeField] private InteractablePropertyDropdown dropdownProperty;

        private readonly ThemeUpdater themeUpdater = new ThemeUpdater();
        
        /// <summary>
        /// Builds the Input Field Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="WhenChangeValue">What happens when the property changes value.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyInputField BuildInputField(string title, string value, Transform parent, Action<string> WhenChangeValue)
        {
            InteractablePropertyInputField inputField = Instantiate(inputFieldProperty, parent).GetComponent<InteractablePropertyInputField>();
            inputField.Construct(title, value, WhenChangeValue);
            themeUpdater.UpdateInputField(inputField);
            return inputField;
        }

        /// <summary>
        /// Builds the Input Field Area Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="WhenChangeValue">What happens when the property changes value.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyInputField BuildInputFieldArea(string title, string value, Transform parent, Action<string> WhenChangeValue)
        {
            InteractablePropertyInputField inputField = Instantiate(inputFieldAreaProperty, parent).GetComponent<InteractablePropertyInputField>();
            inputField.Construct(title, value, WhenChangeValue);
            themeUpdater.UpdateInputField(inputField);
            return inputField;
        }

        /// <summary>
        /// Builds the Toggle Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="WhenChangeValue">What happens when the property changes value.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyToggle BuildToggle(string title, bool value, Transform parent, Action<bool> WhenChangeValue)
        {
            InteractablePropertyToggle toggle = Instantiate(toggleProperty, parent).GetComponent<InteractablePropertyToggle>();
            toggle.Construct(title, value, WhenChangeValue);
            themeUpdater.UpdateToggle(toggle);
            return toggle;
        }

        /// <summary>
        /// Builds the Dropdown Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="options">The list of options the dropdown will be filled with.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="WhenChangeValue">What happens when the property changes value.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyDropdown BuildDropdown(string title, IList<string> options, int value, Transform parent, Action<int> WhenChangeValue)
        {
            InteractablePropertyDropdown dropdown = Instantiate(dropdownProperty, parent).GetComponent<InteractablePropertyDropdown>();
            dropdown.Construct(title, options, value, WhenChangeValue);
            themeUpdater.UpdateDropdown(dropdown);
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
            InteractablePropertyPlainText plainText = Instantiate(plainTextProperty, parent).GetComponent<InteractablePropertyPlainText>();
            plainText.Construct(title, value);
            themeUpdater.UpdatePlainText(plainText);
            return plainText;
        }

        /// <summary>
        /// Builds the Asset Field Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="type">The type of asset to work with.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="WhenChangeValue">The method that runs when the asset is changed.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyAssetField BuildAssetField(string title, AssetType type, AssetBase value, Transform parent, Action<AssetBase> WhenChangeValue)
        {
            InteractablePropertyAssetField assetField = Instantiate(assetProperty, parent).GetComponent<InteractablePropertyAssetField>();
            assetField.Construct(title, type, value, WhenChangeValue);
            themeUpdater.UpdateSpriteField(assetField);
            return assetField;
        }
    }
}