using Rogium.Editors.Sprites;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.ExternalStorage.Serialization;
using Rogium.Options.Core;
using UnityEngine;

namespace Rogium.ExternalStorage
{
    /// <summary>
    /// Overseers files stored on external storage.
    /// </summary>
    public sealed class ExternalStorageOverseer : IExternalStorageOverseer
    {
        private readonly CRUDPackOperations packCRUD;
        private readonly CRUDOperations<CampaignAsset, JSONCampaignAsset> campaignCRUD;
        private readonly CRUDOperations<PaletteAsset, JSONPaletteAsset> paletteCRUD;
        private readonly CRUDOperations<SpriteAsset, JSONSpriteAsset> spriteCRUD;
        private readonly CRUDOperations<WeaponAsset, JSONWeaponAsset> weaponCRUD;
        private readonly CRUDOperations<ProjectileAsset, JSONProjectileAsset> projectileCRUD;
        private readonly CRUDOperations<EnemyAsset, JSONEnemyAsset> enemyCRUD;
        private readonly CRUDOperations<RoomAsset, JSONRoomAsset> roomCRUD;
        private readonly CRUDOperations<TileAsset, JSONTileAsset> tileCRUD;
        
        private readonly CRUDOperations<PreferencesAsset, JSONPreferencesAsset> preferencesCRUD;
        private readonly CRUDOperations<InputBindingsAsset, JSONInputBindingsAsset> inputBindingsCRUD;
        private readonly CRUDOperations<ShortcutBindingsAsset, JSONShortcutBindingsAsset> shortcutBindingsCRUD;

        public ExternalStorageOverseer()
        {
            packCRUD = new CRUDPackOperations(RefreshAssetSaveableData, CreatePack);
            packCRUD.RefreshSaveableData(new SaveableData("Packs", EditorAssetIDs.PackIdentifier));

            campaignCRUD = new CRUDOperations<CampaignAsset, JSONCampaignAsset>(p => new JSONCampaignAsset(p), EditorAssetIDs.CampaignIdentifier);
            campaignCRUD.RefreshSaveableData(new SaveableData("Campaigns", EditorAssetIDs.CampaignIdentifier));

            preferencesCRUD = new CRUDOperations<PreferencesAsset, JSONPreferencesAsset>(p => new JSONPreferencesAsset(p), EditorAssetIDs.PreferencesIdentifier, false);
            preferencesCRUD.RefreshSaveableData(new SaveableData("", EditorAssetIDs.PreferencesIdentifier));
            inputBindingsCRUD = new CRUDOperations<InputBindingsAsset, JSONInputBindingsAsset>(i => new JSONInputBindingsAsset(i), EditorAssetIDs.InputIdentifier, false);
            inputBindingsCRUD.RefreshSaveableData(new SaveableData("", EditorAssetIDs.InputIdentifier));
            shortcutBindingsCRUD = new CRUDOperations<ShortcutBindingsAsset, JSONShortcutBindingsAsset>(s => new JSONShortcutBindingsAsset(s), EditorAssetIDs.ShortcutIdentifier, false);
            shortcutBindingsCRUD.RefreshSaveableData(new SaveableData("", EditorAssetIDs.ShortcutIdentifier));
            
            paletteCRUD = new CRUDOperations<PaletteAsset, JSONPaletteAsset>(p => new JSONPaletteAsset(p), EditorAssetIDs.PaletteIdentifier);
            spriteCRUD = new CRUDOperations<SpriteAsset, JSONSpriteAsset>(s => new JSONSpriteAsset(s), EditorAssetIDs.SpriteIdentifier);
            weaponCRUD = new CRUDOperations<WeaponAsset, JSONWeaponAsset>(w => new JSONWeaponAsset(w), EditorAssetIDs.WeaponIdentifier);
            projectileCRUD = new CRUDOperations<ProjectileAsset, JSONProjectileAsset>(p => new JSONProjectileAsset(p), EditorAssetIDs.ProjectileIdentifier);
            enemyCRUD = new CRUDOperations<EnemyAsset, JSONEnemyAsset>(e => new JSONEnemyAsset(e), EditorAssetIDs.EnemyIdentifier);
            roomCRUD = new CRUDOperations<RoomAsset, JSONRoomAsset>(r => new JSONRoomAsset(r), EditorAssetIDs.RoomIdentifier);
            tileCRUD = new CRUDOperations<TileAsset, JSONTileAsset>(t => new JSONTileAsset(t), EditorAssetIDs.TileIdentifier);
        }

        /// <summary>
        /// Refreshes saveable data for all assets based on the current pack.
        /// </summary>
        private void RefreshAssetSaveableData(PackPathInfo packInfo)
        {
            paletteCRUD.RefreshSaveableData(packInfo.PalettesData);
            spriteCRUD.RefreshSaveableData(packInfo.SpritesData);
            weaponCRUD.RefreshSaveableData(packInfo.WeaponsData);
            projectileCRUD.RefreshSaveableData(packInfo.ProjectilesData);
            enemyCRUD.RefreshSaveableData(packInfo.EnemiesData);
            roomCRUD.RefreshSaveableData(packInfo.RoomsData);
            tileCRUD.RefreshSaveableData(packInfo.TilesData);
        }
        
        private PackAsset CreatePack(PackAsset pack)
        {
            return new PackAsset.Builder()
                .WithID(pack.ID)
                .WithTitle(pack.Title)
                .WithIcon(pack.Icon)
                .WithAuthor(pack.Author)
                .WithCreationDate(pack.CreationDate)
                .WithAssociatedSpriteID(pack.AssociatedSpriteID)
                .WithDescription(pack.Description)
                .WithPalettes(paletteCRUD.LoadAll())
                .WithSprites(spriteCRUD.LoadAll())
                .WithWeapons(weaponCRUD.LoadAll())
                .WithProjectiles(projectileCRUD.LoadAll())
                .WithEnemies(enemyCRUD.LoadAll())
                .WithRooms(roomCRUD.LoadAll())
                .WithTiles(tileCRUD.LoadAll())
                .Build();
        }

        public ICRUDOperations<PackAsset, JSONPackAsset> Packs { get => packCRUD; }
        public ICRUDOperations<CampaignAsset, JSONCampaignAsset> Campaigns { get => campaignCRUD; }
        public ICRUDOperations<PaletteAsset, JSONPaletteAsset> Palettes { get => paletteCRUD; }
        public ICRUDOperations<SpriteAsset, JSONSpriteAsset> Sprites { get => spriteCRUD; }
        public ICRUDOperations<WeaponAsset, JSONWeaponAsset> Weapons { get => weaponCRUD; }
        public ICRUDOperations<ProjectileAsset, JSONProjectileAsset> Projectiles { get => projectileCRUD; }
        public ICRUDOperations<EnemyAsset, JSONEnemyAsset> Enemies { get => enemyCRUD; }
        public ICRUDOperations<RoomAsset, JSONRoomAsset> Rooms { get => roomCRUD; }
        public ICRUDOperations<TileAsset, JSONTileAsset> Tiles { get => tileCRUD; }
        public ICRUDOperations<PreferencesAsset, JSONPreferencesAsset> Preferences { get => preferencesCRUD; }
        public ICRUDOperations<InputBindingsAsset, JSONInputBindingsAsset> InputBindings { get => inputBindingsCRUD; }
        public ICRUDOperations<ShortcutBindingsAsset, JSONShortcutBindingsAsset> ShortcutBindings { get => shortcutBindingsCRUD; }
    }
}