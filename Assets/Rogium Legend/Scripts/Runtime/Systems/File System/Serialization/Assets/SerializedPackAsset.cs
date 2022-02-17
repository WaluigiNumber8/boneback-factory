using BoubakProductions.Systems.FileSystem.Serialization;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using System.Collections.Generic;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the PackAsset Class, ready for saving.
    /// </summary>
    [System.Serializable]
    public class SerializedPackAsset : ISerializedObject<PackAsset>
    {
        private SerializedPackInfoAsset packInfo;
        // private SerializedList<PaletteAsset, SerializedPaletteAsset> palettes;
        // private SerializedList<SpriteAsset, SerializedSpriteAsset> sprites;
        // private SerializedList<TileAsset, SerializedTileAsset> tiles; 
        // private SerializedList<RoomAsset, SerializedRoomAsset> rooms; 

        public SerializedPackAsset(PackAsset asset)
        {
            packInfo = new SerializedPackInfoAsset(asset.PackInfo);
            // palettes = new SerializedList<PaletteAsset, SerializedPaletteAsset>(asset.Palettes, p => new SerializedPaletteAsset(p), p => p.Deserialize());
            // sprites = new SerializedList<SpriteAsset, SerializedSpriteAsset>(asset.Sprites, sprite => new SerializedSpriteAsset(sprite), s => s.Deserialize());
            // tiles = new SerializedList<TileAsset, SerializedTileAsset>(asset.Tiles, t => new SerializedTileAsset(t), t => t.Deserialize());
            // rooms = new SerializedList<RoomAsset, SerializedRoomAsset>(asset.Rooms, r => new SerializedRoomAsset(r), r => r.Deserialize());
        }

        public PackAsset Deserialize()
        {
            PackInfoAsset deserializedInfo = packInfo.Deserialize();
            
            // IList<PaletteAsset> deserializedPalettes = palettes.Deserialize();
            // IList<SpriteAsset> deserializedSprites = sprites.Deserialize();
            // IList<TileAsset> deserializedTiles = tiles.Deserialize();
            // IList<RoomAsset> deserializedRooms = rooms.Deserialize();
            
            return new PackAsset(deserializedInfo);
        }
    }
}