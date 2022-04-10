using BoubakProductions.Safety;
using Rogium.Editors.Campaign;
using Rogium.Editors.Packs;
using Rogium.ExternalStorage;
using Rogium.Systems.SceneTransferService;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Overseers the in-game saveable assets library and controls their content.
    /// This library is also synced with asset data files located on the external hard drive.
    /// </summary>
    public class ExternalLibraryOverseer
    {
        private readonly PackList packs = new PackList();
        private readonly CampaignList campaigns = new CampaignList();

        #region Singleton Pattern
        private static ExternalLibraryOverseer instance;
        private static readonly object padlock = new object();
        public static ExternalLibraryOverseer Instance 
        { 
            get
            {
                // lock (padlock)
                {
                    if (instance == null)
                        instance = new ExternalLibraryOverseer();
                    return instance;
                }
            }
        }
        #endregion

        private ExternalLibraryOverseer() 
        {
            PackEditorOverseer.Instance.OnSaveChanges += UpdatePack;
            CampaignEditorOverseer.Instance.OnSaveChanges += UpdateCampaign;
            ReloadFromExternalStorage();
        }

        /// <summary>
        /// Repopulates the library with all packs located on external storage.
        /// </summary>
        public void ReloadFromExternalStorage()
        {
            packs.ReplaceAll(ExternalStorageOverseer.Instance.LoadAllPacks());
            campaigns.ReplaceAll(ExternalStorageOverseer.Instance.Campaigns.LoadAll());
        }

        #region Packs
        /// <summary>
        /// Creates a new Pack, and adds it to the library.
        /// </summary>
        /// <param name="packInfo">Information about the pack.</param>
        public void CreateAndAddPack(PackInfoAsset packInfo)
        {
            PackAsset newPack = new PackAsset(packInfo);
            packs.Add(newPack);
        }

        /// <summary>
        /// Updates the pack on a specific position in the library
        /// </summary>
        /// <param name="pack">The new data for the pack.</param>
        /// <param name="index">Position to override.</param>
        /// <param name="originalTitle">Pack's Title before updating.</param>
        /// <param name="originalAuthor">Pack's Author before updating.</param>
        public void UpdatePack(PackAsset pack, int index, string originalTitle, string originalAuthor)
        {
            packs.Update(index, pack);
        }
        
        /// <summary>
        /// Remove pack from the library.
        /// </summary>
        /// <param name="packIndex">Pack ID in the list.</param>
        public void DeletePack(int packIndex)
        {
            packs.Remove(packIndex);
        }
        /// <summary>
        /// Remove pack from the library.
        /// </summary>
        /// <param name="title">Pack's Title</param>
        /// <param name="author">Pack's Author</param>
        public void DeletePack(string title, string author)
        {
            packs.Remove(title, author);
        }

        /// <summary>
        /// Prepare one of the packs in the library for editing.
        /// </summary>
        public void ActivatePackEditor(int packIndex)
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(packs, "Pack Library");
            SafetyNet.EnsureIntIsInRange(packIndex, 0, packs.Count, "packIndex for activating Pack Editor");
            packs[packIndex] = ExternalStorageOverseer.Instance.LoadPack(packs[packIndex]);
            PackEditorOverseer.Instance.AssignAsset(packs[packIndex], packIndex);
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
            CampaignAsset newCampaign = new CampaignAsset(title, icon, author, new PackAsset(dataPack));
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
        /// <param name="index">Position to override.</param>
        /// <param name="originalTitle">Campaign's title before updating.</param>
        /// <param name="originalAuthor">Campaign's author before updating.</param>
        public void UpdateCampaign(CampaignAsset campaign, int index, string originalTitle, string originalAuthor)
        {
            campaigns.Update(index, campaign);
        }
        
        /// <summary>
        /// Remove campaign from the library.
        /// </summary>
        /// <param name="campaignIndex">Campaign ID in the list.</param>
        public void DeleteCampaign(int campaignIndex)
        {
            campaigns.Remove(campaignIndex);
        }
        /// <summary>
        /// Remove Campaign from the library.
        /// </summary>
        /// <param name="title">Campaign's Title</param>
        /// <param name="author">Campaign's Author</param>
        public void DeleteCampaign(string title, string author)
        {
            campaigns.Remove(title, author);
        }
        
        /// <summary>
        /// Prepare one of the campaigns in the library for editing.
        /// </summary>
        public void ActivateCampaignEditor(int campaignIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(campaigns, "Campaign Library");
            SafetyNet.EnsureIntIsInRange(campaignIndex, 0, campaigns.Count, "campaignIndex for activating Campaign Editor");
            CampaignEditorOverseer.Instance.AssignAsset(campaigns[campaignIndex], campaignIndex, prepareEditor);
        }

        /// <summary>
        /// Prepares one of the campaign for transfer to gameplay.
        /// </summary>
        /// <param name="campaignIndex"></param>
        public void ActivateCampaignPlaythrough(int campaignIndex)
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(campaigns, "Campaign Library");
            SafetyNet.EnsureIntIsInRange(campaignIndex, 0, campaigns.Count, "campaignIndex for activating Campaign Playthrough");
            SceneTransferOverseer.GetInstance().LoadUp(campaigns[campaignIndex]);
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
        public PackList GetPacksCopy => new(packs);
        
        /// <summary>
        /// Amount of campaigns stored in the library.
        /// </summary>
        public int CampaignCount => campaigns.Count;

        /// <summary>
        /// Returns a copy of the list of campaigns stored here.
        /// </summary>
        /// <returns>A copy of Pack Library.</returns>
        public CampaignList GetCampaignsCopy => new(campaigns);
    }
}