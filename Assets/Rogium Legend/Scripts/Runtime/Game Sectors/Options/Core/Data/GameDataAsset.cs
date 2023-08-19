using Rogium.Editors.Core;
using Rogium.Options.OptionControllers;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Holds all of user's preferences for the game.
    /// </summary>
    public class GameDataAsset : IDataAsset
    {
        private Vector2Int resolution;
        private ScreenType screenMode;

        #region Constructors

        public GameDataAsset()
        {
            resolution = PreferencesDefaults.Resolution;
            screenMode = PreferencesDefaults.ScreenMode;
        }

        public GameDataAsset(GameDataAsset asset)
        {
            resolution = asset.Resolution;
            screenMode = asset.ScreenMode;
        }

        public GameDataAsset(Vector2Int resolution, ScreenType screenMode)
        {
            this.resolution = resolution;
            this.screenMode = screenMode;
        }

        #endregion
        
        #region Update Values

        public void UpdateResolution(Resolution newValue) => UpdateResolution(new Vector2Int(newValue.width, newValue.height));
        public void UpdateResolution(Vector2Int newValue) => resolution = newValue;
        public void UpdateScreenMode(int newValue) => UpdateScreenMode((ScreenType)newValue);
        public void UpdateScreenMode(ScreenType newValue) => screenMode = newValue;

        #endregion

        /// <summary>
        /// Returns the <see cref="Resolution"/> as a UnityEngine <see cref="UnityEngine.Resolution"/>.
        /// </summary>
        /// <returns></returns>
        public Resolution GetResolution()
        {
            Resolution res = new();
            res.width = resolution.x;
            res.height = resolution.y;
            res.refreshRateRatio = Screen.currentResolution.refreshRateRatio;
            return res;
        }

        public override string ToString() => $"{Title}";

        public string ID { get => "ZX"; }
        public string Title { get => "Preferences"; }
        public ScreenType ScreenMode { get => screenMode; }
        public Vector2Int Resolution { get => resolution; }
    }
}