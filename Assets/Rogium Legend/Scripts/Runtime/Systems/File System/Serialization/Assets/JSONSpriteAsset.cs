using Rogium.Editors.Sprites;
using System;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="SpriteAsset"/>.
    /// </summary>
    [System.Serializable]
    public class JSONSpriteAsset : JSONAssetBase<SpriteAsset>
    {
        public JSONGrid<int> spriteData;
        public string preferredPaletteID;
        
        public JSONSpriteAsset(SpriteAsset asset) : base(asset)
        {
            spriteData = new JSONGrid<int>(asset.SpriteData);
            preferredPaletteID = asset.PreferredPaletteID;
        }

        public override SpriteAsset Decode()
        {
            spriteData.SetDefaultCreator(() => -1);
            return new SpriteAsset(id,
                                   title,
                                   icon.Decode(),
                                   author,
                                   spriteData.Decode(),
                                   preferredPaletteID,
                                   DateTime.Parse(creationDate));
        }
    }
}