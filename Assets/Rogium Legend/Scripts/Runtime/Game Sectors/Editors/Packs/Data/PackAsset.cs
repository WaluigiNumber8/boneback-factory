using System.Collections.Generic;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Enemies;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.ExternalStorage;

namespace Rogium.Editors.Packs
{
    /// <summary>
    /// Contains all important data for a given pack.
    /// </summary>
    public class PackAsset : AssetBase
    {
        private PackInfoAsset packInfo;
        private readonly AssetDictionary<PaletteAsset> palettes;
        private readonly AssetDictionary<SpriteAsset> sprites;
        private readonly AssetDictionary<WeaponAsset> weapons;
        private readonly AssetDictionary<ProjectileAsset> projectiles;
        private readonly AssetDictionary<EnemyAsset> enemies;
        private readonly AssetDictionary<RoomAsset> rooms;
        private readonly AssetDictionary<TileAsset> tiles;

        private readonly ExternalStorageOverseer ex = ExternalStorageOverseer.Instance;
        
        #region Constructors
        public PackAsset()
        {
            
            packInfo = new PackInfoAsset(EditorDefaults.PackTitle,
                                         EditorDefaults.PackIcon,
                                         EditorDefaults.Author,
                                         EditorDefaults.PackDescription);
           
            GatherValuesFromInfo(packInfo);
            palettes = new AssetDictionary<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete);
            sprites = new AssetDictionary<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete);
            weapons = new AssetDictionary<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete);
            projectiles = new AssetDictionary<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete);
            enemies = new AssetDictionary<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete);
            rooms = new AssetDictionary<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete);
            tiles = new AssetDictionary<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete);
        }
        public PackAsset(PackInfoAsset packInfo)
        {
            this.packInfo = new PackInfoAsset(packInfo);
            
            GatherValuesFromInfo(packInfo);
            palettes = new AssetDictionary<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete);
            sprites = new AssetDictionary<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete);
            weapons = new AssetDictionary<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete);
            projectiles = new AssetDictionary<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete);
            enemies = new AssetDictionary<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete);
            rooms = new AssetDictionary<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete);
            tiles = new AssetDictionary<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete);
        }
        
        public PackAsset(PackAsset asset)
        {
            packInfo = new PackInfoAsset(asset.packInfo);
            
            GatherValuesFromInfo(packInfo);
            palettes = new AssetDictionary<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, asset.Palettes);
            sprites = new AssetDictionary<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, asset.Sprites);
            weapons = new AssetDictionary<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete, asset.Weapons);
            projectiles = new AssetDictionary<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete, asset.Projectiles);
            enemies = new AssetDictionary<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete, asset.Enemies);
            rooms = new AssetDictionary<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, asset.Rooms);
            tiles = new AssetDictionary<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, asset.Tiles);
        }
        public PackAsset(PackInfoAsset packInfo, IDictionary<string, PaletteAsset> palettes, IDictionary<string, SpriteAsset> sprites,
                         IDictionary<string,WeaponAsset> weapons, IDictionary<string,ProjectileAsset> projectiles, 
                         IDictionary<string,EnemyAsset> enemies, IDictionary<string,RoomAsset> rooms, IDictionary<string,TileAsset> tiles)
        {
            this.packInfo = packInfo;
            
            GatherValuesFromInfo(packInfo);
            this.palettes = new AssetDictionary<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, palettes);
            this.sprites = new AssetDictionary<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, sprites);
            this.weapons = new AssetDictionary<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete, weapons);
            this.projectiles = new AssetDictionary<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete, projectiles);
            this.enemies = new AssetDictionary<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete, enemies);
            this.rooms = new AssetDictionary<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, rooms);
            this.tiles = new AssetDictionary<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, tiles);
        }
        #endregion

        private void GatherValuesFromInfo(PackInfoAsset info)
        {
            id = info.ID;
            title = info.Title;
            icon = info.Icon;
            author = info.Author;
            creationDate = info.CreationDate;
        }

        #region Update Values
        public void UpdatePackInfo(PackInfoAsset newPackInfo)
        {
            packInfo = new PackInfoAsset(newPackInfo);
            GatherValuesFromInfo(packInfo);
        }
        #endregion

        public override bool Equals(object obj)
        {
            PackAsset pack = (PackAsset)obj;
            if (pack == null) return false;
            if (pack.packInfo.Title == packInfo.Title &&
                pack.packInfo.Author == packInfo.Author &&
                pack.packInfo.CreationDate == packInfo.CreationDate)
                return true;
            return false;
        }

        public override int GetHashCode() => int.Parse(packInfo.ID);

        public bool ContainsAnyPalettes => (palettes?.Count > 0);
        public bool ContainsAnySprites => (sprites?.Count > 0);
        public bool ContainsAnyWeapons => (weapons?.Count > 0);
        public bool ContainsAnyProjectiles => (projectiles?.Count > 0);
        public bool ContainsAnyEnemies => (enemies?.Count > 0);
        public bool ContainsAnyRooms => (rooms?.Count > 0);
        public bool ContainsAnyTiles => (tiles?.Count > 0);
        
        public PackInfoAsset PackInfo { get => packInfo; }
        public AssetDictionary<PaletteAsset> Palettes { get => palettes; }
        public AssetDictionary<SpriteAsset> Sprites { get => sprites; }
        public AssetDictionary<WeaponAsset> Weapons { get => weapons; }
        public AssetDictionary<ProjectileAsset> Projectiles { get => projectiles; }
        public AssetDictionary<EnemyAsset> Enemies { get => enemies; }
        public AssetDictionary<RoomAsset> Rooms { get => rooms; }
        public AssetDictionary<TileAsset> Tiles { get => tiles; }
    }
}

