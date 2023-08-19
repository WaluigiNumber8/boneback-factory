using Rogium.Options.OptionControllers;
using UnityEngine;

namespace Rogium.Options.Core
{
    /// <summary>
    /// Contains defaults for in-game preferences.
    /// </summary>
    public static class PreferencesDefaults
    {
        public const ScreenType ScreenMode = ScreenType.Fullscreen;
        public static readonly Vector2Int Resolution = new(Screen.currentResolution.width, Screen.currentResolution.height);
        public const bool VSync = false;
    }
}