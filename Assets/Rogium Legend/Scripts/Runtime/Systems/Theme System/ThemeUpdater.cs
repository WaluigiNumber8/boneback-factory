using Rogium.UserInterface.Interactables;
using Rogium.UserInterface.Interactables.Properties;

namespace Rogium.Systems.ThemeSystem
{
    /// <summary>
    /// Updates various special UI elements with correct sprites based on the current theme.
    /// </summary>
    public class ThemeUpdater
    {
        private readonly ThemeOverseer theme;
        
        public ThemeUpdater() => theme = ThemeOverseer.Instance;

        /// <summary>
        /// Updates a button with correct data from the current theme.
        /// Style is "Menu."
        /// </summary>
        /// <param name="button">The button to update.</param>
        public void UpdateButtonMenu(InteractableButton button)
        {
            button.UpdateTheme(theme.GetInteractable(ThemeInteractableType.ButtonMenu),
                               theme.GetFont(ThemeFontType.General));
        }
        
        /// <summary>
        /// Updates a button with correct data from the current theme.
        /// Style is "Card."
        /// </summary>
        /// <param name="button">The button to update.</param>
        public void UpdateButtonCard(InteractableButton button)
        {
            button.UpdateTheme(theme.GetInteractable(ThemeInteractableType.ButtonCard),
                               theme.GetFont(ThemeFontType.General));
        }
        
        /// <summary>
        /// Updates an input field with correct data from the current theme.
        /// </summary>
        /// <param name="inputField">The input field to update.</param>
        public void UpdateInputField(InteractablePropertyInputField inputField)
        {
            inputField.UpdateTheme(theme.GetInteractable(ThemeInteractableType.InputField),
                                   theme.GetFont(ThemeFontType.General),
                                   theme.GetFont(ThemeFontType.Inputted));
        }

        /// <summary>
        /// Updates a toggle with correct data from the current theme.
        /// </summary>
        /// <param name="toggle">The toggle to update.</param>
        public void UpdateToggle(InteractablePropertyToggle toggle)
        {
            toggle.UpdateTheme(theme.GetInteractable(ThemeInteractableType.Toggle),
                               theme.GetElement(ThemeElementType.ToggleCheckmark));
        }

        /// <summary>
        /// Updates a dropdown with correct data from the current theme.
        /// </summary>
        /// <param name="dropdown">The dropdown to update.</param>
        public void UpdateDropdown(InteractablePropertyDropdown dropdown)
        {
            dropdown.UpdateTheme(theme.GetInteractable(ThemeInteractableType.InputField),
                                 theme.GetInteractable(ThemeInteractableType.InputField),
                                 theme.GetElement(ThemeElementType.DropdownHeader),
                                 theme.GetElement(ThemeElementType.DropdownBackground),
                                 theme.GetElement(ThemeElementType.DropdownArrow),
                                 theme.GetElement(ThemeElementType.ToggleCheckmark),
                                 theme.GetFont(ThemeFontType.General),
                                 theme.GetFont(ThemeFontType.Inputted));
        }

        /// <summary>
        /// Updates a plain text property with correct data from the current theme.
        /// </summary>
        /// <param name="text">The text property to update.</param>
        public void UpdatePlainText(InteractablePropertyPlainText text)
        {
            text.UpdateTheme(theme.GetFont(ThemeFontType.General),
                             theme.GetFont(ThemeFontType.Inputted));
        }

        /// <summary>
        /// Updates a asset field with correct data from the current theme.
        /// </summary>
        /// <param name="assetField">The AssetField to update.</param>
        public void UpdateAssetField(InteractablePropertyAssetField assetField)
        {
            assetField.UpdateTheme(theme.GetInteractable(ThemeInteractableType.AssetField));
        }

        /// <summary>
        /// Updates the slider with correct data from the current theme.
        /// </summary>
        /// <param name="slider">The slider property to update.</param>
        public void UpdateSlider(InteractablePropertySlider slider)
        {
            slider.UpdateTheme(theme.GetInteractable(ThemeInteractableType.Slider),
                               theme.GetElement(ThemeElementType.SliderBackground),
                               theme.GetElement(ThemeElementType.SliderHandle),
                               theme.GetFont(ThemeFontType.General));
        }

        /// <summary>
        /// Updates the header with correct data from the current theme.
        /// </summary>
        /// <param name="header">The header property to update.</param>
        public void UpdateHeader(InteractablePropertyHeader header)
        {
            header.UpdateTheme(theme.GetFont(ThemeFontType.Header));
        }
        
    }
}