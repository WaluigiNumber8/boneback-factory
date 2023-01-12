using RedRats.Safety;
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
        private int adventureLength;
        private PackAsset dataPack;
        private IList<string> packReferences;

        #region Constructors
        public CampaignAsset()
        {
            this.title = EditorConstants.CampaignTitle;
            this.icon = EditorConstants.RoomIcon;
            this.author = EditorConstants.Author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.CampaignIdentifier);

            this.adventureLength = EditorConstants.CampaignLength;
            this.dataPack = new PackAsset();
            this.packReferences = new List<string>();
        }
        public CampaignAsset(PackAsset pack)
        {
            this.title = EditorConstants.CampaignTitle;
            this.icon = EditorConstants.RoomIcon;
            this.author = EditorConstants.Author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.CampaignIdentifier);

            this.adventureLength = EditorConstants.CampaignLength;
            this.dataPack = new PackAsset(pack);
            this.packReferences = new List<string>();
        }
        
        public CampaignAsset(CampaignAsset asset)
        {
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = asset.Icon;
            this.author = asset.Author;
            this.creationDate = asset.CreationDate;

            this.adventureLength = asset.AdventureLength;
            this.dataPack = new PackAsset(asset.DataPack);
            this.packReferences = new List<string>(asset.packReferences);
        }
        
        public CampaignAsset(string title, Sprite icon, string author, PackAsset pack)
        {
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            GenerateID(EditorAssetIDs.CampaignIdentifier);

            this.adventureLength = EditorConstants.CampaignLength;
            this.dataPack = new PackAsset(pack);
            this.packReferences = new List<string>();
        }
        public CampaignAsset(string title, Sprite icon, string author, DateTime creationDate, int adventureLength, PackAsset dataPack)
        {
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            GenerateID(EditorAssetIDs.CampaignIdentifier);

            this.adventureLength = adventureLength;
            this.dataPack = new PackAsset(dataPack);
            this.packReferences = new List<string>();
        }
        public CampaignAsset(string id, string title, Sprite icon, string author, DateTime creationDate, int adventureLength,
                             PackAsset dataPack, IList<string> packReferences)
        {
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;

            this.adventureLength = adventureLength;
            this.dataPack = new PackAsset(dataPack);
            this.packReferences = new List<string>(packReferences);
        }
        #endregion

        #region Update Values

        public void UpdateLength(int newLength)
        {
            SafetyNet.EnsureIntIsBiggerThan(newLength, 0, "New Campaign Length");
            adventureLength = newLength;
        }
        public void UpdateDataPack(PackAsset newPack)
        {
            SafetyNet.EnsureIsNotNull(newPack, "newPack");
            dataPack = new PackAsset(newPack);
        }

        public void UpdatePackReferences(IList<string> newPackReferences)
        {
            SafetyNet.EnsureIsNotNull(newPackReferences, nameof(newPackReferences));
            packReferences = new List<string>(newPackReferences);
        }
        #endregion

        public int AdventureLength { get => adventureLength; }
        public PackAsset DataPack { get => new PackAsset(dataPack); }
        public IList<string> PackReferences { get => new List<string>(packReferences); }
    }
}