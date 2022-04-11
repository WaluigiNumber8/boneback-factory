using BoubakProductions.UI;
using Rogium.UserInterface.Editors.AssetSelection.PickerVariant;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine.UI;

namespace Rogium.Systems.ThemeSystem
{
    /// <summary>
    /// Updates various special UI elements with correct sprites based on the current theme.
    /// </summary>
    public static class ThemeUpdaterRogium
    {
        private static ThemeStyleAsset t;

        /// <summary>
        /// Updates a button with correct data from the current theme.
        /// Style is "Menu."
        /// </summary>
        /// <param name="button">The button to update.</param>
        public static void UpdateButtonMenu(InteractableButton button, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            button.UpdateTheme(t.Interactables.buttonMenu,
                               t.Fonts.general);
        }
        
        /// <summary>
        /// Updates a button with correct data from the current theme.
        /// Style is "Card."
        /// </summary>
        /// <param name="button">The button to update.</param>
        public static void UpdateButtonCard(InteractableButton button, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            button.UpdateTheme(t.Interactables.buttonCard,
                               t.Fonts.general);
        }
        
        /// <summary>
        /// Updates an input field with correct data from the current theme.
        /// </summary>
        /// <param name="inputField">The input field to update.</param>
        public static void UpdateInputField(InteractablePropertyInputField inputField, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            inputField.UpdateTheme(t.Interactables.inputField,
                                   t.Fonts.general,
                                   t.Fonts.inputted);
        }

        /// <summary>
        /// Updates a toggle with correct data from the current theme.
        /// </summary>
        /// <param name="toggle">The toggle to update.</param>
        public static void UpdateToggle(InteractablePropertyToggle toggle, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            toggle.UpdateTheme(t.Interactables.toggle,
                               t.Elements.toggleCheckmark);
        }

        /// <summary>
        /// Updates a dropdown with correct data from the current theme.
        /// </summary>
        /// <param name="dropdown">The dropdown to update.</param>
        public static void UpdateDropdown(InteractablePropertyDropdown dropdown, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            dropdown.UpdateTheme(t.Interactables.inputField,
                                 t.Interactables.inputField,
                                 t.Elements.dropdownHeader,
                                 t.Elements.dropdownBackground,
                                 t.Elements.dropdownArrow,
                                 t.Elements.toggleCheckmark,
                                 t.Fonts.general,
                                 t.Fonts.inputted);
        }

        /// <summary>
        /// Updates a plain text property with correct data from the current theme.
        /// </summary>
        /// <param name="text">The text property to update.</param>
        public static void UpdatePlainText(InteractablePropertyPlainText text, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            text.UpdateTheme(t.Fonts.general,
                             t.Fonts.inputted);
        }

        /// <summary>
        /// Updates a asset field with correct data from the current theme.
        /// </summary>
        /// <param name="assetField">The AssetField to update.</param>
        public static void UpdateAssetField(InteractablePropertyAssetField assetField, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            assetField.UpdateTheme(t.Interactables.assetField);
        }

        /// <summary>
        /// Updates the slider with correct data from the current theme.
        /// </summary>
        /// <param name="slider">The slider property to update.</param>
        public static void UpdateSlider(InteractablePropertySlider slider, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            slider.UpdateTheme(t.Interactables.slider,
                               t.Elements.sliderBackground,
                               t.Elements.sliderHandle,
                               t.Fonts.general);
        }

        /// <summary>
        /// Updates the header with correct data from the current theme.
        /// </summary>
        /// <param name="header">The header property to update.</param>
        public static void UpdateHeader(InteractablePropertyHeader header, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            header.UpdateTheme(t.Fonts.header,
                               t.Elements.editorBackground);
        }

        /// <summary>
        /// Updates an editor element with correct data from the correct theme.
        /// </summary>
        /// <param name="element">The element to update.</param>
        public static void UpdateElement(Image element, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            element.sprite = t.Elements.editorBackground;
        }

        /// <summary>
        /// Updates the Asset Picker Window with correct data from the current theme.
        /// </summary>
        /// <param name="window"></param>
        public static void UpdateAssetPickerWindow(AssetPickerWindow window, ThemeType theme = ThemeType.NoTheme)
        {
            UpdateUsedTheme(theme);
            window.UpdateTheme(t.Elements.modalWindowBackground,
                               t.Interactables.inputField.normal,
                               t.Interactables.buttonMenu,
                               t.Fonts.general,
                               t.Fonts.inputted);
        }
        
        /// <summary>
        /// Gets the correct theme to work with.
        /// </summary>
        /// <param name="theme">The theme to get.</param>
        /// <returns></returns>
        private static void UpdateUsedTheme(ThemeType theme = ThemeType.NoTheme)
        {
            t = (theme == ThemeType.NoTheme) ? ThemeOverseerMono.GetInstance().CurrentThemeData : ThemeOverseerMono.GetInstance().GetTheme(theme);
        }
    }
}