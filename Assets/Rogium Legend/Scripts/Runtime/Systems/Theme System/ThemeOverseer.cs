using BoubakProductions.Safety;
using BoubakProductions.UI.Core;
using UnityEngine;

namespace Rogium.Systems.ThemeSystem
{
    /// <summary>
    /// Overseers information regarding the editor's theme.
    /// </summary>
    public class ThemeOverseer
    {
        private ThemeStyleInfo[] themes;
        private ThemeStyleInfo currentTheme;

        #region Singleton Pattern
        private static ThemeOverseer instance;
        private static readonly object padlock = new object();

        public static ThemeOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new ThemeOverseer();
                    return instance;
                }
            }
        }

        #endregion
        
        public void Initialize(ThemeStyleInfo[] themes, int startingTheme)
        {
            SafetyNet.EnsureIntIsInRange(startingTheme, 0, themes.Length, "Themes to set");
            
            this.themes = themes;
            currentTheme = themes[startingTheme];
        }

        /// <summary>
        /// Changes the active theme of the editor.
        /// </summary>
        /// <param name="newTheme">The new theme to use.</param>
        public void ChangeTheme(ThemeType newTheme)
        {
            currentTheme = themes[(int)newTheme];
        }

        /// <summary>
        /// Gets an element from the active editor theme.
        /// </summary>
        /// <param name="element">The element type.</param>
        /// <returns>The element as a sprite.</returns>
        public Sprite GetElement(ThemeElementType element)
        {
            return currentTheme.elements[(int)element];
        }

        /// <summary>
        /// Gets an interactable sprite set from the current theme.
        /// </summary>
        /// <param name="interactable">The type of interactable.</param>
        /// <returns>Sprite set for an interactable.</returns>
        public InteractableInfo GetInteractable(ThemeInteractableType interactable)
        {
            return currentTheme.interactables[(int)interactable];
        }
        
        /// <summary>
        /// Gets a font from the active editor theme.
        /// </summary>
        /// <param name="font">The font type to get.</param>
        /// <returns>TMPro Font Asset.</returns>
        public FontInfo GetFont(ThemeFontType font)
        {
            return currentTheme.fonts[(int)font];
        }
    }
}