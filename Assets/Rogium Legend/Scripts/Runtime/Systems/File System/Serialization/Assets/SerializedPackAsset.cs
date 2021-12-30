using BoubakProductions.Systems.Serialization;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using System.Collections.Generic;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the PackAsset Class, ready for saving.
    /// </summary>
    [System.Serializable]
    public class SerializedPackAsset
    {
        private SerializedPackInfoAsset packInfo;
        private SerializedList<PaletteAsset, SerializedPaletteAsset> palettes;
        private SerializedList<SpriteAsset, SerializedSpriteAsset> sprites;
        private SerializedList<TileAsset, SerializedTileAsset> tiles; 
        private SerializedList<RoomAsset, SerializedRoomAsset> rooms; 

        public SerializedPackAsset(PackAsset asset)
        {
            this.packInfo = new SerializedPackInfoAsset(asset.PackInfo);
            this.palettes = new SerializedList<PaletteAsset, SerializedPaletteAsset>(asset.Palettes, palette => new SerializedPaletteAsset(palette));
            this.sprites = new SerializedList<SpriteAsset, SerializedSpriteAsset>(asset.Sprites, sprite => new SerializedSpriteAsset(sprite));
            this.tiles = new SerializedList<TileAsset, SerializedTileAsset>(asset.Tiles, tile => new SerializedTileAsset(tile));
            this.rooms = new SerializedList<RoomAsset, SerializedRoomAsset>(asset.Rooms, room => new SerializedRoomAsset(room));
        }

        /// <summary>
        /// Deserializes the Pack, so that it can be red by Unity.
        /// </summary>
        /// <returns>The Pack Asset in a readable form.</returns>
        public PackAsset Deserialize()
        {
            PackInfoAsset deserializedInfo = this.packInfo.Deserialize();
            
            IList<PaletteAsset> deserializedPalettes = palettes.Deserialize(palette => palette.Deserialize());
            IList<SpriteAsset> deserializedSprites = sprites.Deserialize(sprite => sprite.Deserialize());
            IList<TileAsset> deserializedTiles = tiles.Deserialize(tile => tile.Deserialize());
            IList<RoomAsset> deserializedRooms = rooms.Deserialize(room => room.Deserialize());
            
            return new PackAsset(deserializedInfo, deserializedPalettes, deserializedSprites, deserializedTiles,
                                deserializedRooms);
        }
    }
}