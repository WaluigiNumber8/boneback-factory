using BoubakProductions.UI;
using UnityEngine.UI;

namespace Rogium.Systems.ThemeSystem
{
    /// <summary>
    /// Updates various special UI elements with correct sprites based on the current theme.
    /// </summary>
    public static class ThemeUpdater
    {
        private static ThemeStyleAsset t;
        
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
        /// Updates the Modal Window with correct data from the current theme.
        /// </summary>
        /// <param name="window">The modal window to update.</param>
        public static void UpdateModalWindow(ModalWindow window, ThemeType theme = ThemeType.NoTheme)
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