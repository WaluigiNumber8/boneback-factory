using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Systems.FileSystem.JSON.Serialization;
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
    [Serializable]
    public class JSONCampaignAsset : JSONAssetBase<CampaignAsset>
    {
        public JSONList<PaletteAsset, JSONPaletteAsset> palettes;
        public JSONList<SpriteAsset, JSONSpriteAsset> sprites;
        public JSONList<WeaponAsset, JSONWeaponAsset> weapons;
        public JSONList<ProjectileAsset, JSONProjectileAsset> projectiles;
        public JSONList<EnemyAsset, JSONEnemyAsset> enemies;
        public JSONList<RoomAsset, JSONRoomAsset> rooms;
        public JSONList<TileAsset, JSONTileAsset> tiles;

        public int adventureLength;
        public string[] packReferences;

        public JSONCampaignAsset(CampaignAsset asset) : base(asset)
        {
            palettes = new JSONList<PaletteAsset, JSONPaletteAsset>(asset.DataPack.Palettes, p => new JSONPaletteAsset(p));
            sprites = new JSONList<SpriteAsset, JSONSpriteAsset>(asset.DataPack.Sprites, s => new JSONSpriteAsset(s));
            weapons = new JSONList<WeaponAsset, JSONWeaponAsset>(asset.DataPack.Weapons, w => new JSONWeaponAsset(w));
            projectiles = new JSONList<ProjectileAsset, JSONProjectileAsset>(asset.DataPack.Projectiles, p => new JSONProjectileAsset(p));
            enemies = new JSONList<EnemyAsset, JSONEnemyAsset>(asset.DataPack.Enemies, e => new JSONEnemyAsset(e));
            rooms = new JSONList<RoomAsset, JSONRoomAsset>(asset.DataPack.Rooms, r => new JSONRoomAsset(r));
            tiles = new JSONList<TileAsset, JSONTileAsset>(asset.DataPack.Tiles, t => new JSONTileAsset(t));

            adventureLength = asset.AdventureLength;
            packReferences = asset.PackReferences.ToArray();
        }

        /// <summary>
        /// Deserialize this Campaign.
        /// </summary>
        /// <returns>The deserialized form of the campaign.</returns>
        public override CampaignAsset Decode()
        {
            palettes.SetDecodingMethod(p => p.Decode());
            sprites.SetDecodingMethod(s => s.Decode());
            weapons.SetDecodingMethod(w => w.Decode());
            projectiles.SetDecodingMethod(p => p.Decode());
            enemies.SetDecodingMethod(e => e.Decode());
            rooms.SetDecodingMethod(r => r.Decode());
            tiles.SetDecodingMethod(t => t.Decode());

            PackAsset dataPack = new(palettes.Decode(), sprites.Decode(), weapons.Decode(), projectiles.Decode(),
                enemies.Decode(), rooms.Decode(), tiles.Decode());

            return new CampaignAsset.Builder()
                .WithID(id)
                .WithTitle(title)
                .WithIcon(icon.Decode())
                .WithAuthor(author)
                .WithCreationDate(DateTime.Parse(creationDate))
                .WithAdventureLength(adventureLength)
                .WithDataPack(dataPack)
                .WithPackReferences(packReferences)
                .Build();
        }
    }
}