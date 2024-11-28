using System;
using RedRats.Systems.FileSystem;
using Rogium.Options.Core;
using Rogium.Options.OptionControllers;
using UnityEngine;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PreferencesAsset"/>.
    /// </summary>
    [Serializable]
    public class JSONPreferencesAsset : IEncodedObject<PreferencesAsset>
    {
        public float MasterVolume, MusicVolume, SoundVolume, UIVolume;
        public int ResolutionX, ResolutionY;
        public int ScreenMode;
        public bool VSync;

        public JSONPreferencesAsset(PreferencesAsset preferences)
        {
            MasterVolume = preferences.MasterVolume;
            MusicVolume = preferences.MusicVolume;
            SoundVolume = preferences.SoundVolume;
            UIVolume = preferences.UIVolume;
            
            ResolutionX = preferences.Resolution.x;
            ResolutionY = preferences.Resolution.y;
            ScreenMode = (int) preferences.ScreenMode;
            VSync = preferences.VSync;
        }
        
        public PreferencesAsset Decode()
        {
            return new PreferencesAsset.Builder()
                .WithMasterVolume(MasterVolume)
                .WithMusicVolume(MusicVolume)
                .WithSoundVolume(SoundVolume)
                .WithUIVolume(UIVolume)
                .WithResolution(new Vector2Int(ResolutionX, ResolutionY))
                .WithScreenMode((ScreenType) ScreenMode)
                .WithVSync(VSync)
                .Build();
        }
    }
}