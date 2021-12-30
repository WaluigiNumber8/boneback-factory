using Rogium.Editors.Campaign;
using System;
using System.Collections.Generic;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// The Serialized form of the Campaign Asset.
    /// </summary>
    [System.Serializable]
    public class SerializedCampaignAsset : SerializedAssetBase
    {
        public readonly SerializedPackAsset dataPack;
        public readonly IList<string> packReferences;

        public SerializedCampaignAsset(CampaignAsset asset) : base(asset)
        {
            this.dataPack = new SerializedPackAsset(asset.DataPack);
            this.packReferences = new List<string>(asset.PackReferences);
        }
        
        /// <summary>
        /// Deserialize this Campaign.
        /// </summary>
        /// <returns>The deserialized form of the campaign.</returns>
        public CampaignAsset Deserialize()
        {
            return new CampaignAsset(this.id,
                                     this.title,
                                     this.icon.Deserialize(),
                                     this.author,
                                     DateTime.Parse(this.creationDate),
                                     this.dataPack.Deserialize(),
                                     this.packReferences);
        }
        
    }
}