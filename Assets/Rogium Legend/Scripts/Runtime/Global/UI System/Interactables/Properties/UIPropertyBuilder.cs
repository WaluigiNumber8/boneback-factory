using BoubakProductions.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rogium.Global.UISystem.Interactables.Properties
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
        [SerializeField] private InteractablePropertySprite spriteProperty;
        [SerializeField] private InteractablePropertyToggle toggleProperty;
        [SerializeField] private InteractablePropertyDropdown dropdownProperty;

        /// <summary>
        /// Builds the Input Field Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="OnChangeValue">What happens when the property changes value.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyInputField BuildInputField(string title, string value, Transform parent, Action<string> OnChangeValue)
        {
            InteractablePropertyInputField inputField = Instantiate(inputFieldProperty, parent).GetComponent<InteractablePropertyInputField>();
            inputField.Set(title, value, OnChangeValue);
            return inputField;
        }

        /// <summary>
        /// Builds the Input Field Area Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="OnChangeValue">What happens when the property changes value.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyInputField BuildInputFieldArea(string title, string value, Transform parent, Action<string> OnChangeValue)
        {
            InteractablePropertyInputField inputField = Instantiate(inputFieldAreaProperty, parent).GetComponent<InteractablePropertyInputField>();
            inputField.Set(title, value, OnChangeValue);
            return inputField;
        }

        /// <summary>
        /// Builds the Toggle Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="OnChangeValue">What happens when the property changes value.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyToggle BuildToggle(string title, bool value, Transform parent, Action<bool> OnChangeValue)
        {
            InteractablePropertyToggle toggle = Instantiate(toggleProperty, parent).GetComponent<InteractablePropertyToggle>();
            toggle.Set(title, value, OnChangeValue);
            return toggle;
        }

        /// <summary>
        /// Builds the Dropdown Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="options">The list of options the dropdown will be filled with.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <param name="OnChangeValue">What happens when the property changes value.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertyDropdown BuildDropdown(string title, IList<string> options, int value, Transform parent, Action<int> OnChangeValue)
        {
            InteractablePropertyDropdown dropdown = Instantiate(dropdownProperty, parent).GetComponent<InteractablePropertyDropdown>();
            dropdown.Set(title, options, value, OnChangeValue);
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
            plainText.Set(title, value);
            return plainText;
        }

        /// <summary>
        /// Builds the Sprite Property.
        /// </summary>
        /// <param name="title">Name of the property.</param>
        /// <param name="value">Starting value of the property</param>
        /// <param name="parent">Under which transform is this property going to be created.</param>
        /// <returns>The property itself.</returns>
        public InteractablePropertySprite BuildSprite(string title, Sprite value, Transform parent)
        {
            InteractablePropertySprite spriteField = Instantiate(spriteProperty, parent).GetComponent<InteractablePropertySprite>();
            spriteField.Set(title, value);
            return spriteField;
        }
    }
}