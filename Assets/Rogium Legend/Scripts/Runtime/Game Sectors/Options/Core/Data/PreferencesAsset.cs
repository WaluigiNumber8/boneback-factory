using Rogium.Editors.Core;
using Rogium.Options.OptionControllers;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Holds all of user's preferences for the game.
    /// </summary>
    public class PreferencesAsset : IDataAsset
    {
        private float masterVolume;
        private float musicVolume;
        private float soundVolume;
        private float uiVolume;
        
        private Vector2Int resolution;
        private ScreenType screenMode;
        private bool vSync;

        private PreferencesAsset() { }
        
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

        public class Builder
        {
            private readonly PreferencesAsset asset = new();
            
            public Builder()
            {
                asset.masterVolume = PreferencesDefaults.MasterVolume;
                asset.musicVolume = PreferencesDefaults.MusicVolume;
                asset.soundVolume = PreferencesDefaults.SoundVolume;
                asset.uiVolume = PreferencesDefaults.UIVolume;
                asset.resolution = PreferencesDefaults.Resolution;
                asset.screenMode = PreferencesDefaults.ScreenMode;
                asset.vSync = PreferencesDefaults.VSync;
            }

            public Builder WithMasterVolume(float masterVolume)
            {
                asset.masterVolume = masterVolume;
                return this;
            }
            
            public Builder WithMusicVolume(float musicVolume)
            {
                asset.musicVolume = musicVolume;
                return this;
            }
            
            public Builder WithSoundVolume(float soundVolume)
            {
                asset.soundVolume = soundVolume;
                return this;
            }
            
            public Builder WithUIVolume(float uiVolume)
            {
                asset.uiVolume = uiVolume;
                return this;
            }
            
            public Builder WithResolution(Vector2Int resolution)
            {
                asset.resolution = resolution;
                return this;
            }
            
            public Builder WithScreenMode(ScreenType screenMode)
            {
                asset.screenMode = screenMode;
                return this;
            }
            
            public Builder WithVSync(bool vSync)
            {
                asset.vSync = vSync;
                return this;
            }
            
            /// <summary>
            /// Copies the values from another asset.
            /// </summary>
            /// <param name="asset">The asset to copy from.</param>
            public Builder AsCopy(PreferencesAsset asset)
            {
                this.asset.masterVolume = asset.MasterVolume;
                this.asset.musicVolume = asset.MusicVolume;
                this.asset.soundVolume = asset.SoundVolume;
                this.asset.uiVolume = asset.UIVolume;
                     
                this.asset.resolution = asset.Resolution;
                this.asset.screenMode = asset.ScreenMode;
                this.asset.vSync = asset.VSync;
                return this;
            }
            
            public PreferencesAsset Build() => asset;
        }
    }
}