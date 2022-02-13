using BoubakProductions.Safety;
using BoubakProductions.UI;
using BoubakProductions.UI.Core;
using UnityEngine;

namespace Rogium.Systems.ThemeSystem
{
    /// <summary>
    /// Overseers information regarding the editor's theme.
    /// </summary>
    public class ThemeOverseer
    {
        private ThemeStyleAsset[] themes;
        private ThemeStyleAsset currentTheme;

        #region Singleton Pattern
        private static ThemeOverseer instance;
        private static readonly object padlock = new object();

        public static ThemeOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    return instance ??= new ThemeOverseer();
                }
            }
        }

        #endregion
        
        /// <summary>
        /// Initializes the ThemeOverseer with variables.
        /// </summary>
        /// <param name="themes">The array of theme data.</param>
        /// <param name="startingThemeIndex">The theme to set as default.</param>
        public void Initialize(ThemeStyleAsset[] themes, int startingThemeIndex)
        {
            SafetyNet.EnsureIntIsInRange(startingThemeIndex, 0, themes.Length, "Themes to set");
            
            this.themes = themes;
            currentTheme = themes[startingThemeIndex];
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
        /// Grabs a Theme Asset.
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public ThemeStyleAsset GetTheme(ThemeType theme)
        {
            return themes[(int) theme];
        }
        
        public ThemeStyleAsset CurrentTheme { get => currentTheme; }
    }
}