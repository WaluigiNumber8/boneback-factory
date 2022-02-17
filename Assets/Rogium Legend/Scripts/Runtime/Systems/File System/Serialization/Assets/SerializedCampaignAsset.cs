using Rogium.Editors.Campaign;
using System;
using System.Collections.Generic;
using BoubakProductions.Systems.FileSystem.Serialization;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// The Serialized form of the Campaign Asset.
    /// </summary>
    [System.Serializable]
    public class SerializedCampaignAsset : SerializedAssetBase<CampaignAsset>
    {
        private SerializedList<PaletteAsset, SerializedPaletteAsset> palettes;
        private SerializedList<SpriteAsset, SerializedSpriteAsset> sprites;
        private SerializedList<TileAsset, SerializedTileAsset> tiles; 
        private SerializedList<RoomAsset, SerializedRoomAsset> rooms; 
        public readonly IList<string> packReferences;

        public SerializedCampaignAsset(CampaignAsset asset) : base(asset)
        {
            palettes = new SerializedList<PaletteAsset, SerializedPaletteAsset>(asset.DataPack.Palettes, p => new SerializedPaletteAsset(p), p => p.Deserialize());
            sprites = new SerializedList<SpriteAsset, SerializedSpriteAsset>(asset.DataPack.Sprites, sprite => new SerializedSpriteAsset(sprite), s => s.Deserialize());
            tiles = new SerializedList<TileAsset, SerializedTileAsset>(asset.DataPack.Tiles, t => new SerializedTileAsset(t), t => t.Deserialize());
            rooms = new SerializedList<RoomAsset, SerializedRoomAsset>(asset.DataPack.Rooms, r => new SerializedRoomAsset(r), r => r.Deserialize());
            
            this.packReferences = new List<string>(asset.PackReferences);
        }
        
        /// <summary>
        /// Deserialize this Campaign.
        /// </summary>
        /// <returns>The deserialized form of the campaign.</returns>
        public override CampaignAsset Deserialize()
        {
            IList<PaletteAsset> deserializedPalettes = palettes.Deserialize();
            IList<SpriteAsset> deserializedSprites = sprites.Deserialize();
            IList<TileAsset> deserializedTiles = tiles.Deserialize();
            IList<RoomAsset> deserializedRooms = rooms.Deserialize();

            PackAsset dataPack = new PackAsset(new PackInfoAsset(), deserializedPalettes, deserializedSprites, deserializedTiles, deserializedRooms);
            
            return new CampaignAsset(id,
                                     title,
                                     icon.Deserialize(),
                                     author,
                                     DateTime.Parse(creationDate),
                                     dataPack,
                                     packReferences);
        }
        
    }
}