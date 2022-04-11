using System.Collections.Generic;
using BoubakProductions.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Campaign;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// Overseers asset selection for Campaigns.
    /// </summary>
    public class CampaignAssetSelectionOverseer
    {
        private readonly ExternalLibraryOverseer lib;
        
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

        private CampaignAssetSelectionOverseer()
        {
            lib = ExternalLibraryOverseer.Instance;
        }

        /// <summary>
        /// Initializes the overseer.
        /// <param name="wallpaper">The Wallpaper Controller, that holds information about the UI.</param>
        /// </summary>
        public void Initialize(AssetWallpaperController wallpaper)
        {
            SafetyNet.EnsureIsNotNull(wallpaper, "Wallpaper to Initialize");
            this.wallpaper = wallpaper;
        }

        private void ReloadList()
        {
            campaigns = new List<CampaignAsset>(lib.GetCampaignsCopy);
        }
        
        /// <summary>
        /// Loads the campaign on the next index.
        /// </summary>
        public void SelectCampaignNext()
        {
            ReloadList();
            
            currentIndex++;
            SelectCampaign();
        }

        /// <summary>
        /// Loads the campaign on the previous index.
        /// </summary>
        public void SelectCampaignPrevious()
        {
            ReloadList();
            
            currentIndex--;
            SelectCampaign();
        }

        /// <summary>
        /// Loads the first campaign on the list.
        /// </summary>
        public void SelectCampaignFirst()
        {
            ReloadList();

            currentIndex = 0;
            SelectCampaign();
        }
        
        /// <summary>
        /// Loads the last campaign on the list.
        /// </summary>
        public void SelectCampaignLast()
        {
            ReloadList();
            
            currentIndex = campaigns.Count-1;
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
        /// Reselects the same campaign, that is already loaded.
        /// </summary>
        public void SelectAgain()
        {
            ReloadList();
            SelectCampaign();
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
            if (currentIndex > (campaigns.Count - 1)) currentIndex = 0;
            else if (currentIndex < 0) currentIndex = campaigns.Count-1;
            
            wallpaper.Construct(AssetType.Campaign, currentIndex, campaigns[currentIndex]);
        }

    }
}