using Rogium.Editors.PackData;
using System.Collections.Generic;
using System.IO;
using Rogium.Editors.Campaign;
using UnityEngine;

namespace Rogium.ExternalStorage
{
    /// <summary>
    /// Overseers files stored on external storage.
    /// </summary>
    public class ExternalStorageOverseer
    {
        private SaveableData packData;
        private SaveableData campaignData;

        #region Singleton Pattern
        private static ExternalStorageOverseer instance;
        private static readonly object padlock = new object();
        public static ExternalStorageOverseer Instance 
        { 
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new ExternalStorageOverseer();
                    return instance;
                }
            }
        }
        #endregion

        private ExternalStorageOverseer()
        {
            packData = new SaveableData("Packs", "bumpack");
            campaignData = new SaveableData("Campaigns", "bumcamp");

            //Create directories if they don't exist.
            if (!Directory.Exists(packData.Path))
                Directory.CreateDirectory(packData.Path);
            
            if (!Directory.Exists(campaignData.Path))
                Directory.CreateDirectory(campaignData.Path);
        }

        /// <summary>
        /// Wrapper for saving a pack to external storage.
        /// </summary>
        /// <param name="pack">The pack to save.</param>
        public void Save(PackAsset pack)
        {
            string savePath = Path.Combine(packData.Path, $"{pack.PackInfo.Title}.{packData.Extension}");
            FileSystem.SavePack(savePath, pack);
        }
        /// <summary>
        /// Wrapper for saving a campaign to external storage.
        /// </summary>
        /// <param name="campaign">The campaign to save.</param>
        public void Save(CampaignAsset campaign)
        {
            string savePath = Path.Combine(campaignData.Path, $"{campaign.Title}.{campaignData.Extension}");
            FileSystem.SaveCampaign(savePath, campaign);
        }

        /// <summary>
        /// Wrapper for loading all packs from external storage.
        /// </summary>
        /// <returns></returns>
        public IList<PackAsset> LoadAllPacks()
        {
            return FileSystem.LoadAllPacks(packData.Path);
        }
        /// <summary>
        /// Wrapper for loading all campaigns from external storage.
        /// </summary>
        /// <returns></returns>
        public IList<CampaignAsset> LoadAllCampaigns()
        {
            return FileSystem.LoadAllCampaigns(campaignData.Path);
        }

        /// <summary>
        /// Wrapper for removing a pack from external storage.
        /// <param name="pack">The pack to remove.</param>
        /// </summary>
        public void DeletePack(PackAsset pack)
        {
            DeletePack(pack.Title);
        }
        /// <summary>
        /// Wrapper for removing a pack from external storage.
        /// </summary>
        /// <param name="packTitle">The name of the pack to remove.</param>
        public void DeletePack(string packTitle)
        {
            string removePath = Path.Combine(packData.Path, $"{packTitle}.{packData.Extension}");
            FileSystem.Delete(removePath);
        }
        
        /// <summary>
        /// Wrapper for removing a campaign from external storage.
        /// <param name="campaign">The pack to remove.</param>
        /// </summary>
        public void DeleteCampaign(CampaignAsset campaign)
        {
            DeleteCampaign(campaign.Title);
        }

        /// <summary>
        /// Wrapper for removing a campaign from external storage.
        /// </summary>
        /// <param name="campaignTitle">The name of the campaign to remove.</param>
        public void DeleteCampaign(string campaignTitle)
        {
            string removePath = Path.Combine(campaignData.Path, $"{campaignTitle}.{campaignData.Extension}");
            FileSystem.Delete(removePath);
        }

        public string PackPath {get => packData.Path;}
        public string CampaignPath {get => campaignData.Path;}
        public string PackExtension {get => packData.Extension;}
        public string CampaignExtension {get => campaignData.Extension;}
    }
}