using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Contains everything important to an individual Campaign.
    /// </summary>
    public class CampaignAsset : AssetBase
    {
        private PackAsset dataPack;
        private IList<string> packReferences;

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
            this.packReferences = new List<string>();
        }
        public CampaignAsset(PackAsset pack)
        {
            this.title = EditorDefaults.CampaignTitle;
            this.icon = EditorDefaults.RoomIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.CampaignIdentifier);

            this.dataPack = new PackAsset(pack);
            this.packReferences = new List<string>();
        }
        
        public CampaignAsset(CampaignAsset campaign)
        {
            this.id = campaign.ID;
            this.title = campaign.Title;
            this.icon = campaign.Icon;
            this.author = campaign.Author;
            this.creationDate = campaign.CreationDate;

            this.dataPack = new PackAsset(campaign.DataPack);
            this.packReferences = new List<string>(campaign.packReferences);
        }
        
        public CampaignAsset(string title, Sprite icon, string author, PackAsset pack)
        {
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.CampaignIdentifier);

            this.dataPack = new PackAsset(pack);
            this.packReferences = new List<string>();
        }
        public CampaignAsset(string title, Sprite icon, string author, DateTime creationDate, PackAsset dataPack)
        {
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            GenerateID(EditorAssetIDs.CampaignIdentifier);
            
            this.dataPack = new PackAsset(dataPack);
            this.packReferences = new List<string>();
        }
        public CampaignAsset(string id, string title, Sprite icon, string author, DateTime creationDate, PackAsset dataPack, IList<string> packReferences)
        {
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.dataPack = new PackAsset(dataPack);
            this.packReferences = new List<string>(packReferences);
        }
        #endregion

        #region Update Values
        public void UpdateDataPack(PackAsset newPack)
        {
            SafetyNet.EnsureIsNotNull(newPack, "newPack");
            this.dataPack = new PackAsset(newPack);
        }

        public void UpdatePackReferences(IList<string> newPackReferences)
        {
            SafetyNet.EnsureIsNotNull(newPackReferences, nameof(newPackReferences));
            this.packReferences = new List<string>(newPackReferences);
        }
        #endregion

        public PackAsset DataPack { get => new PackAsset(dataPack); }
        public IList<string> PackReferences { get => new List<string>(packReferences); }
    }
}