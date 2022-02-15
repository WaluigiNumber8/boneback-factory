using Rogium.Editors.Campaign;
using System;
using System.Collections.Generic;
using Rogium.Editors.Packs;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// The Serialized form of the Campaign Asset.
    /// </summary>
    [System.Serializable]
    public class SerializedCampaignAsset : SerializedAssetBase<CampaignAsset>
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
        public override CampaignAsset Deserialize()
        {
            return new CampaignAsset(id,
                                     title,
                                     icon.Deserialize(),
                                     author,
                                     DateTime.Parse(creationDate),
                                     dataPack.Deserialize(),
                                     packReferences);
        }
        
    }
}