using System;
using Rogium.Editors.Packs;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the PackInfoAsset, ready for saving.
    /// </summary>
    [System.Serializable]
    public class SerializedPackInfoAsset : SerializedAssetBase
    {
        public readonly string description;

        public SerializedPackInfoAsset(PackInfoAsset asset) : base(asset)
        {
            this.description = asset.Description;
        }

        public PackInfoAsset Deserialize()
        {
            return new PackInfoAsset(this.id,
                                     this.title,
                                     this.icon.Deserialize(),
                                     this.author,
                                     this.description,
                                     DateTime.Parse(this.creationDate));
        }

    }
}