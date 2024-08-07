using Rogium.Editors.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="SpriteAsset"/>.
    /// </summary>
    [Serializable]
    public class JSONSpriteAsset : JSONAssetBase<SpriteAsset>
    {
        public JSONGrid<int> spriteData;
        public string preferredPaletteID;
        public string[] associatedAssetIDs;

        public JSONSpriteAsset(SpriteAsset asset) : base(asset)
        {
            spriteData = new JSONGrid<int>(asset.SpriteData);
            preferredPaletteID = asset.PreferredPaletteID;
            associatedAssetIDs = asset.AssociatedAssetsIDs.ToArray();
        }

        public override SpriteAsset Decode()
        {
            spriteData.SetDefaultCreator(() => -1);
            return new SpriteAsset.Builder()
                .WithID(id)
                .WithTitle(title)
                .WithIcon(icon.Decode())
                .WithAuthor(author)
                .WithCreationDate(DateTime.Parse(creationDate))
                .WithSpriteData(spriteData.Decode())
                .WithPreferredPaletteID(preferredPaletteID)
                .WithAssociatedAssetIDs((associatedAssetIDs == null) ? new HashSet<string>() : associatedAssetIDs.ToHashSet())
                .Build();
        }
    }
}