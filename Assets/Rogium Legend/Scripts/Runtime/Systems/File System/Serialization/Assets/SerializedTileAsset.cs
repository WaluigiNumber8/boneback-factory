using RedRats.Systems.FileSystem.Serialization;
using Rogium.Editors.Tiles;
using System;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="TileAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedTileAsset : SerializedAssetBase<TileAsset>
    {
        public readonly int tileType;
        public readonly SerializedColor color;

        public SerializedTileAsset(TileAsset asset) : base(asset)
        {
            tileType = (int)asset.Type;
            icon = new SerializedSprite(asset.Tile.sprite);
            color = new SerializedColor(asset.Tile.color);
        }

        /// <summary>
        /// Turns the Tile Asset into a Unity readable format.
        /// </summary>
        /// <returns></returns>
        public override TileAsset Deserialize()
        {
            return new TileAsset(id,
                                 title,
                                 icon.Deserialize(),
                                 author,
                                 (TileType)tileType,
                                 color.Deserialize(),
                                 DateTime.Parse(creationDate));
        }
    }
}