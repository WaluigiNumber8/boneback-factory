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
        private readonly AssetList<PaletteAsset> palettes;
        private readonly AssetList<SpriteAsset> sprites;
        private readonly AssetList<WeaponAsset> weapons;
        private readonly AssetList<ProjectileAsset> projectiles;
        private readonly AssetList<EnemyAsset> enemies;
        private readonly AssetList<RoomAsset> rooms;
        private readonly AssetList<TileAsset> tiles;

        private readonly IExternalStorageOverseer ex = ExternalCommunicator.Instance;

        #region Constructors

        public PackAsset()
        {
            InitBase(EditorDefaults.Instance.PackTitle, EditorDefaults.Instance.PackIcon, EditorDefaults.Instance.Author, DateTime.Now);
            GenerateID(EditorAssetIDs.PackIdentifier);

            description = EditorDefaults.Instance.PackDescription;
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
            AssetValidation.ValidateTitle(asset.title);
            AssetValidation.ValidateDescription(asset.description);

            id = asset.ID;
            InitBase(asset.Title, asset.Icon, asset.Author, asset.CreationDate);

            associatedSpriteID = asset.AssociatedSpriteID;
            
            description = asset.Description;
            palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, asset.Palettes);
            sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, asset.Sprites);
            weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete, asset.Weapons);
            projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete, asset.Projectiles);
            enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete, asset.Enemies);
            rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, asset.Rooms);
            tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, asset.Tiles);
        }

        public PackAsset(string id, string title, Sprite icon, string author, string associatedSpriteID, string description,
            DateTime creationDateTime)
        {
            AssetValidation.ValidateTitle(title);
            AssetValidation.ValidateDescription(description);

            this.id = id;
            InitBase(title, icon, author, creationDateTime);
            
            this.associatedSpriteID = associatedSpriteID;

            this.description = description;
            this.palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete);
            this.sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete);
            this.weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete);
            this.projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete);
            this.enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete);
            this.rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete);
            this.tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete);
        }

        public PackAsset(IList<PaletteAsset> palettes, IList<SpriteAsset> sprites,
            IList<WeaponAsset> weapons, IList<ProjectileAsset> projectiles, IList<EnemyAsset> enemies,
            IList<RoomAsset> rooms, IList<TileAsset> tiles)
        {
            InitBase(EditorDefaults.Instance.PackTitle, EditorDefaults.Instance.PackIcon, EditorDefaults.Instance.Author, DateTime.Now);
            GenerateID(EditorAssetIDs.PackIdentifier);

            this.description = EditorDefaults.Instance.PackDescription;
            this.palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, palettes);
            this.sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, sprites);
            this.weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete, weapons);
            this.projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete, projectiles);
            this.enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete, enemies);
            this.rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, rooms);
            this.tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, tiles);
        }

        public PackAsset(string id, string title, Sprite icon, string author, string associatedSpriteID, string description,
            DateTime creationDateTime, IList<PaletteAsset> palettes, IList<SpriteAsset> sprites,
            IList<WeaponAsset> weapons, IList<ProjectileAsset> projectiles, IList<EnemyAsset> enemies,
            IList<RoomAsset> rooms, IList<TileAsset> tiles)
        {
            AssetValidation.ValidateTitle(title);
            AssetValidation.ValidateDescription(description);

            this.id = id;
            InitBase(title, icon, author, creationDateTime);

            this.associatedSpriteID = associatedSpriteID;
            
            this.description = description;
            this.palettes = new AssetList<PaletteAsset>(ex.Palettes.Save, ex.Palettes.UpdateTitle, ex.Palettes.Delete, palettes);
            this.sprites = new AssetList<SpriteAsset>(ex.Sprites.Save, ex.Sprites.UpdateTitle, ex.Sprites.Delete, sprites);
            this.weapons = new AssetList<WeaponAsset>(ex.Weapons.Save, ex.Weapons.UpdateTitle, ex.Weapons.Delete, weapons);
            this.projectiles = new AssetList<ProjectileAsset>(ex.Projectiles.Save, ex.Projectiles.UpdateTitle, ex.Projectiles.Delete, projectiles);
            this.enemies = new AssetList<EnemyAsset>(ex.Enemies.Save, ex.Enemies.UpdateTitle, ex.Enemies.Delete, enemies);
            this.rooms = new AssetList<RoomAsset>(ex.Rooms.Save, ex.Rooms.UpdateTitle, ex.Rooms.Delete, rooms);
            this.tiles = new AssetList<TileAsset>(ex.Tiles.Save, ex.Tiles.UpdateTitle, ex.Tiles.Delete, tiles);
        }

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
    }
}