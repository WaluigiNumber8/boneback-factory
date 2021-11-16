using System;
using System.Collections.Generic;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using UnityEngine;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Contains everything important to an individual Campaign.
    /// </summary>
    public class CampaignAsset : AssetBase
    {
        private PackAsset dataPack;
        private IList<string> referencedIDs;

        //TODO Read the icon from the icon of the "Beginning Room" from the campaign.

        #region Constructors
        public CampaignAsset()
        {
            this.title = EditorDefaults.CampaignTitle;
            this.icon = EditorDefaults.RoomIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.CampaignIdentifier);

            this.dataPack = new PackAsset();
            this.referencedIDs = new List<string>();
        }
        public CampaignAsset(PackAsset pack)
        {
            this.title = EditorDefaults.CampaignTitle;
            this.icon = EditorDefaults.RoomIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.CampaignIdentifier);

            this.dataPack = new PackAsset(pack);
            this.referencedIDs = new List<string>();
        }
        
        public CampaignAsset(CampaignAsset campaign)
        {
            this.id = campaign.ID;
            this.title = campaign.Title;
            this.icon = campaign.Icon;
            this.author = campaign.Author;
            this.creationDate = campaign.CreationDate;

            this.dataPack = new PackAsset(campaign.DataPack);
            this.referencedIDs = new List<string>(campaign.referencedIDs);
        }
        
        public CampaignAsset(string title, Sprite icon, string author, PackAsset pack)
        {
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.CampaignIdentifier);

            this.dataPack = new PackAsset(pack);
            this.referencedIDs = new List<string>();
        }
        public CampaignAsset(string title, Sprite icon, string author, DateTime creationDate, PackAsset dataPack)
        {
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            GenerateID(EditorAssetIDs.CampaignIdentifier);
            
            this.dataPack = new PackAsset(dataPack);
        }
        public CampaignAsset(string id, string title, Sprite icon, string author, DateTime creationDate, PackAsset dataPack, IList<string> referencedIDs)
        {
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.dataPack = new PackAsset(dataPack);
            this.referencedIDs = new List<string>(referencedIDs);
        }
        #endregion

        #region Update Values
        public void UpdateDataPack(PackAsset newPack)
        {
            SafetyNet.EnsureIsNotNull(newPack, "newPack");
            this.dataPack = new PackAsset(newPack);
        }

        public void UpdateReferencedIDs(IList<string> newReferencedIDs)
        {
            SafetyNet.EnsureIsNotNull(newReferencedIDs, nameof(newReferencedIDs));
            this.referencedIDs = new List<string>(newReferencedIDs);
        }
        #endregion

        public PackAsset DataPack { get => new PackAsset(dataPack); }
        public IList<string> ReferencedIDs { get => new List<string>(referencedIDs); }
    }
}