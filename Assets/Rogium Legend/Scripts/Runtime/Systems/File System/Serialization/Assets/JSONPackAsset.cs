using System;
using Rogium.Editors.Packs;
using UnityEngine;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PackAsset"/>.
    /// </summary>
    [System.Serializable]
    public class JSONPackAsset : JSONAssetWithReferencedSpriteBase<PackAsset>
    {
        public string description;

        public JSONPackAsset(PackAsset asset) : base(asset)
        {
            description = asset.Description;
        }

        public override PackAsset Decode()
        {
            string titleHere = title;
            Sprite decode = icon.Decode();
            string au = author;
            string spriteID = associatedSpriteID;
            string descr = description;
            DateTime creationDateTime = DateTime.Parse(creationDate);
            return new PackAsset(id,
                titleHere,
                decode,
                au,
                spriteID,
                descr,
                creationDateTime);
        }
        
    }
}