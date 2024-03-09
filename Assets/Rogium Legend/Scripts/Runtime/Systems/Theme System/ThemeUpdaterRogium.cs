using RedRats.Systems.Themes;
using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;
using Rogium.UserInterface.ModalWindows;
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
        public static void UpdateButtonMenu(InteractableButton button, ThemeType theme = ThemeType.Current)
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
        public static void UpdateButtonCard(InteractableButton button, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            button.UpdateTheme(t.Interactables.buttonCard,
                               t.Fonts.general);
        }
        
        /// <summary>
        /// Updates a scrollbar with correct data from the current theme.
        /// </summary>
        /// <param name="scrollbar">The scrollbar to update.</param>
        public static void UpdateScrollbar(InteractableScrollbar scrollbar, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            scrollbar.UpdateTheme(t.Interactables.scrollbarHandle);
        }
        
        /// <summary>
        /// Updates an input field with correct data from the current theme.
        /// </summary>
        /// <param name="inputField">The input field to update.</param>
        public static void UpdateInputField(InteractablePropertyInputField inputField, ThemeType theme = ThemeType.Current)
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
        public static void UpdateToggle(InteractablePropertyToggle toggle, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            toggle.UpdateTheme(t.Interactables.toggle,
                               t.Elements.toggleCheckmark,
                               t.Fonts.general);
        }

        /// <summary>
        /// Updates a dropdown with correct data from the current theme.
        /// </summary>
        /// <param name="dropdown">The dropdown to update.</param>
        public static void UpdateDropdown(InteractablePropertyDropdown dropdown, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            dropdown.UpdateTheme(t.Interactables.inputField,
                                 t.Interactables.dropdownItem,
                                 t.Elements.dropdownHeader,
                                 t.Elements.dropdownBackground,
                                 t.Elements.dropdownArrow,
                                 t.Elements.dropdownCheckmark,
                                 t.Fonts.general,
                                 t.Fonts.inputted);
        }

        /// <summary>
        /// Updates a plain text property with correct data from the current theme.
        /// </summary>
        /// <param name="text">The text property to update.</param>
        public static void UpdatePlainText(InteractablePropertyPlainText text, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            text.UpdateTheme(t.Fonts.general,
                             t.Fonts.inputted);
        }

        /// <summary>
        /// Updates an Asset Field with correct data from the current theme.
        /// </summary>
        /// <param name="assetField">The AssetField to update.</param>
        public static void UpdateAssetField(InteractablePropertyAssetField assetField, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            assetField.UpdateTheme(t.Interactables.assetField,
                                   t.Fonts.general,
                                   t.Fonts.inputted);
        }
        
        /// <summary>
        /// Updates an Asset Field (text variant) with correct data from the current theme.
        /// </summary>
        /// <param name="assetField">The AssetField to update.</param>
        public static void UpdateAssetFieldText(InteractablePropertyAssetField assetField, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            assetField.UpdateTheme(t.Interactables.inputField,
                                   t.Fonts.general,
                                   t.Fonts.inputted);
        }

        /// <summary>
        /// Updates the slider with correct data from the current theme.
        /// </summary>
        /// <param name="slider">The slider property to update.</param>
        public static void UpdateSlider(InteractablePropertySlider slider, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            slider.UpdateTheme(t.Interactables.slider,
                               t.Elements.sliderBackground,
                               t.Elements.sliderHandle,
                               t.Fonts.general);
            if (slider.InputField != null) UpdateInputField(slider.InputField);
        }

        /// <summary>
        /// Updates the header with correct data from the current theme.
        /// </summary>
        /// <param name="header">The header property to update.</param>
        public static void UpdateHeader(InteractablePropertyHeader header, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            header.UpdateTheme(t.Fonts.header,
                               t.Elements.editorBackground);
        }
        
        /// <summary>
        /// Updates the sound picker with correct data from the current theme.
        /// </summary>
        public static void UpdateSoundPicker(InteractablePropertySoundPicker soundPicker, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            soundPicker.UpdateTheme(t.Interactables.inputField,
                                    t.Interactables.buttonMenu, 
                                    t.Icons.play,
                                    t.Fonts.general,
                                    t.Fonts.inputted);
        }

        /// <summary>
        /// Updates an editor element with correct data from the correct theme.
        /// </summary>
        /// <param name="element">The element to update.</param>
        public static void UpdateEditorBackground(Image element, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            element.sprite = t.Elements.editorBackground;
        }

        /// <summary>
        /// Updates the Asset Picker Window with correct data from the current theme.
        /// </summary>
        public static void UpdateAssetPickerWindow(AssetPickerWindow window, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            window.UpdateTheme(t.Elements.modalWindowBackground,
                               t.Interactables.inputField.normal,
                               t.Interactables.buttonMenu,
                               t.Fonts.general,
                               t.Fonts.inputted);
        }

        /// <summary>
        /// Updates the Sound Picker Modal Window with correct data from the current theme.
        /// </summary>
        public static void UpdateSoundPickerWindow(SoundPickerModalWindow window, ThemeType theme = ThemeType.Current)
        {
            UpdateUsedTheme(theme);
            window.UpdateTheme(t.Elements.modalWindowBackground, 
                               t.Elements.toggleBorder, 
                               t.Interactables.inputField, 
                               t.Interactables.buttonMenu,
                               t.Icons.play);
        }
        
        /// <summary>
        /// Gets the correct theme to work with.
        /// </summary>
        /// <param name="theme">The theme to get.</param>
        /// <returns></returns>
        private static void UpdateUsedTheme(ThemeType theme = ThemeType.Current)
        {
            t = (theme == ThemeType.Current) ? ThemeOverseerMono.GetInstance().CurrentThemeData : ThemeOverseerMono.GetInstance().GetTheme(theme);
        }
    }
}