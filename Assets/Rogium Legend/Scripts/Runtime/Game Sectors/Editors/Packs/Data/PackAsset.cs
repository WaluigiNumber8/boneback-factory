using System;
using System.Collections.Generic;
using Rogium.Core;
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
using Rogium.Systems.Validation;
using UnityEngine;

namespace Rogium.Editors.Packs
{
    /// <summary>
    /// Contains all important data for a given pack.
    /// </summary>
    public class PackAsset : AssetWithReferencedSpriteBase
    {
        private string description;
        private AssetList<PaletteAsset> palettes;
        private AssetList<SpriteAsset> sprites;
        private AssetList<WeaponAsset> weapons;
        private AssetList<ProjectileAsset> projectiles;
        private AssetList<EnemyAsset> enemies;
        private AssetList<RoomAsset> rooms;
        private AssetList<TileAsset> tiles;

        private PackAsset() { }

        #region Constructors

        // public PackAsset()
        // {
        //     InitBase(EditorDefaults.Instance.PackTitle, EditorDefaults.Instance.PackIcon, EditorDefaults.Instance.Author, DateTime.Now);
        //     GenerateID();
        //
        //     description = EditorDefaults.Instance.PackDescription;
        //     palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete);
        //     sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete);
        //     weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete);
        //     projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete);
        //     enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete);
        //     rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete);
        //     tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete);
        // }
        //
        // public PackAsset(PackAsset asset)
        // {
        //     AssetValidation.ValidateTitle(asset.title);
        //     AssetValidation.ValidateDescription(asset.description);
        //
        //     id = asset.ID;
        //     InitBase(asset.Title, asset.Icon, asset.Author, asset.CreationDate);
        //
        //     associatedSpriteID = asset.AssociatedSpriteID;
        //     
        //     description = asset.Description;
        //     palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, asset.Palettes);
        //     sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, asset.Sprites);
        //     weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete, asset.Weapons);
        //     projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete, asset.Projectiles);
        //     enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete, asset.Enemies);
        //     rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, asset.Rooms);
        //     tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, asset.Tiles);
        // }
        //
        // public PackAsset(string id, string title, Sprite icon, string author, string associatedSpriteID, string description,
        //     DateTime creationDateTime)
        // {
        //     AssetValidation.ValidateTitle(title);
        //     AssetValidation.ValidateDescription(description);
        //
        //     this.id = id;
        //     InitBase(title, icon, author, creationDateTime);
        //     
        //     this.associatedSpriteID = associatedSpriteID;
        //
        //     this.description = description;
        //     this.palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete);
        //     this.sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete);
        //     this.weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete);
        //     this.projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete);
        //     this.enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete);
        //     this.rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete);
        //     this.tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete);
        // }
        //
        // public PackAsset(IList<PaletteAsset> palettes, IList<SpriteAsset> sprites,
        //     IList<WeaponAsset> weapons, IList<ProjectileAsset> projectiles, IList<EnemyAsset> enemies,
        //     IList<RoomAsset> rooms, IList<TileAsset> tiles)
        // {
        //     InitBase(EditorDefaults.Instance.PackTitle, EditorDefaults.Instance.PackIcon, EditorDefaults.Instance.Author, DateTime.Now);
        //     GenerateID();
        //
        //     this.description = EditorDefaults.Instance.PackDescription;
        //     this.palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, palettes);
        //     this.sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, sprites);
        //     this.weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete, weapons);
        //     this.projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete, projectiles);
        //     this.enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete, enemies);
        //     this.rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, rooms);
        //     this.tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, tiles);
        // }
        //
        // public PackAsset(string id, string title, Sprite icon, string author, string associatedSpriteID, string description,
        //     DateTime creationDateTime, IList<PaletteAsset> palettes, IList<SpriteAsset> sprites,
        //     IList<WeaponAsset> weapons, IList<ProjectileAsset> projectiles, IList<EnemyAsset> enemies,
        //     IList<RoomAsset> rooms, IList<TileAsset> tiles)
        // {
        //     AssetValidation.ValidateTitle(title);
        //     AssetValidation.ValidateDescription(description);
        //
        //     this.id = id;
        //     InitBase(title, icon, author, creationDateTime);
        //
        //     this.associatedSpriteID = associatedSpriteID;
        //     
        //     this.description = description;
        //     this.palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, palettes);
        //     this.sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, sprites);
        //     this.weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete, weapons);
        //     this.projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete, projectiles);
        //     this.enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete, enemies);
        //     this.rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, rooms);
        //     this.tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, tiles);
        // }

        #endregion

        #region Update Values
        public void UpdateDescription(string newDescription) => description = newDescription;
        #endregion

        public override bool Equals(object obj)
        {
            PackAsset pack = (PackAsset) obj;
            if (pack == null) return false;
            if (pack.Title == Title &&
                pack.Author == Author &&
                pack.CreationDate == CreationDate)
                return true;
            return false;
        }

        public override int GetHashCode() => int.Parse(ID);

        public override void ClearAssociatedSprite()
        {
            base.ClearAssociatedSprite();
            icon = EditorDefaults.Instance.PackIcon;
        }

        /// <summary>
        /// Try getting a sprite from the pack.
        /// <br/> If id is an empty asset, returns an <see cref="EmptyAsset"/> with a <see cref="defaultSprite"/>.
        /// <br/> If a sprite with the given returns an <see cref="EmptyAsset"/> with a missing sprite.
        /// </summary>
        /// <param name="id">The ID of the sprite to search for.</param>
        /// <param name="defaultSprite">What sprite to use as default.</param>
        public IAsset TryGetSprite(string id, Sprite defaultSprite)
        {
            if (id == EditorDefaults.EmptyAssetID || string.IsNullOrEmpty(id)) return new EmptyAsset(defaultSprite);
            return Sprites.FindValueFirst(id) ?? (IAsset) new EmptyAsset(EditorDefaults.Instance.MissingSprite);
        }

        public bool ContainsAnyPalettes => (palettes?.Count > 0);
        public bool ContainsAnySprites => (sprites?.Count > 0);
        public bool ContainsAnyWeapons => (weapons?.Count > 0);
        public bool ContainsAnyProjectiles => (projectiles?.Count > 0);
        public bool ContainsAnyEnemies => (enemies?.Count > 0);
        public bool ContainsAnyRooms => (rooms?.Count > 0);
        public bool ContainsAnyTiles => (tiles?.Count > 0);

        public string Description { get => description; }
        public AssetList<PaletteAsset> Palettes { get => palettes; }
        public AssetList<SpriteAsset> Sprites { get => sprites; }
        public AssetList<WeaponAsset> Weapons { get => weapons; }
        public AssetList<ProjectileAsset> Projectiles { get => projectiles; }
        public AssetList<EnemyAsset> Enemies { get => enemies; }
        public AssetList<RoomAsset> Rooms { get => rooms; }
        public AssetList<TileAsset> Tiles { get => tiles; }
        
        public class Builder : AssetWithReferencedSpriteBuilder<PackAsset, Builder>
        {
            private readonly IExternalStorageOverseer ex = ExternalCommunicator.Instance;
            public Builder()
            {
                Asset.title = EditorDefaults.Instance.PackTitle;
                Asset.icon = EditorDefaults.Instance.PackIcon;
                Asset.author = EditorDefaults.Instance.Author;
                Asset.creationDate = DateTime.Now;
                Asset.GenerateID();
                
                Asset.description = EditorDefaults.Instance.PackDescription;
                Asset.palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete);
                Asset.sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete);
                Asset.weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete);
                Asset.projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete);
                Asset.enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete);
                Asset.rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete);
                Asset.tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete);
            }

            public Builder WithDescription(string description)
            {
                Asset.description = description;
                return This;
            }

            public Builder WithPalettes(IList<PaletteAsset> palettes)
            {
                Asset.palettes.Clear();
                Asset.palettes.AddAllWithoutSave(palettes);
                return This;
            }

            public Builder WithSprites(IList<SpriteAsset> sprites)
            {
                Asset.sprites.Clear();
                Asset.sprites.AddAllWithoutSave(sprites);
                return This;
            }

            public Builder WithWeapons(IList<WeaponAsset> weapons)
            {
                Asset.weapons.Clear();
                Asset.weapons.AddAllWithoutSave(weapons);
                return This;
            }

            public Builder WithProjectiles(IList<ProjectileAsset> projectiles)
            {
                Asset.projectiles.Clear();
                Asset.projectiles.AddAllWithoutSave(projectiles);
                return This;
            }

            public Builder WithEnemies(IList<EnemyAsset> enemies)
            {
                Asset.enemies.Clear();
                Asset.enemies.AddAllWithoutSave(enemies);
                return This;
            }

            public Builder WithRooms(IList<RoomAsset> rooms)
            {
                Asset.rooms.Clear();
                Asset.rooms.AddAllWithoutSave(rooms);
                return This;
            }

            public Builder WithTiles(IList<TileAsset> tiles)
            {
                Asset.tiles.Clear();
                Asset.tiles.AddAllWithoutSave(tiles);
                return This;
            }
            
            public override Builder AsClone(PackAsset asset)
            {
                AsCopy(asset);
                Asset.GenerateID();
                return This;
            }

            public override Builder AsCopy(PackAsset asset)
            {
                Asset.id = asset.ID;
                Asset.title = asset.Title;
                Asset.icon = asset.Icon;
                Asset.author = asset.Author;
                Asset.creationDate = asset.CreationDate;
                Asset.associatedSpriteID = asset.AssociatedSpriteID;
                Asset.description = asset.Description;
                Asset.palettes.AddAllWithoutSave(asset.Palettes);
                Asset.sprites.AddAllWithoutSave(asset.Sprites);
                Asset.weapons.AddAllWithoutSave(asset.Weapons);
                Asset.projectiles.AddAllWithoutSave(asset.Projectiles);
                Asset.enemies.AddAllWithoutSave(asset.Enemies);
                Asset.rooms.AddAllWithoutSave(asset.Rooms);
                Asset.tiles.AddAllWithoutSave(asset.Tiles);
                return This;
            }

            protected sealed override PackAsset Asset { get; } = new();

        }
    }
}