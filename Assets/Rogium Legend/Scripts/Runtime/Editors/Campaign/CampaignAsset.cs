using System;
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

        //TODO Read the icon from the icon of the "Beginning Room" from the campaign.

        #region Constructors
        public CampaignAsset(PackAsset dataPack)
        {
            this.title = EditorDefaults.CampaignTitle;
            this.icon = EditorDefaults.RoomIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.CampaignIdentifier);
            
            this.dataPack = new PackAsset(dataPack);
        }
        
        public CampaignAsset(CampaignAsset campaign)
        {
            this.id = campaign.ID;
            this.title = campaign.Title;
            this.icon = campaign.Icon;
            this.author = campaign.Author;
            this.creationDate = campaign.CreationDate;
            
            this.dataPack = new PackAsset(campaign.DataPack);
        }
        
        public CampaignAsset(string title, Sprite icon, string author, PackAsset dataPack)
        {
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.CampaignIdentifier);

            this.dataPack = dataPack;
        }
        public CampaignAsset(string title, Sprite icon, string author, DateTime creationDate, PackAsset dataPack)
        {
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            GenerateID(EditorAssetIDs.CampaignIdentifier);
            
            this.dataPack = dataPack;
        }
        public CampaignAsset(string id, string title, Sprite icon, string author, DateTime creationDate, PackAsset dataPack)
        {
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.dataPack = dataPack;
        }
        #endregion

        #region Update Values
        public void UpdateDataPack(PackAsset newDataPack)
        {
            this.dataPack = new PackAsset(newDataPack);
        }
        #endregion

        public PackAsset DataPack { get => dataPack; }
    }
}