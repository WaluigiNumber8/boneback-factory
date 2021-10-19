using Rogium.Editors.Campaign;
using Rogium.Gameplay.DataLoading;
using UnityEngine;

namespace Rogium.Gameplay.Core
{
    /// <summary>
    /// Overseers everything happening in the gameplay part of the game.
    /// </summary>
    public class GameplayOverseer
    {
        private CampaignAsset currentCampaign;
        private RoomLoader roomLoader;

        #region Singleton Pattern
        private static GameplayOverseer instance;
        private static readonly object padlock = new object();

        public static GameplayOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new GameplayOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private GameplayOverseer()
        {
            roomLoader = new RoomLoader();
        }
        
        /// <summary>
        /// Prepares the game scene.
        /// </summary>
        /// <param name="campaign"></param>
        public void PrepareGame(CampaignAsset campaign, TilemapLayer[] tilemaps, Vector3Int offset)
        {
            currentCampaign = new CampaignAsset(campaign);
            
            //TODO Create the specific flag for Rooms: Beginning Room. Then make it load here.
            roomLoader.Load(tilemaps, offset, currentCampaign.DataPack.Rooms[0], currentCampaign.DataPack);
        }
    
        public CampaignAsset CurrentCampaign
        {
            get 
            {
                if (currentCampaign == null) throw new MissingReferenceException("Current Campaign has not been set. Did you forget to activate gameplay mode?");
                return this.currentCampaign;
            }
        }
    }
}