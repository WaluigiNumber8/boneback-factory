using System;
using System.Collections.Generic;
using BoubakProductions.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using UnityEngine;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Overseers the work on a given campaign.
    /// </summary>
    public class CampaignEditorOverseer : IEditorOverseer
    {
        public event Action<CampaignAsset> OnAssignAsset; 
        public event Action<CampaignAsset, int, string, string> OnSaveChanges;

        private PackCombiner packCombiner;
        
        private CampaignAsset currentCampaign;
        private int myIndex;
        private string originalTitle; 
        private string originalAuthor;

        #region Singleton Pattern
        private static CampaignEditorOverseer instance;
        private static readonly object padlock = new object();

        public static CampaignEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new CampaignEditorOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private CampaignEditorOverseer()
        {
            packCombiner = new PackCombiner();
        }
        
        /// <summary>
        /// Feeds the Campaign Editor data to edit.
        /// </summary>
        /// <param name="campaign">The campaign to edit.</param>
        /// <param name="index">The index of the campaign in the library list.</param>
        public void AssignAsset(CampaignAsset campaign, int index)
        {
            SafetyNet.EnsureIsNotNull(campaign, "Campaign to assign.");
            currentCampaign = new CampaignAsset(campaign);
            myIndex = index;
            originalTitle = campaign.Title;
            originalAuthor = campaign.Author;
            OnAssignAsset?.Invoke(currentCampaign);
        }

        /// <summary>
        /// Updates the campaign asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(CampaignAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentCampaign, "Currently active asset.");
            currentCampaign = new CampaignAsset(updatedAsset);
        }

        /// <summary>
        /// Updates current campaign's Data Pack with data from a list of packs.
        /// </summary>
        /// <param name="data">The list of packs to combine.</param>
        public void UpdateDataPack(IList<PackAsset> data)
        {
            PackAsset ultimatePack = packCombiner.Combine(data);
            
            currentCampaign.UpdateDataPack(ultimatePack);
            currentCampaign.UpdatePackReferences(data.ConvertToIDs());
        }
        
        public void CompleteEditing()
        {
            SaveChanges();
            currentCampaign = null;
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        private void SaveChanges()
        {
            OnSaveChanges?.Invoke(CurrentCampaign, myIndex, originalTitle, originalAuthor);
        }
        
        public CampaignAsset CurrentCampaign
        {
            get 
            {
                if (currentCampaign == null) throw new MissingReferenceException("Current Campaign has not been set. Did you forget to activate the editor?");
                return this.currentCampaign;
            }
        }
    }
}