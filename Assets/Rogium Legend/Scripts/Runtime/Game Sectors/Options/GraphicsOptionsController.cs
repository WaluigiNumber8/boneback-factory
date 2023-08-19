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
        public void SetScreen(ScreenType mode)
        {
            Screen.fullScreen = (mode is ScreenType.Fullscreen);
            Screen.fullScreenMode = mode switch
            {
                ScreenType.Fullscreen => FullScreenMode.ExclusiveFullScreen,
                ScreenType.Borderless => FullScreenMode.FullScreenWindow,
                _ => FullScreenMode.Windowed
            };
        }
    }
}