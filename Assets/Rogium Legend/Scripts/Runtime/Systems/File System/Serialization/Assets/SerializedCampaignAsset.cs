using BoubakProductions.Systems.Serialization;
using Rogium.Editors.Campaign;
using System;
using System.Collections.Generic;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// The Serialized form of the Campaign Asset.
    /// </summary>
    [System.Serializable]
    public class SerializedCampaignAsset
    {
        public readonly string ID;
        public readonly string title;
        public readonly SerializedSprite icon;
        public readonly string author;
        public readonly string creationDate;

        public readonly SerializedPackAsset dataPack;
        public readonly IList<string> packReferences;

        public SerializedCampaignAsset(CampaignAsset asset)
        {
            this.ID = asset.ID;
            this.title = asset.Title;
            this.icon = new SerializedSprite(asset.Icon);
            this.author = asset.Author;
            this.creationDate = asset.CreationDate.ToString();

            this.dataPack = new SerializedPackAsset(asset.DataPack);
            this.packReferences = new List<string>(asset.PackReferences);
        }

        
        /// <summary>
        /// Deserialize this Campaign.
        /// </summary>
        /// <returns>The deserialized form of the campaign.</returns>
        public CampaignAsset Deserialize()
        {
            return new CampaignAsset(ID,
                                     title,
                                     icon.Deserialize(),
                                     author,
                                     DateTime.Parse(creationDate),
                                     dataPack.Deserialize(),
                                     packReferences);
        }
        
    }
}