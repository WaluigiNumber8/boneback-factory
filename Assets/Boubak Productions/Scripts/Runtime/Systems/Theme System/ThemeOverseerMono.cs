using BoubakProductions.Core;
using BoubakProductions.Safety;
using BoubakProductions.UI;
using UnityEngine;

namespace Rogium.Systems.ThemeSystem
{
    public class ThemeOverseerMono : MonoSingleton<ThemeOverseerMono>
    {
        [SerializeField] private int defaultThemeIndex;
        [SerializeField] private ThemeStyleAsset[] themes;
        
        private ThemeStyleAsset currentThemeData;
        private ThemeType currentTheme;
        
        protected override void Awake()
        {
            base.Awake();
            SafetyNet.EnsureIntIsInRange(defaultThemeIndex, 0, themes.Length, "Themes to set");
            
            currentTheme = (ThemeType) defaultThemeIndex;
            currentThemeData = themes[defaultThemeIndex];
        }
        
        /// <summary>
        /// Changes the active theme of the editor.
        /// </summary>
        /// <param name="newTheme">The new theme to use.</param>
        public void ChangeTheme(ThemeType newTheme)
        {
            currentTheme = newTheme;
            currentThemeData = themes[(int)newTheme];
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
        
        public ThemeType CurrentTheme { get => currentTheme; }
        public ThemeStyleAsset CurrentThemeData { get => currentThemeData; }
        
    }
}