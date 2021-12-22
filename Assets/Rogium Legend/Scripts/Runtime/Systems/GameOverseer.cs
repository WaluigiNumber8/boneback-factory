using Rogium.Global.PlayerData;

namespace Rogium.Core
{
    /// <summary>
    /// Overseers everything that is happening in-game.
    /// </summary>
    public class GameOverseer
    {
        private PlayerProfileAsset playerData;

        #region Singleton Pattern
        private static GameOverseer instance;
        private static readonly object padlock = new object();
        public static GameOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new GameOverseer();
                    return instance;
                }
            }
        }

        #endregion
        
        private GameOverseer()
        {
            // PlayerProfile.SetPlayerData(playerData);
        }
    }
}