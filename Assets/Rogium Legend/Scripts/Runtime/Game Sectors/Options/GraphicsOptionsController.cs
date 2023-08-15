using UnityEngine;

namespace Rogium.Options.OptionControllers
{
    /// <summary>
    /// Controls the visuals of the game.
    /// </summary>
    public class GraphicsOptionsController
    {
        public void SetScreen(FullScreenMode mode)
        {
            Screen.fullScreenMode = mode;
        }
    }
}