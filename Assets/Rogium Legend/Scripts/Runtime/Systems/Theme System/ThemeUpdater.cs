using Rogium.UserInterface.Interactables.Properties;

namespace Rogium.Systems.ThemeSystem
{
    /// <summary>
    /// Updates various special UI elements with correct sprites based on the current theme.
    /// </summary>
    public class ThemeUpdater
    {
        private readonly ThemeOverseer theme;
        
        public ThemeUpdater()
        {
            theme = ThemeOverseer.Instance;
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
            toggle.UpdateTheme(theme.GetElement(ThemeElementType.ToggleBorder), 
                                                theme.GetInteractable(ThemeInteractableType.Toggle),
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
        /// Updates a sprite field with correct data from the current theme.
        /// </summary>
        /// <param name="spriteField">The SpriteField to update.</param>
        public void UpdateSpriteField(InteractablePropertySprite spriteField)
        {
            spriteField.UpdateTheme(theme.GetInteractable(ThemeInteractableType.SpriteField));
        }
        
    }
}