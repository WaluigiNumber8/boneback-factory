using System;
using System.Collections.Generic;
using Rogium.Editors.Campaign;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.ExternalStorage.Serialization;
using Rogium.Options.Core;

namespace Rogium.ExternalStorage
{
    public interface IExternalStorageOverseer
    {
        CRUDPackOperations Packs { get; }
        ICRUDOperations<CampaignAsset, JSONCampaignAsset> Campaigns { get; }
        ICRUDOperations<PaletteAsset, JSONPaletteAsset> Palettes { get; }
        ICRUDOperations<SpriteAsset, JSONSpriteAsset> Sprites { get; }
        ICRUDOperations<WeaponAsset, JSONWeaponAsset> Weapons { get; }
        ICRUDOperations<ProjectileAsset, JSONProjectileAsset> Projectiles { get; }
        ICRUDOperations<EnemyAsset, JSONEnemyAsset> Enemies { get; }
        ICRUDOperations<RoomAsset, JSONRoomAsset> Rooms { get; }
        ICRUDOperations<TileAsset, JSONTileAsset> Tiles { get; }
        ICRUDOperations<GameDataAsset, JSONGameDataAsset> Preferences { get; }
    }
}