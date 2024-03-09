using Rogium.Editors.Tiles;
using System;
using RedRats.Systems.FileSystem.JSON.Serialization;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="TileAsset"/>.
    /// </summary>
    [Serializable]
    public class JSONTileAsset : JSONAssetWithReferencedSpriteBase<TileAsset>
    {
        public int tileType;
        public JSONColor color;
        public int terrainType;

        public JSONTileAsset(TileAsset asset) : base(asset)
        {
            tileType = (int)asset.Type;
            icon = new JSONSprite(asset.Tile.sprite);
            color = new JSONColor(asset.Tile.color);
            terrainType = (int)asset.TerrainType;
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
                                 associatedSpriteID,
                                 (TileType)tileType,
                                 color.Decode(),
                                 (TerrainType)terrainType,
                                 DateTime.Parse(creationDate));
        }
    }
}