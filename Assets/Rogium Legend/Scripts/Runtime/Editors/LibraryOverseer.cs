using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Campaign;
using Rogium.ExternalStorage;
using Rogium.Global.SceneTransferService;
using UnityEngine;

namespace Rogium.Editors.Packs
{
    /// <summary>
    /// Overseers the in-game saveable assets library and controls their content.
    /// This library is also synced with asset data files located on the external hard drive.
    /// </summary>
    public class LibraryOverseer
    {
        private readonly PackList packs = new PackList();
        private readonly CampaignList campaigns = new CampaignList();

        #region Singleton Pattern
        private static LibraryOverseer instance;
        private static readonly object padlock = new object();
        public static LibraryOverseer Instance 
        { 
            get
            {
                // lock (padlock)
                {
                    if (instance == null)
                        instance = new LibraryOverseer();
                    return instance;
                }
            }
        }
        #endregion

        private LibraryOverseer() 
        {
            PackEditorOverseer.Instance.OnSaveChanges += UpdatePack;
            ReloadFromExternalStorage();
        }

        /// <summary>
        /// Repopulates the library with all packs located on external storage.
        /// </summary>
        public void ReloadFromExternalStorage()
        {
            packs.ReplaceAll(ExternalStorageOverseer.Instance.LoadAllPacks());
            campaigns.ReplaceAll(ExternalStorageOverseer.Instance.LoadAllCampaigns());
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
        public void UpdatePack(PackAsset pack, int index, string lastTitle, string lastAuthor)
        {
            if (packs.TryFinding(lastTitle, lastAuthor) != null)
                ExternalStorageOverseer.Instance.DeletePack(lastTitle);
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
            SafetyNet.EnsureListIsNotEmptyOrNull(packs, "Pack Library");
            SafetyNet.EnsureIntIsInRange(packIndex, 0, packs.Count, "packIndex for activating Pack Editor");
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
            CampaignAsset newCampaign = new CampaignAsset(title, icon, author, dataPack);
            campaigns.Add(newCampaign);
        }
        public void CreateAndAddCampaign(CampaignAsset campaign)
        {
            CampaignAsset newCampaign = new CampaignAsset(campaign);
            campaigns.Add(newCampaign);
        }

        /// <summary>
        /// Updates the campaign on a specific position in the library.
        /// </summary>
        /// <param name="campaign">The new data for the campaign.</param>
        /// <param name="index">Position to override.</param>
        public void UpdateCampaign(CampaignAsset campaign, int index)
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
        /// Tries to remove a pack from the library. If it doesn't find one, nothing will happen.
        /// </summary>
        /// <param name="title">The Title of the pack.</param>
        /// <param name="author">The author of the pack.</param>
        public void TryDeletePack(string title, string author)
        {
            if (packs.TryFinding(title, author) != null)
                DeletePack(title, author);
        }

        /// <summary>
        /// Prepare one of the campaigns in the library for editing.
        /// </summary>
        public void ActivateCampaignEditor(int campaignIndex)
        {
            SafetyNet.EnsureListIsNotEmptyOrNull(campaigns, "Campaign Library");
            SafetyNet.EnsureIntIsInRange(campaignIndex, 0, campaigns.Count, "campaignIndex for activating Campaign Editor");
            CampaignOverseer.Instance.AssignAsset(campaigns[campaignIndex]);
        }

        /// <summary>
        /// Prepares one of the campaign for transfer to gameplay.
        /// </summary>
        /// <param name="campaignIndex"></param>
        public void ActivateCampaignPlaythrough(int campaignIndex)
        {
            SafetyNet.EnsureListIsNotEmptyOrNull(campaigns, "Campaign Library");
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
        public PackList GetPacksCopy => new PackList(packs);
        
        /// <summary>
        /// Amount of campaigns stored in the library.
        /// </summary>
        public int CampaignCount => campaigns.Count;

        /// <summary>
        /// Returns a copy of the list of campaigns stored here.
        /// </summary>
        /// <returns>A copy of Pack Library.</returns>
        public CampaignList GetCampaignsCopy => new CampaignList(campaigns);
    }
}