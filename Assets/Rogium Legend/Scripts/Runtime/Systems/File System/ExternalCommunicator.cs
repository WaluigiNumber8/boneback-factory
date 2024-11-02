using System.Collections.Generic;
using RedRats.Core;
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
    /// Communicates with the external storage.
    /// </summary>
    public class ExternalCommunicator : Singleton<ExternalCommunicator>, IExternalStorageOverseer
    {
        private IExternalStorageOverseer storage = new ExternalStorageOverseer();
        
        public void OverrideStorageOverseer(IExternalStorageOverseer newStorage) => storage = newStorage;
        
        public CRUDPackOperations Packs { get => storage.Packs; }
        public ICRUDOperations<CampaignAsset, JSONCampaignAsset> Campaigns { get => storage.Campaigns; }
        public ICRUDOperations<PaletteAsset, JSONPaletteAsset> Palettes { get => storage.Palettes; }
        public ICRUDOperations<SpriteAsset, JSONSpriteAsset> Sprites { get => storage.Sprites; }
        public ICRUDOperations<WeaponAsset, JSONWeaponAsset> Weapons { get => storage.Weapons; }
        public ICRUDOperations<ProjectileAsset, JSONProjectileAsset> Projectiles { get => storage.Projectiles; }
        public ICRUDOperations<EnemyAsset, JSONEnemyAsset> Enemies { get => storage.Enemies; }
        public ICRUDOperations<RoomAsset, JSONRoomAsset> Rooms { get => storage.Rooms; }
        public ICRUDOperations<TileAsset, JSONTileAsset> Tiles { get => storage.Tiles; }
        public ICRUDOperations<GameDataAsset, JSONGameDataAsset> Preferences { get => storage.Preferences; }
    }
}