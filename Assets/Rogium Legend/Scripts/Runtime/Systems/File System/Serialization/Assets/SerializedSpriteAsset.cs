using Rogium.Editors.Sprites;
using System;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="SpriteAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedSpriteAsset : SerializedAssetBase<SpriteAsset>
    {
        private SerializedGrid<int> spriteData;
        private string preferredPaletteID;
        
        public SerializedSpriteAsset(SpriteAsset asset) : base(asset)
        {
            spriteData = new SerializedGrid<int>(asset.SpriteData);
            preferredPaletteID = asset.PreferredPaletteID;
        }

        public override SpriteAsset Deserialize()
        {
            return new SpriteAsset(id,
                                   title,
                                   icon.Deserialize(),
                                   author,
                                   spriteData.Deserialize(() => -1),
                                   preferredPaletteID,
                                   DateTime.Parse(creationDate));
        }
    }
}