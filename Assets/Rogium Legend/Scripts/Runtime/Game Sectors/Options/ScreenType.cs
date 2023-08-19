namespace Rogium.Options.OptionControllers
{
    /// <summary>
    /// The different screen types the game can be in.
    /// </summary>
    public enum ScreenType
    {
        /// <summary>
        /// The game is in fullscreen.
        /// </summary>
        Fullscreen = 0,
        
        /// <summary>
        /// The game is in a maximized window, that has no borders.
        /// </summary>
        Borderless = 1,
        
        /// <summary>
        /// The game is in a bordered window, that can be moved around the screen.
        /// </summary>
        Windowed = 2,
    }
}