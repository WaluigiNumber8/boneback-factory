using System;
using Rogium.Editors.Packs;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PackInfoAsset"/>.
    /// </summary>
    [System.Serializable]
    public class JSONPackInfoAsset : JSONAssetBase<PackInfoAsset>
    {
        public string description;

        public JSONPackInfoAsset(PackInfoAsset asset) : base(asset)
        {
            description = asset.Description;
        }

        public override PackInfoAsset Decode()
        {
            return new PackInfoAsset(id,
                                     title,
                                     icon.Decode(),
                                     author,
                                     description,
                                     DateTime.Parse(creationDate));
        }

    }
}