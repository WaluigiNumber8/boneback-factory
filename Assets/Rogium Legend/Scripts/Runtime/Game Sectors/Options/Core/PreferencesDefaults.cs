using Rogium.Options.OptionControllers;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Contains defaults for in-game preferences.
    /// </summary>
    public static class PreferencesDefaults
    {
        //Audio
        public const float MasterVolume = 1f;
        public const float MusicVolume = 1f;
        public const float SoundVolume = 1f;
        public const float UIVolume = 1f;
        
        //Video
        public const ScreenType ScreenMode = ScreenType.Fullscreen;
        public static readonly Vector2Int Resolution = new(Screen.currentResolution.width, Screen.currentResolution.height);
        public const bool VSync = false;
    }
}