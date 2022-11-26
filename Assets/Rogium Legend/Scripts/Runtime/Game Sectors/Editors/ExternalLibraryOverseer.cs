using System.Collections.Generic;
using RedRats.Safety;
using Rogium.Editors.Campaign;
using Rogium.Editors.Packs;
using Rogium.ExternalStorage;
using Rogium.Systems.SceneTransferService;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Overseers the main in-game saveable assets library and controls their content.
    /// This library is also synced with asset data files located on the external hard drive.
    /// </summary>
    public class ExternalLibraryOverseer
    {
        private readonly ExternalStorageOverseer ex;
        
        private readonly AssetDictionary<PackAsset> packs;
        private readonly AssetDictionary<CampaignAsset> campaigns;

        #region Singleton Pattern
        static ExternalLibraryOverseer() { }
        public static ExternalLibraryOverseer Instance { get; } = new();
        #endregion

        private ExternalLibraryOverseer()
        {
            ex = ExternalStorageOverseer.Instance;
            packs = new AssetDictionary<PackAsset>(ex.CreatePack, ex.UpdatePack, ex.DeletePack);
            campaigns = new AssetDictionary<CampaignAsset>(ex.Campaigns.Save, ex.Campaigns.UpdateTitle, ex.Campaigns.Delete);
            
            PackEditorOverseer.Instance.OnSaveChanges += UpdatePack;
            CampaignEditorOverseer.Instance.OnSaveChanges += UpdateCampaign;
            ReloadFromExternalStorage();
        }

        /// <summary>
        /// Repopulates the library with all packs located on external storage.
        /// </summary>
        public void ReloadFromExternalStorage()
        {
            packs.ReplaceAll(ex.LoadAllPacks());
            campaigns.ReplaceAll(ex.Campaigns.LoadAll());
        }

        #region Packs
        /// <summary>
        /// Creates a new Pack, and adds it to the library.
        /// </summary>
        /// <param name="packInfo">Information about the pack.</param>
        public void CreateAndAddPack(PackInfoAsset packInfo)
        {
            PackAsset newPack = new(packInfo);
            packs.Add(newPack);
        }

        /// <summary>
        /// Updates the pack on a specific position in the library
        /// </summary>
        /// <param name="pack">The new data for the pack.</param>
        /// <param name="index">Position to override.</param>
        /// <param name="originalTitle">Pack's Title before updating.</param>
        /// <param name="originalAuthor">Pack's Author before updating.</param>
        public void UpdatePack(PackAsset pack, string originalTitle, string originalAuthor)
        {
            packs.Update(pack);
        }
        
        /// <summary>
        /// Remove pack from the library.
        /// </summary>
        /// <param name="assetID">Pack ID in the dictionary.</param>
        public void DeletePack(string assetID)
        {
            packs.Remove(assetID);
        }

        /// <summary>
        /// Prepare one of the packs in the library for editing.
        /// </summary>
        public void ActivatePackEditor(string packID)
        {
            SafetyNet.EnsureDictionaryIsNotNullOrEmpty(packs, "Pack Library");
            packs[packID] = ex.LoadPack(packs[packID]);
            PackEditorOverseer.Instance.AssignAsset(packs[packID]);
        }
        #endregion

        #region Campaigns

        /// <summary>
        /// Creates a new Campaign, and adds it to the library.
        /// </summary>
        /// <param name="title">Title of the campaign.</param>
        /// <param name="icon">Icon of the campaign.</param>
        /// <param name="author">Author, who made the campaign.</param>
        /// <param name="dataPack">Pack Asset, containing everything used in the campaign.</param>
        public void CreateAndAddCampaign(string title, Sprite icon, string author, PackAsset dataPack)
        {
            CampaignAsset newCampaign = new(title, icon, author, new PackAsset(dataPack));
            campaigns.Add(newCampaign);
        }
        public void CreateAndAddCampaign(CampaignAsset campaign)
        {
            campaigns.Add(new CampaignAsset(campaign));
        }

        /// <summary>
        /// Updates the campaign on a specific position in the library.
        /// </summary>
        /// <param name="campaign">The new data for the campaign.</param>
        /// <param name="originalTitle">Campaign's title before updating.</param>
        /// <param name="originalAuthor">Campaign's author before updating.</param>
        public void UpdateCampaign(CampaignAsset campaign, string originalTitle, string originalAuthor)
        {
            campaigns.Update(campaign);
        }
        
        /// <summary>
        /// Remove campaign from the library.
        /// </summary>
        /// <param name="campaignID">Campaign ID in the collection.</param>
        public void DeleteCampaign(string campaignID)
        {
            campaigns.Remove(campaignID);
        }
        
        /// <summary>
        /// Prepare one of the campaigns in the library for editing.
        /// </summary>
        public void ActivateCampaignEditor(string campaignID, bool prepareEditor = true)
        {
            SafetyNet.EnsureDictionaryIsNotNullOrEmpty(campaigns, "Campaign Library");
            CampaignEditorOverseer.Instance.AssignAsset(campaigns[campaignID], prepareEditor);
        }

        /// <summary>
        /// Prepares one of the campaign for transfer to gameplay.
        /// </summary>
        /// <param name="campaignID"></param>
        public void ActivateCampaignPlaythrough(string campaignID)
        {
            SafetyNet.EnsureDictionaryIsNotNullOrEmpty(campaigns, "Campaign Library");
            SceneTransferOverseer.GetInstance().LoadUp(campaigns[campaignID]);
        }
        #endregion
        
        /// <summary>
        /// Amount of packs, stored in the library.
        /// </summary>
        public int PackCount => packs.Count;

        /// <summary>
        /// Returns a copy of the list of packs stored here.
        /// </summary>
        /// <returns>A copy of Pack Library.</returns>
        public IDictionary<string, PackAsset> GetPacksCopy => new Dictionary<string, PackAsset>(packs);
        
        /// <summary>
        /// Amount of campaigns stored in the library.
        /// </summary>
        public int CampaignCount => campaigns.Count;

        /// <summary>
        /// Returns a copy of the list of campaigns stored here.
        /// </summary>
        /// <returns>A copy of Pack Library.</returns>
        public IDictionary<string, CampaignAsset> GetCampaignsCopy => new Dictionary<string, CampaignAsset>(campaigns);
    }
}