namespace Rogium.Systems.Input
{
    /// <summary>
    /// Overseers all input profiles and deals with their switching.
    /// </summary>
    public class InputOverseer
    {
        private readonly InputProfilePlayer inputPlayer;
        private readonly InputProfileUI inputUI;
        
        #region Singleton Pattern
        private static InputOverseer instance;
        private static readonly object padlock = new object();
        public static InputOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new InputOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private InputOverseer()
        {
            inputPlayer = new InputProfilePlayer();
            inputUI = new InputProfileUI();
        }

        /// <summary>
        /// Enables the UI Action Map.
        /// </summary>
        public void EnableUIMap()
        {
            DisableAll();
            inputUI.Enable();
        }
        
        /// <summary>
        /// Enables the Player Action Map.
        /// </summary>
        public void EnablePlayerMap()
        {
            DisableAll();
            inputPlayer.Enable();
        }
        
        /// <summary>
        /// Disables all Action Maps.
        /// </summary>
        private void DisableAll()
        {
            inputUI.Disable();
            inputPlayer.Disable();
        }
        
        public InputProfilePlayer Player { get => inputPlayer; }
        public InputProfileUI UI { get => inputUI; }
    }
}