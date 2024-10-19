using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using UnityEngine;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Overseers the work on a given campaign.
    /// </summary>
    public sealed class CampaignEditorOverseer : Singleton<CampaignEditorOverseer>, IEditorOverseer
    {
        public event Action<CampaignAsset> OnAssignAsset; 
        public event Action<CampaignAsset, int, string, string> OnSaveChanges;

        private readonly PackCombiner packCombiner;
        
        private CampaignAsset currentCampaign;
        private int myIndex;
        private string originalTitle; 
        private string originalAuthor;
        private CampaignEditorOverseer() => packCombiner = new PackCombiner();

        /// <summary>
        /// Feeds the Campaign Editor data to edit.
        /// </summary>
        /// <param name="campaign">The campaign to edit.</param>
        /// <param name="index">The index of the campaign in the library list.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void AssignAsset(CampaignAsset campaign, int index, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(campaign, "Campaign to assign.");
            currentCampaign = new CampaignAsset.Builder().AsCopy(campaign).Build();
            myIndex = index;
            originalTitle = campaign.Title;
            originalAuthor = campaign.Author;

            if (!prepareEditor) return;
            OnAssignAsset?.Invoke(currentCampaign);
        }

        /// <summary>
        /// Updates the campaign asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset Containing new data.</param>
        public void UpdateAsset(CampaignAsset updatedAsset)
        { 
            SafetyNet.EnsureIsNotNull(currentCampaign, "Currently active asset.");
            currentCampaign = new CampaignAsset.Builder().AsCopy(updatedAsset).Build();
        }

        /// <summary>
        /// Updates current campaign's Data Pack with data from a list of packs.
        /// </summary>
        /// <param name="data">The list of packs to combine.</param>
        public void UpdateDataPack(ISet<PackAsset> data)
        {
            PackAsset ultimatePack = packCombiner.Combine(data);
            
            currentCampaign.UpdateDataPack(ultimatePack);
            currentCampaign.UpdatePackReferences(data.Select(p => p.ID).ToHashSet());
        }
        
        public void CompleteEditing()
        {
            currentCampaign.UpdateIcon(GetIconDirty(currentCampaign.DataPack));
            SaveChanges();
            currentCampaign = null;
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        private void SaveChanges()
        {
            OnSaveChanges?.Invoke(CurrentAsset, myIndex, originalTitle, originalAuthor);
        }

        /// <summary>
        /// Returns the icon of the first entrance room.
        /// </summary>
        /// <param name="dataPack"></param>
        /// <returns></returns>
        private Sprite GetIconDirty(PackAsset dataPack)
        {
            Sprite icon = dataPack.Rooms.FirstOrDefault(rm => rm.Type == RoomType.Entrance)?.Banner;
            return icon;
        }
        
        public CampaignAsset CurrentAsset
        {
            get 
            {
                if (currentCampaign == null) throw new MissingReferenceException("Current Campaign has not been set. Did you forget to activate the editor?");
                return this.currentCampaign;
            }
        }
    }
}