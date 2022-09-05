using Rogium.Editors.Tiles;
using System;
using RedRats.Systems.FileSystem.JSON.Serialization;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="TileAsset"/>.
    /// </summary>
    [System.Serializable]
    public class JSONTileAsset : JSONAssetBase<TileAsset>
    {
        public int tileType;
        public JSONColor color;

        public JSONTileAsset(TileAsset asset) : base(asset)
        {
            tileType = (int)asset.Type;
            icon = new JSONSprite(asset.Tile.sprite);
            color = new JSONColor(asset.Tile.color);
        }

        /// <summary>
        /// Turns the Tile Asset into a Unity readable format.
        /// </summary>
        /// <returns></returns>
        public override TileAsset Decode()
        {
            return new TileAsset(id,
                                 title,
                                 icon.Decode(),
                                 author,
                                 (TileType)tileType,
                                 color.Decode(),
                                 DateTime.Parse(creationDate));
        }
    }
}