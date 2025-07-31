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
    /// <summary>
    /// Represents an overseer that manages all external storage operations.
    /// </summary>
    public interface IExternalStorageOverseer
    {
        public ICRUDOperations<PackAsset, JSONPackAsset>  Packs { get; }
        public ICRUDOperations<CampaignAsset, JSONCampaignAsset> Campaigns { get; }
        public ICRUDOperations<PaletteAsset, JSONPaletteAsset> Palettes { get; }
        public ICRUDOperations<SpriteAsset, JSONSpriteAsset> Sprites { get; }
        public ICRUDOperations<WeaponAsset, JSONWeaponAsset> Weapons { get; }
        public ICRUDOperations<ProjectileAsset, JSONProjectileAsset> Projectiles { get; }
        public ICRUDOperations<EnemyAsset, JSONEnemyAsset> Enemies { get; }
        public ICRUDOperations<RoomAsset, JSONRoomAsset> Rooms { get; }
        public ICRUDOperations<TileAsset, JSONTileAsset> Tiles { get; }
        public ICRUDOperations<PreferencesAsset, JSONPreferencesAsset> Preferences { get; }
        public ICRUDOperations<InputBindingsAsset, JSONInputBindingsAsset> InputBindings { get; }
        public ICRUDOperations<ShortcutBindingsAsset, JSONShortcutBindingsAsset> ShortcutBindings { get; }
    }
}