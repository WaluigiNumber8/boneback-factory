using System.Collections.Generic;
using BoubakProductions.Safety;
using Rogium.Core;
using Rogium.Editors.Campaign;

namespace Rogium.Global.UISystem.AssetSelection
{
    /// <summary>
    /// Overseers asset selection for Campaigns.
    /// </summary>
    public class CampaignAssetSelectionOverseer
    {
        private AssetWallpaperController wallpaper;
        private IList<CampaignAsset> campaigns;
        private int currentIndex;
        
        #region Singleton Pattern
        private static CampaignAssetSelectionOverseer instance;
        private static readonly object padlock = new object();

        public static CampaignAssetSelectionOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new CampaignAssetSelectionOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private CampaignAssetSelectionOverseer() {}

        /// <summary>
        /// Initializes the overseer.
        /// <param name="campaigns">The list of campaigns to load into the overseer.</param>
        /// <param name="startingIndex">The starting position on the list.</param>
        /// </summary>
        public void Load(AssetWallpaperController wallpaper, IList<CampaignAsset> campaigns, int startingIndex)
        {
            SafetyNet.EnsureIsNotNull(campaigns, "List of Campaigns to initialize");
            SafetyNet.EnsureIsNotNull(wallpaper, "Wallpaper to Initialize");
            SafetyNet.EnsureIntIsInRange(startingIndex, 0, campaigns.Count, "Starting Index");
            
            this.wallpaper = wallpaper;
            this.campaigns = new List<CampaignAsset>(campaigns);
            this.currentIndex = startingIndex;
            
            SelectCampaign();
        }
        
        /// <summary>
        /// Loads the campaign on the next index.
        /// </summary>
        public void SelectCampaignNext()
        {
            currentIndex++;
            SelectCampaign();
        }

        /// <summary>
        /// Loads the campaign on the previous index.
        /// </summary>
        public void SelectCampaignPrevious()
        {
            currentIndex--;
            SelectCampaign();
        }

        /// <summary>
        /// Loads the last campaign on the list.
        /// </summary>
        public void SelectCampaignLast()
        {
            currentIndex = campaigns.Count;
            SelectCampaign();
        }
        
        /// <summary>
        /// Loads an empty wallpaper.
        /// </summary>
        public void SelectEmpty()
        {
            wallpaper.ConstructEmpty();
        }
        
        /// <summary>
        /// Loads a campaign located on the index into the wallpaper.
        /// </summary>
        private void SelectCampaign()
        {
            if (campaigns.Count == 0)
             SelectEmpty();
            else SelectCampaignWithWrapping();
        }

        /// <summary>
        /// Constructs a campaign under a wrapping <see cref="currentIndex"/>.
        /// </summary>
        private void SelectCampaignWithWrapping()
        {
            //Index Wrapping
            currentIndex = (currentIndex > campaigns.Count) ? 0 : ((currentIndex < 0) ? campaigns.Count : currentIndex);
            wallpaper.Construct(AssetType.Campaign, currentIndex, campaigns[currentIndex]);
        }
        
    }
}