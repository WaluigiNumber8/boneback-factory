using BoubakProductions.Systems.Serialization;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Editors.TileData;
using System;
using System.Collections.Generic;
using Rogium.Editors.PaletteData;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the PackAsset Class, ready for saving.
    /// </summary>
    [System.Serializable]
    public class SerializedPackAsset
    {
        public readonly SerializedPackInfoAsset packInfo;
        public readonly SerializedList<PaletteAsset, SerializedPaletteAsset> palettes;
        public readonly SerializedList<TileAsset, SerializedTileAsset> tiles; 
        public readonly SerializedList<RoomAsset, SerializedRoomAsset> rooms; 

        public SerializedPackAsset(PackAsset asset)
        {
            this.packInfo = new SerializedPackInfoAsset(asset.PackInfo);
            this.palettes = new SerializedList<PaletteAsset, SerializedPaletteAsset>(asset.Palettes, palette => new SerializedPaletteAsset(palette));
            this.tiles = new SerializedList<TileAsset, SerializedTileAsset>(asset.Tiles, tile => new SerializedTileAsset(tile));
            this.rooms = new SerializedList<RoomAsset, SerializedRoomAsset>(asset.Rooms, room => new SerializedRoomAsset(room));
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

            IList<PaletteAsset> deserializedPalettes = palettes.Deserialize(palette => palette.Deserialize());
            IList<TileAsset> deserializedTiles = tiles.Deserialize(tile => tile.Deserialize());
            IList<RoomAsset> deserializedRooms = rooms.Deserialize(room => room.Deserialize());
            return new PackAsset(deserializedInfo, deserializedPalettes, deserializedTiles, deserializedRooms);
        }
    }
}