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
        private float masterVolume;
        private float musicVolume;
        private float soundVolume;
        private float uiVolume;
        
        private Vector2Int resolution;
        private ScreenType screenMode;
        private bool vSync;

        #region Constructors

        public GameDataAsset()
        {
            masterVolume = PreferencesDefaults.MasterVolume;
            musicVolume = PreferencesDefaults.MusicVolume;
            soundVolume = PreferencesDefaults.SoundVolume;
            uiVolume = PreferencesDefaults.UIVolume;
            
            resolution = PreferencesDefaults.Resolution;
            screenMode = PreferencesDefaults.ScreenMode;
            vSync = PreferencesDefaults.VSync;
        }

        public GameDataAsset(GameDataAsset asset)
        {
            masterVolume = asset.MasterVolume;
            musicVolume = asset.MusicVolume;
            soundVolume = asset.SoundVolume;
            uiVolume = asset.UIVolume;
            
            resolution = asset.Resolution;
            screenMode = asset.ScreenMode;
            vSync = asset.VSync;
        }

        public GameDataAsset(float masterVolume, float musicVolume, float soundVolume, float uiVolume, 
                             Vector2Int resolution, ScreenType screenMode, bool vSync)
        {
            this.masterVolume = masterVolume;
            this.musicVolume = musicVolume;
            this.soundVolume = soundVolume;
            this.uiVolume = uiVolume;
            
            this.resolution = resolution;
            this.screenMode = screenMode;
            this.vSync = vSync;
        }

        #endregion
        
        #region Update Values
        public void UpdateMasterVolume(float newValue) => masterVolume = newValue;
        public void UpdateMusicVolume(float newValue) => musicVolume = newValue;
        public void UpdateSoundVolume(float newValue) => soundVolume = newValue;
        public void UpdateUIVolume(float newValue) => uiVolume = newValue;
        public void UpdateResolution(Resolution newValue) => UpdateResolution(new Vector2Int(newValue.width, newValue.height));
        public void UpdateResolution(Vector2Int newValue) => resolution = newValue;
        public void UpdateScreenMode(int newValue) => UpdateScreenMode((ScreenType)newValue);
        public void UpdateScreenMode(ScreenType newValue) => screenMode = newValue;
        public void UpdateVSync(bool newValue) => vSync = newValue;

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
        public float MasterVolume { get => masterVolume; }
        public float MusicVolume { get => musicVolume; }
        public float SoundVolume { get => soundVolume; }
        public float UIVolume { get => uiVolume; }
        public ScreenType ScreenMode { get => screenMode; }
        public Vector2Int Resolution { get => resolution; }
        public bool VSync { get => vSync; }
    }
}