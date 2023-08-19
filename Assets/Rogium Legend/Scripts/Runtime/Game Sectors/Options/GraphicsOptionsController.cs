using UnityEngine;

namespace Rogium.Options.OptionControllers
{
    /// <summary>
    /// Controls the visuals of the game.
    /// </summary>
    public class GraphicsOptionsController : MonoBehaviour
    {
        /// <summary>
        /// Set the screen type.
        /// </summary>
        /// <param name="mode">The mode the game is switching to.</param>
        public void UpdateScreen(ScreenType mode)
        {
            Screen.fullScreenMode = mode switch
            {
                ScreenType.Fullscreen => FullScreenMode.ExclusiveFullScreen,
                ScreenType.Borderless => FullScreenMode.FullScreenWindow,
                _ => FullScreenMode.Windowed
            };
        }

        /// <summary>
        /// Set the current resolution.
        /// </summary>
        /// <param name="resolution">The new resolution of the game.</param>
        public void UpdateResolution(Resolution resolution)
        {
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);
        }
    }
}