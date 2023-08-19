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
        /// <param name="mode"></param>
        public void SetScreen(FullScreenMode mode)
        {
            Screen.fullScreenMode = mode;
            Screen.fullScreen = mode is FullScreenMode.FullScreenWindow or FullScreenMode.ExclusiveFullScreen;
        }
    }
}