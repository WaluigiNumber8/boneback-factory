using System;
using Rogium.Editors.Packs;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="PackAsset"/>.
    /// </summary>
    [Serializable]
    public class JSONPackAsset : JSONAssetWithReferencedSpriteBase<PackAsset>
    {
        public string description;

        public JSONPackAsset(PackAsset asset) : base(asset)
        {
            description = asset.Description;
        }

        public override PackAsset Decode()
        {
            return new PackAsset.Builder()
                .WithID(id)
                .WithTitle(title)
                .WithIcon(icon.Decode())
                .WithAuthor(author)
                .WithCreationDate(DateTime.Parse(creationDate))
                .WithAssociatedSpriteID(associatedSpriteID)
                .WithDescription(description)
                .Build();
        }
        
    }
}