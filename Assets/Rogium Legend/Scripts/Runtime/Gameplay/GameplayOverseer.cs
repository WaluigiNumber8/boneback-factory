using Rogium.Editors.Campaign;
using Rogium.Editors.Packs;
using Rogium.Gameplay.DataLoading;
using Rogium.Gameplay.AssetRandomGenerator;
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
        private RRG rrg;

        private TilemapLayer[] tilemapLayers;
        private Vector3Int spawnOffset;
        
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
            this.tilemapLayers = tilemaps;
            this.spawnOffset = offset;
            rrg = new RRG(currentCampaign.DataPack.Rooms);
            
            LoadEntranceRoom();
        }

        #region Room Loading

        /// <summary>
        /// Loads the next normal type room.
        /// </summary>
        public void LoadNextNormalRoom()
        {
            LoadNewRoom(rrg.NextNormalRoom());
        }
        
        /// <summary>
        /// Loads the next rare type room.
        /// </summary>
        public void LoadNextRareRoom()
        {
            LoadNewRoom(rrg.NextRareRoom());
        }
        
        /// <summary>
        /// Loads the entrance room, chosen for this campaign.
        /// </summary>
        public void LoadEntranceRoom()
        {
            LoadNewRoom(rrg.ChosenEntranceRoom());
        }
        
        /// <summary>
        /// Loads the shop room, chosen for this campaign.
        /// </summary>
        public void LoadShopRoom()
        {
            LoadNewRoom(rrg.ChosenShopRoom());
        }
        
        /// <summary>
        /// Loads a room from the DataPack.
        /// </summary>
        /// <param name="index">The index of the room on the list.</param>
        private void LoadNewRoom(int index)
        {
            roomLoader.Load(tilemapLayers, spawnOffset, currentCampaign.DataPack.Rooms[index], currentCampaign.DataPack);
        }

        #endregion
    
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