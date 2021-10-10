using System;
using Rogium.Editors.Campaign;

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

        public SerializedCampaignAsset(CampaignAsset asset)
        {
            this.ID = asset.ID;
            this.title = asset.Title;
            this.icon = new SerializedSprite(asset.Icon);
            this.author = asset.Author;
            this.creationDate = asset.CreationDate.ToString();

            this.dataPack = new SerializedPackAsset(asset.DataPack);
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
                                     dataPack.Deserialize());
        }
        
    }
}