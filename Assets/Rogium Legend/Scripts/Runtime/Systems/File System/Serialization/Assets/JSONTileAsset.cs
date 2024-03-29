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
        public JSONColor color;
        public int type;
        public int layerType;
        public int terrainType;

        public JSONTileAsset(TileAsset asset) : base(asset)
        {
            icon = new JSONSprite(asset.Tile.sprite);
            color = new JSONColor(asset.Tile.color);
            type = (int)asset.Type;
            layerType = (int)asset.LayerType;
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
                                 color.Decode(),
                                 (TileType)type,
                                 (TileLayerType)layerType,
                                 (TerrainType)terrainType,
                                 DateTime.Parse(creationDate));
        }
    }
}