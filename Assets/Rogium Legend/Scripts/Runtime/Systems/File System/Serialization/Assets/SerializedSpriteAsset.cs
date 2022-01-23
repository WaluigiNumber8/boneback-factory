using Rogium.Editors.Sprites;
using System;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="SpriteAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedSpriteAsset : SerializedAssetBase
    {
        private SerializedGrid<int> spriteData;
        private string preferredPaletteID;
        
        public SerializedSpriteAsset(SpriteAsset asset) : base(asset)
        {
            this.spriteData = new SerializedGrid<int>(asset.SpriteData);
            this.preferredPaletteID = asset.PreferredPaletteID;
        }

        public SpriteAsset Deserialize()
        {
            return new SpriteAsset(this.id,
                                   this.title,
                                   this.icon.Deserialize(),
                                   this.author,
                                   this.spriteData.Deserialize( () => -1),
                                   this.preferredPaletteID,
                                   DateTime.Parse(this.creationDate));
        }
    }
}