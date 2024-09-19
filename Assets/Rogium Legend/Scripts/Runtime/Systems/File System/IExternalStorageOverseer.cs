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
        /// <summary>
        /// Create a new pack on external storage.
        /// </summary>
        /// <param name="pack">The data to create the pack with.</param>
        void CreatePack(PackAsset pack);

        /// <summary>
        /// Updates information
        /// </summary>
        /// <param name="pack"></param>
        void UpdatePack(PackAsset pack);

        /// <summary>
        /// Loads all packs stored at application persistent path.
        /// </summary>
        /// <returns>A list of all <see cref="PackAsset"/>s.</returns>
        IList<PackAsset> LoadAllPacks();

        /// <summary>
        /// Delete a pack from external storage.
        /// </summary>
        /// <param name="pack">The pack to delete.</param>
        /// <exception cref="InvalidOperationException">Is thrown when pack doesn't exist.</exception>
        void DeletePack(PackAsset pack);

        /// <summary>
        /// Prepares the overseer for working with a specific pack and loads it's data.
        /// </summary>
        /// <param name="pack">The pack to load.</param>
        PackAsset LoadPack(PackAsset pack);

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