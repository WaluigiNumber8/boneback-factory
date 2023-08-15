using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Holds all of user's preferences for the game.
    /// </summary>
    public class GameDataAsset : IDataAsset
    {
        private FullScreenMode screenMode;

        #region Constructors

        public GameDataAsset()
        {
            screenMode = PreferencesDefaults.ScreenMode;
        }

        public GameDataAsset(GameDataAsset asset)
        {
            screenMode = asset.ScreenMode;
        }

        public GameDataAsset(FullScreenMode screenMode)
        {
            this.screenMode = screenMode;
        }

        #endregion
        
        #region Update Values

        public void UpdateScreenMode(int newValue) => UpdateScreenMode((FullScreenMode)newValue);
        public void UpdateScreenMode(FullScreenMode newValue) => screenMode = newValue;

        #endregion
        
        public string ID { get => "ZX"; }
        public string Title { get => "Preferences"; }
        public FullScreenMode ScreenMode { get => screenMode; }
    }
}