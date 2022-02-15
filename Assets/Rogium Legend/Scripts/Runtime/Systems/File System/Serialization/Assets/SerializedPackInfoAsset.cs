using System;
using Rogium.Editors.Packs;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the PackInfoAsset, ready for saving.
    /// </summary>
    [System.Serializable]
    public class SerializedPackInfoAsset : SerializedAssetBase<PackInfoAsset>
    {
        public readonly string description;

        public SerializedPackInfoAsset(PackInfoAsset asset) : base(asset)
        {
            this.description = asset.Description;
        }

        public override PackInfoAsset Deserialize()
        {
            return new PackInfoAsset(id,
                                     title,
                                     icon.Deserialize(),
                                     author,
                                     description,
                                     DateTime.Parse(creationDate));
        }

    }
}