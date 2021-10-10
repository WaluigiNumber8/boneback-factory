using Rogium.Editors.PackData;
using Rogium.Editors.RoomData;
using Rogium.Editors.TileData;
using System;
using System.Collections.Generic;

namespace Rogium.ExternalStorage.Serialization
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
            this.tiles = new SerializedList<TileAsset, SerializedTileAsset>(pack.Tiles, tile => new SerializedTileAsset(tile));
            this.rooms = new SerializedList<RoomAsset, SerializedRoomAsset>(pack.Rooms, room => new SerializedRoomAsset(room));
        }

        /// <summary>
        /// Deserializes the Pack, so that it can be red by Unity.
        /// </summary>
        /// <returns>The Pack Asset in a readable form.</returns>
        public PackAsset Deserialize()
        {
            PackInfoAsset deserializedInfo = new PackInfoAsset(this.packInfo.id,
                                                               this.packInfo.title,
                                                               this.packInfo.icon.Deserialize(),
                                                               this.packInfo.author,
                                                               this.packInfo.description,
                                                               DateTime.Parse(this.packInfo.creationTime));

            IList<TileAsset> deserializedTiles = tiles.Deserialize(tile => tile.Deserialize());
            IList<RoomAsset> deserializedRooms = rooms.Deserialize(room => room.Deserialize());
            return new PackAsset(deserializedInfo, deserializedRooms, deserializedTiles);
        }
    }
}