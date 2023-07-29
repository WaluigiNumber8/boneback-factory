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
    public class PackAsset : AssetWithReferencedSpriteBase
    {
        private PackInfoAsset packInfo;
        private readonly AssetList<PaletteAsset> palettes;
        private readonly AssetList<SpriteAsset> sprites;
        private readonly AssetList<WeaponAsset> weapons;
        private readonly AssetList<ProjectileAsset> projectiles;
        private readonly AssetList<EnemyAsset> enemies;
        private readonly AssetList<RoomAsset> rooms;
        private readonly AssetList<TileAsset> tiles;

        private readonly ExternalStorageOverseer ex = ExternalStorageOverseer.Instance;
        
        #region Constructors
        public PackAsset()
        {
            
            packInfo = new PackInfoAsset(EditorConstants.PackTitle,
                                         EditorConstants.PackIcon,
                                         EditorConstants.Author,
                                         EditorConstants.PackDescription);
           
            GatherValuesFromInfo(packInfo);
            palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete);
            sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete);
            weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete);
            projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete);
            enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete);
            rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete);
            tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete);
        }
        public PackAsset(PackInfoAsset packInfo)
        {
            this.packInfo = new PackInfoAsset(packInfo);
            
            GatherValuesFromInfo(packInfo);
            palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete);
            sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete);
            weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete);
            projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete);
            enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete);
            rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete);
            tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete);
        }
        
        public PackAsset(PackAsset asset)
        {
            packInfo = new PackInfoAsset(asset.packInfo);
            
            GatherValuesFromInfo(packInfo);
            palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, asset.Palettes);
            sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, asset.Sprites);
            weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete, asset.Weapons);
            projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete, asset.Projectiles);
            enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete, asset.Enemies);
            rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, asset.Rooms);
            tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, asset.Tiles);
        }
        public PackAsset(PackInfoAsset packInfo, IList<PaletteAsset> palettes, IList<SpriteAsset> sprites,
                        IList<WeaponAsset> weapons, IList<ProjectileAsset> projectiles, IList<EnemyAsset> enemies,
                        IList<RoomAsset> rooms, IList<TileAsset> tiles)
        {
            this.packInfo = packInfo;
            
            GatherValuesFromInfo(packInfo);
            this.palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, palettes);
            this.sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, sprites);
            this.weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete, weapons);
            this.projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete, projectiles);
            this.enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete, enemies);
            this.rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, rooms);
            this.tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, tiles);
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

        public override int GetHashCode()
        {
            return int.Parse(packInfo.ID);
        }

        public bool ContainsAnyPalettes => (palettes?.Count > 0);
        public bool ContainsAnySprites => (sprites?.Count > 0);
        public bool ContainsAnyWeapons => (weapons?.Count > 0);
        public bool ContainsAnyProjectiles => (projectiles?.Count > 0);
        public bool ContainsAnyEnemies => (enemies?.Count > 0);
        public bool ContainsAnyRooms => (rooms?.Count > 0);
        public bool ContainsAnyTiles => (tiles?.Count > 0);
        
        public PackInfoAsset PackInfo { get => packInfo; }
        public AssetList<PaletteAsset> Palettes { get => palettes; }
        public AssetList<SpriteAsset> Sprites { get => sprites; }
        public AssetList<WeaponAsset> Weapons { get => weapons; }
        public AssetList<ProjectileAsset> Projectiles { get => projectiles; }
        public AssetList<EnemyAsset> Enemies { get => enemies; }
        public AssetList<RoomAsset> Rooms { get => rooms; }
        public AssetList<TileAsset> Tiles { get => tiles; }
    }
}

