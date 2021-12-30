using BoubakProductions.Systems.Serialization;
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
        private SerializedSprite sprite;
        
        public SerializedSpriteAsset(SpriteAsset asset) : base(asset)
        {
            this.sprite = new SerializedSprite(asset.Sprite);
        }

        public SpriteAsset Deserialize()
        {
            return new SpriteAsset(this.id,
                                   this.title,
                                   this.icon.Deserialize(),
                                   this.author,
                                   this.sprite.Deserialize(),
                                   DateTime.Parse(this.creationDate));
        }
    }
}