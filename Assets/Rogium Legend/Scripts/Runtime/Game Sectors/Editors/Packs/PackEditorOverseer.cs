using System;
using UnityEngine;
using RedRats.Safety;
using Rogium.Editors.Enemies;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;

namespace Rogium.Editors.Packs
{
    /// <summary>
    /// Overseers the work on a given pack.
    /// </summary>
    public class PackEditorOverseer : IEditorOverseer
    {
        public event Action<PackAsset, string, string> OnSaveChanges;

        private readonly PaletteEditorOverseer paletteEditor;
        private readonly SpriteEditorOverseer spriteEditor;
        private readonly WeaponEditorOverseer weaponEditor;
        private readonly ProjectileEditorOverseer projectileEditor;
        private readonly EnemyEditorOverseer enemyEditor;
        private readonly RoomEditorOverseer roomEditor;
        private readonly TileEditorOverseer tileEditor;

        private PackAsset currentPack;
        private string startingTitle;
        private string startingAuthor;

        #region Singleton Pattern
        private static PackEditorOverseer instance;
        private static readonly object padlock = new object();

        public static PackEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new PackEditorOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private PackEditorOverseer() 
        {
            paletteEditor = PaletteEditorOverseer.Instance;
            spriteEditor = SpriteEditorOverseer.Instance;
            weaponEditor = WeaponEditorOverseer.Instance;
            projectileEditor = ProjectileEditorOverseer.Instance;
            enemyEditor = EnemyEditorOverseer.Instance;
            roomEditor = RoomEditorOverseer.Instance;
            tileEditor = TileEditorOverseer.Instance;

            paletteEditor.OnCompleteEditing += UpdatePalette;
            spriteEditor.OnCompleteEditing += UpdateSprite;
            weaponEditor.OnCompleteEditing += UpdateWeapon;
            projectileEditor.OnCompleteEditing += UpdateProjectile;
            enemyEditor.OnCompleteEditing += UpdateEnemy;
            roomEditor.OnCompleteEditing += UpdateRoom;
            tileEditor.OnCompleteEditing += UpdateTile;
        }

        /// <summary>
        /// Assigns a new pack for editing.
        /// </summary>
        /// <param name="pack">The new pack that will be edited.</param>
        public void AssignAsset(PackAsset pack)
        {
            SafetyNet.EnsureIsNotNull(pack, "Pack to assign");
            currentPack = new PackAsset(pack);
            startingTitle = currentPack.Title;
            startingAuthor = currentPack.Author;
        }

        #region Palettes
        /// <summary>
        /// Creates a new palette, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Palette Asset to Add.</param>
        /// </summary>
        public void CreateNewPalette(PaletteAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - collection Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Palettes, "Pack Editor - Collection of Palettes");
            CurrentPack.Palettes.Add(newAsset);
        }
        public void CreateNewPalette()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Palettes, "Pack Editor - Collection of Palettes");
            CurrentPack.Palettes.Add(new PaletteAsset());
        }

        /// <summary>
        /// Updates the palette in the given pack.
        /// </summary>
        /// <param name="newAsset">Palette Asset with the new details.</param>
        public void UpdatePalette(PaletteAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Palettes, "Collection of Palettes");
            
            CurrentPack.Palettes.Update(newAsset);
        } 

        /// <summary>
        /// Deletes a palette from the pack.
        /// <param name="assetID">The index of the palette to be deleted.</param>
        /// </summary>
        public void RemovePalette(string assetID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Palettes, "Collection of Palettes");
            currentPack.Palettes.Remove(assetID);
        }

        /// <summary>
        /// Send Command to the Palette Editor, to start editing a palette.
        /// </summary>
        /// <param name="assetID">Palette index from the collection.</param>
        /// <param name="prepareEditor"></param>
        public void ActivatePaletteEditor(string assetID, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Palettes, "Collection of Palettes");
            paletteEditor.AssignAsset(CurrentPack.Palettes[assetID], prepareEditor);
        }

        #endregion
        
        #region Sprites
        /// <summary>
        /// Creates a new sprite, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Sprite Asset to Add.</param>
        /// </summary>
        public void CreateNewSprite(SpriteAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Sprites, "Pack Editor - Collection of Sprites");
            CurrentPack.Sprites.Add(newAsset);
        }
        public void CreateNewSprite()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Sprites, "Pack Editor - Collection of Sprites");
            CurrentPack.Sprites.Add(new SpriteAsset());
        }

        /// <summary>
        /// Updates the sprite in the given pack.
        /// </summary>
        /// <param name="newAsset">Sprite Asset with the new details.</param>
        public void UpdateSprite(SpriteAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Sprites, "Collection of Sprites");
            CurrentPack.Sprites.Update(newAsset);
        } 

        /// <summary>
        /// Deletes a sprite from the pack.
        /// <param name="assetID">The id of the sprite to be deleted.</param>
        /// </summary>
        public void RemoveSprite(string assetID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Sprites, "Collection of Sprites");
            currentPack.Sprites.Remove(assetID);
        }

        /// <summary>
        /// Send Command to the Sprite Editor, to start editing a palette.
        /// </summary>
        /// <param name="assetID">Sprite index from the collection.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateSpriteEditor(string assetID, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Sprites, "Collection of Sprites");
            spriteEditor.AssignAsset(CurrentPack.Sprites[assetID], prepareEditor);
        }

        #endregion
        
        #region Weapons
        /// <summary>
        /// Creates a new weapon, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Weapon Asset to Add.</param>
        /// </summary>
        public void CreateNewWeapon(WeaponAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Weapons, "Pack Editor - Collection of Weapons");
            CurrentPack.Weapons.Add(newAsset);
        }
        public void CreateNewWeapon()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Weapons, "Pack Editor - Collection of Weapons");
            CurrentPack.Weapons.Add(new WeaponAsset());
        }

        /// <summary>
        /// Updates a weapon in the given pack.
        /// </summary>
        /// <param name="newAsset">Weapon Asset with the new details.</param>
        public void UpdateWeapon(WeaponAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Weapons, "Collection of Weapons");
            CurrentPack.Weapons.Update(newAsset);
        } 

        /// <summary>
        /// Deletes a weapon from the pack.
        /// <param name="assetID">The index of the weapon to be deleted.</param>
        /// </summary>
        public void RemoveWeapon(string assetID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Weapons, "Collection of Weapons");
            currentPack.Weapons.Remove(assetID);
        }

        /// <summary>
        /// Send Command to the Weapon Editor, to start editing a weapon.
        /// </summary>
        /// <param name="assetID">Weapon index from the collection.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateWeaponEditor(string assetID, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Weapons, "Collection of Weapons");
            weaponEditor.AssignAsset(CurrentPack.Weapons[assetID], prepareEditor);
        }

        #endregion
        
        #region Projectiles
        /// <summary>
        /// Creates a new projectile, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Projectile Asset to Add.</param>
        /// </summary>
        public void CreateNewProjectile(ProjectileAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Projectiles, "Pack Editor - Collection of Projectiles");
            CurrentPack.Projectiles.Add(newAsset);
        }
        public void CreateNewProjectile()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Projectiles, "Pack Editor - Collection of Projectiles");
            CurrentPack.Projectiles.Add(new ProjectileAsset());
        }

        /// <summary>
        /// Updates the projectile in the given pack.
        /// </summary>
        /// <param name="newAsset">Projectile Asset with the new details.</param>
        public void UpdateProjectile(ProjectileAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Projectiles, "Collection of Projectiles");
            CurrentPack.Projectiles.Update(newAsset);
        } 

        /// <summary>
        /// Deletes a projectile from the pack.
        /// <param name="assetID">The id of the projectile to be deleted.</param>
        /// </summary>
        public void RemoveProjectile(string assetID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Projectiles, "Collection of Projectiles");
            currentPack.Projectiles.Remove(assetID);
        }

        /// <summary>
        /// Send Command to the Projectile Editor, to start editing a tile.
        /// </summary>
        /// <param name="assetID">Projectile id from the collection.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateProjectileEditor(string assetID, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Projectiles, "Collection of Projectiles");
            projectileEditor.AssignAsset(CurrentPack.Projectiles[assetID], prepareEditor);
        }

        #endregion
        
        #region Enemies
        /// <summary>
        /// Creates a new enemy, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Enemy Asset to Add.</param>
        /// </summary>
        public void CreateNewEnemy(EnemyAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Enemies, "Pack Editor - Collection of Enemies");
            CurrentPack.Enemies.Add(newAsset);
        }
        public void CreateNewEnemy()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Enemies, "Pack Editor - Collection of Enemies");
            CurrentPack.Enemies.Add(new EnemyAsset());
        }

        /// <summary>
        /// Updates the enemy in the given pack.
        /// </summary>
        /// <param name="newAsset">Enemy Asset with the new details.</param>
        public void UpdateEnemy(EnemyAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Enemies, "Collection of Enemies");
            CurrentPack.Enemies.Update(newAsset);
        } 

        /// <summary>
        /// Deletes a enemy from the pack.
        /// <param name="assetID">The id of the enemy to be deleted.</param>
        /// </summary>
        public void RemoveEnemy(string assetID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Enemies, "Collection of Enemies");
            currentPack.Enemies.Remove(assetID);
        }

        /// <summary>
        /// Send Command to the Enemy Editor, to start editing a enemy.
        /// </summary>
        /// <param name="assetID">Enemy index from the collection.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateEnemyEditor(string assetID, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Enemies, "Collection of Enemies");
            enemyEditor.AssignAsset(CurrentPack.Enemies[assetID], prepareEditor);
        }

        #endregion
        
        #region Tiles
        /// <summary>
        /// Creates a new tile, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Tile Asset to Add.</param>
        /// </summary>
        public void CreateNewTile(TileAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Tiles, "Pack Editor - Collection of Tiles");
            CurrentPack.Tiles.Add(newAsset);
        }
        public void CreateNewTile()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Tiles, "Pack Editor - Collection of Tiles");
            CurrentPack.Tiles.Add(new TileAsset());
        }

        /// <summary>
        /// Updates the tile in the given pack.
        /// </summary>
        /// <param name="newAsset">Tile Asset with the new details.</param>
        public void UpdateTile(TileAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Tiles, "Collection of Tiles");
            CurrentPack.Tiles.Update(newAsset);
        } 

        /// <summary>
        /// Deletes a tile from the pack.
        /// <param name="assetID">The id of the tile to be deleted.</param>
        /// </summary>
        public void RemoveTile(string assetID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Tiles, "Collection Tiles");
            currentPack.Tiles.Remove(assetID);
        }

        /// <summary>
        /// Send Command to the Tile Editor, to start editing a tile.
        /// </summary>
        /// <param name="assetID">Tile id from the collection.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateTileEditor(string assetID, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Tiles, "Collection of Tiles");
            tileEditor.AssignAsset(CurrentPack.Tiles[assetID], prepareEditor);
        }

        #endregion
        
        #region Rooms
        /// <summary>
        /// Creates a new room, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Room Asset to Add.</param>
        /// </summary>
        public void CreateNewRoom(RoomAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Rooms, "Pack Editor - Collection of Rooms");
            CurrentPack.Rooms.Add(newAsset);
        }
        public void CreateNewRoom()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Rooms, "Pack Editor - Collection of Rooms");
            CurrentPack.Rooms.Add(new RoomAsset());
        }

        /// <summary>
        /// Updates the room in the given pack.
        /// </summary>
        /// <param name="newAsset">Room Asset with the new details.</param>
        public void UpdateRoom(RoomAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Rooms, "Collection of Rooms");
            CurrentPack.Rooms.Update(newAsset);
        } 

        /// <summary>
        /// Deletes a room from the pack.
        /// <param name="assetID">The index of the room to be deleted.</param>
        /// </summary>
        public void RemoveRoom(string assetID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Rooms, "Collection of Rooms");
            currentPack.Rooms.Remove(assetID);
        }

        /// <summary>
        /// Send Command to Room Editor, to start editing a room.
        /// </summary>
        /// <param name="assetID">Room index from the collection</param>
        /// <param name="prepareEditor"></param>
        public void ActivateRoomEditor(string assetID, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureCollectionIsNotNullOrEmpty(currentPack.Rooms, "Collection of Rooms");
            roomEditor.AssignAsset(CurrentPack.Rooms[assetID], prepareEditor);
        }

        #endregion

        /// <summary>
        /// Saves all edits done to a pack and "returns" it to the library.
        /// </summary>
        public void CompleteEditing()
        {
            SavePackChanges();
            currentPack = null;
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        private void SavePackChanges()
        {
            OnSaveChanges?.Invoke(currentPack, startingTitle, startingAuthor);
        }

        public PackAsset CurrentPack { get => currentPack; }
    }
}