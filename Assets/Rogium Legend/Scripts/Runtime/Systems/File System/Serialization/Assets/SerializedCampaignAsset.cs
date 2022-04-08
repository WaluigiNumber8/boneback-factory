using System;
using System.Collections.Generic;
using BoubakProductions.Systems.FileSystem.Serialization;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Campaign;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="CampaignAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedCampaignAsset : SerializedAssetBase<CampaignAsset>
    {
        private readonly SerializedList<PaletteAsset, SerializedPaletteAsset> palettes;
        private readonly SerializedList<SpriteAsset, SerializedSpriteAsset> sprites;
        private readonly SerializedList<WeaponAsset, SerializedWeaponAsset> weapons;
        private readonly SerializedList<ProjectileAsset, SerializedProjectileAsset> projectiles;
        private readonly SerializedList<EnemyAsset, SerializedEnemyAsset> enemies;
        private readonly SerializedList<RoomAsset, SerializedRoomAsset> rooms; 
        private readonly SerializedList<TileAsset, SerializedTileAsset> tiles;

        private readonly int adventureLength;
        private readonly IList<string> packReferences;

        public SerializedCampaignAsset(CampaignAsset asset) : base(asset)
        {
            palettes = new SerializedList<PaletteAsset, SerializedPaletteAsset>(asset.DataPack.Palettes, p => new SerializedPaletteAsset(p), p => p.Deserialize());
            sprites = new SerializedList<SpriteAsset, SerializedSpriteAsset>(asset.DataPack.Sprites, s => new SerializedSpriteAsset(s), s => s.Deserialize());
            weapons = new SerializedList<WeaponAsset, SerializedWeaponAsset>(asset.DataPack.Weapons, w => new SerializedWeaponAsset(w), w => w.Deserialize());
            projectiles = new SerializedList<ProjectileAsset, SerializedProjectileAsset>(asset.DataPack.Projectiles, p => new SerializedProjectileAsset(p), p => p.Deserialize());
            enemies = new SerializedList<EnemyAsset, SerializedEnemyAsset>(asset.DataPack.Enemies, e => new SerializedEnemyAsset(e), e => e.Deserialize());
            rooms = new SerializedList<RoomAsset, SerializedRoomAsset>(asset.DataPack.Rooms, r => new SerializedRoomAsset(r), r => r.Deserialize());
            tiles = new SerializedList<TileAsset, SerializedTileAsset>(asset.DataPack.Tiles, t => new SerializedTileAsset(t), t => t.Deserialize());

            adventureLength = asset.AdventureLength;
            packReferences = new List<string>(asset.PackReferences);
        }
        
        /// <summary>
        /// Deserialize this Campaign.
        /// </summary>
        /// <returns>The deserialized form of the campaign.</returns>
        public override CampaignAsset Deserialize()
        {
            PackAsset dataPack = new PackAsset(new PackInfoAsset(), palettes.Deserialize(), sprites.Deserialize(), 
                                               weapons.Deserialize(), projectiles.Deserialize(), enemies.Deserialize(),
                                               rooms.Deserialize(), tiles.Deserialize());
            
            return new CampaignAsset(id,
                                     title,
                                     icon.Deserialize(),
                                     author,
                                     DateTime.Parse(creationDate),
                                     adventureLength,
                                     dataPack,
                                     packReferences);
        }
        
    }
}