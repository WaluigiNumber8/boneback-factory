using RogiumLegend.Editors.PackData;
using RogiumLegend.Editors.RoomData;
using RogiumLegend.Editors.TileData;
using System;
using System.Collections.Generic;

namespace RogiumLegend.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the PackAsset Class, ready for saving.
    /// </summary>
    [System.Serializable]
    public class SerializedPackAsset
    {
        public readonly SerializedPackInfoAsset packInfo;
        public readonly SerializedList<RoomAsset, SerializedRoomAsset> rooms; 
        public readonly SerializedList<TileAsset, SerializedTileAsset> tiles; 

        public SerializedPackAsset(PackAsset pack)
        {
            this.packInfo = new SerializedPackInfoAsset(pack.PackInfo);
            this.tiles = new SerializedList<TileAsset, SerializedTileAsset>(pack.Tiles, _ => new SerializedTileAsset());
            this.rooms = new SerializedList<RoomAsset, SerializedRoomAsset>(pack.Rooms, _ => new SerializedRoomAsset());
        }

        /// <summary>
        /// Deserializes the Pack, so that it can be red by Unity.
        /// </summary>
        /// <returns>The Pack Asset in a readable form.</returns>
        public PackAsset Deserialize()
        {
            PackInfoAsset packInfo = new PackInfoAsset(this.packInfo.title,
                                                       this.packInfo.icon.Deserialize(),
                                                       this.packInfo.author,
                                                       this.packInfo.description,
                                                       DateTime.Parse(this.packInfo.creationTime));

            IList<TileAsset> deserializedTiles = tiles.Deserialize(tile => tile.Deserialize());
            IList<RoomAsset> deserializedRooms = rooms.Deserialize(room => room.Deserialize(deserializedTiles));
            return new PackAsset(packInfo, deserializedRooms, deserializedTiles);
        }
    }
}