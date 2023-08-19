using Rogium.Editors.Core;
using Rogium.Options.OptionControllers;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Holds all of user's preferences for the game.
    /// </summary>
    public class GameDataAsset : IDataAsset
    {
        private ScreenType screenMode;

        #region Constructors

        public GameDataAsset()
        {
            screenMode = PreferencesDefaults.ScreenMode;
        }

        public GameDataAsset(GameDataAsset asset)
        {
            screenMode = asset.ScreenMode;
        }

        public GameDataAsset(ScreenType screenMode)
        {
            this.screenMode = screenMode;
        }

        #endregion
        
        #region Update Values

        public void UpdateScreenMode(int newValue) => UpdateScreenMode((ScreenType)newValue);
        public void UpdateScreenMode(ScreenType newValue) => screenMode = newValue;

        #endregion
        
        public string ID { get => "ZX"; }
        public string Title { get => "Preferences"; }
        public ScreenType ScreenMode { get => screenMode; }
    }
}