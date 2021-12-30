using BoubakProductions.Systems.Serialization;
using Rogium.Editors.Tiles;
using System;

namespace Rogium.ExternalStorage.Serialization
{
    [System.Serializable]
    public class SerializedTileAsset : SerializedAssetBase
    {
        public readonly int tileType;
        public readonly SerializedColor color;

        public SerializedTileAsset(TileAsset asset) : base(asset)
        {
            this.tileType = (int)asset.Type;
            this.icon = new SerializedSprite(asset.Tile.sprite);
            this.color = new SerializedColor(asset.Tile.color);
        }

        /// <summary>
        /// Turns the Tile Asset into a Unity readable format.
        /// </summary>
        /// <returns></returns>
        public TileAsset Deserialize()
        {
            return new TileAsset(this.id,
                                 this.title,
                                 this.icon.Deserialize(),
                                 this.author,
                                 (TileType)this.tileType,
                                 this.color.Deserialize(),
                                 DateTime.Parse(this.creationDate));
        }
    }
}