using System;
using RedRats.Core;
using UnityEngine;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Enemies;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using static Rogium.Editors.Packs.AssetAssociation;

namespace Rogium.Editors.Packs
{
    /// <summary>
    /// Overseers the work on a given pack.
    /// </summary>
    public sealed class PackEditorOverseer : Singleton<PackEditorOverseer>, IEditorOverseer
    {
        public event Action<PackAsset, int, string, string, string> OnSaveChanges;
        public event Action<string> OnRemoveSprite;
        
        private readonly PaletteEditorOverseer paletteEditor;
        private readonly SpriteEditorOverseer spriteEditor;
        private readonly WeaponEditorOverseer weaponEditor;
        private readonly ProjectileEditorOverseer projectileEditor;
        private readonly EnemyEditorOverseer enemyEditor;
        private readonly RoomEditorOverseer roomEditor;
        private readonly TileEditorOverseer tileEditor;

        private PackAsset currentPack;
        private int myIndex;
        private string lastTitle;
        private string lastAuthor;
        private string lastAssociatedSpriteID;

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
        /// <param name="asset">The new pack that will be edited.</param>
        /// <param name="index">The index of the pack in library list.</param>
        public void AssignAsset(PackAsset asset, int index)
        {
            SafetyNet.EnsureIsNotNull(asset, "Pack to assign");
            currentPack = new PackAsset.Builder().AsCopy(asset).Build();
            myIndex = index;
            lastAssociatedSpriteID = asset.AssociatedSpriteID;
            lastTitle = currentPack.Title;
            lastAuthor = currentPack.Author;
        }

        /// <summary>
        /// Updates the asset with new data. Not allowed when no asset is assigned.
        /// </summary>
        /// <param name="updatedAsset">Asset, containing new data.</param>
        public void UpdateAsset(PackAsset updatedAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Currently active asset.");
            currentPack = new PackAsset.Builder().AsCopy(updatedAsset).Build();
        }
        
        #region Palettes
        /// <summary>
        /// Creates a new palette, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Palette Asset to Add.</param>
        /// </summary>
        public void CreateNewPalette(PaletteAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Palettes, "Pack Editor - List of Palettes");
            CurrentPack.Palettes.Add(newAsset);
            CompleteEditing();
        }

        /// <summary>
        /// Updates the palette in the given pack.
        /// </summary>
        /// <param name="newAsset">Palette Asset with the new details.</param>
        /// <param name="positionIndex">Which palette to override.</param>
        public void UpdatePalette(PaletteAsset newAsset, int positionIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Palettes, "List of Palettes");
            RefreshPaletteForOtherAssets(newAsset, currentPack, CompleteEditing);
            CurrentPack.Palettes.Update(positionIndex, newAsset);
        } 

        /// <summary>
        /// Deletes a palette from the pack.
        /// <param name="assetIndex">The index of the palette to be deleted.</param>
        /// </summary>
        public void RemovePalette(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Palettes, "List of Palettes");
            PaletteAsset palette = currentPack.Palettes[assetIndex];
            foreach (string id in palette.AssociatedAssetsIDs)
            {
                RemovePaletteAssociationsAndSaveAsset(currentPack.Sprites, id);
            }
            
            currentPack.Palettes.Remove(assetIndex);
            CompleteEditing();
        }

        /// <summary>
        /// Send Command to the Palette Editor, to start editing a palette.
        /// </summary>
        /// <param name="assetIndex">Palette index from the list.</param>
        /// <param name="prepareEditor"></param>
        public void ActivatePaletteEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Palettes, "List of Palettes");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Palettes.Count, "Palette Index");
            paletteEditor.AssignAsset(CurrentPack.Palettes[assetIndex], assetIndex, prepareEditor);
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
            SafetyNet.EnsureIsNotNull(currentPack.Sprites, "Pack Editor - List of Sprites");
            CurrentPack.Sprites.Add(newAsset);
            CompleteEditing();
        }

        /// <summary>
        /// Updates the sprite in the given pack.
        /// </summary>
        /// <param name="newAsset">Sprite Asset with the new details.</param>
        /// <param name="positionIndex">Which sprite to override.</param>
        /// <param name="lastAssociatedPaletteID">The palette ID associated before editing.</param>
        public void UpdateSprite(SpriteAsset newAsset, int positionIndex, string lastAssociatedPaletteID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Sprites, "List of Sprites");
            
            ProcessPaletteAssociations(currentPack, newAsset, lastAssociatedPaletteID);
            CurrentPack.Sprites.Update(positionIndex, newAsset);
            RefreshSpriteForOtherAssets(newAsset, currentPack, CompleteEditing);
        }

        /// <summary>
        /// Deletes a sprite from the pack.
        /// <param name="assetIndex">The index of the sprite to be deleted.</param>
        /// </summary>
        public void RemoveSprite(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Sprites, "List of Sprites");
            SpriteAsset sprite = currentPack.Sprites[assetIndex];
            RemovePaletteAssociation(currentPack, sprite);
            foreach (string id in sprite.AssociatedAssetsIDs)
            {
                string identifier = id[..2];
                switch (identifier)
                {
                    case EditorAssetIDs.PackIdentifier:
                        currentPack.ClearAssociatedSprite();
                        OnRemoveSprite?.Invoke(currentPack.ID);
                        break;
                    case EditorAssetIDs.WeaponIdentifier:
                        RemoveSpriteAssociationsAndSaveAsset(currentPack.Weapons, id);
                        break;
                    case EditorAssetIDs.ProjectileIdentifier:
                        RemoveSpriteAssociationsAndSaveAsset(currentPack.Projectiles, id);
                        break;
                    case EditorAssetIDs.EnemyIdentifier:
                        RemoveSpriteAssociationsAndSaveAsset(currentPack.Enemies, id);
                        break;
                    case EditorAssetIDs.TileIdentifier:
                        RemoveSpriteAssociationsAndSaveAsset(currentPack.Tiles, id);
                        break;
                }
            }
            currentPack.Sprites.Remove(assetIndex);
            CompleteEditing();
        }

        /// <summary>
        /// Send Command to the Sprite Editor, to start editing a palette.
        /// </summary>
        /// <param name="assetIndex">Sprite index from the list.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateSpriteEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Sprites, "List of Sprites");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Sprites.Count, "Sprite Index");
            spriteEditor.AssignAsset(CurrentPack.Sprites[assetIndex], assetIndex, prepareEditor);
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
            SafetyNet.EnsureIsNotNull(currentPack.Weapons, "Pack Editor - List of Weapons");
            CurrentPack.Weapons.Add(newAsset);
            CompleteEditing();
        }

        /// <summary>
        /// Updates a weapon in the given pack.
        /// </summary>
        /// <param name="newAsset">Weapon Asset with the new details.</param>
        /// <param name="positionIndex">Which weapon to override.</param>
        /// <param name="lastAssociatedSpriteID">The weapon's sprite before it was edited.</param>
        public void UpdateWeapon(WeaponAsset newAsset, int positionIndex, string lastAssociatedSpriteID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Weapons, "List of Weapons");
            ProcessSpriteAssociations(currentPack, newAsset, lastAssociatedSpriteID);
            CurrentPack.Weapons.Update(positionIndex, newAsset);
        } 

        /// <summary>
        /// Deletes a weapon from the pack.
        /// <param name="assetIndex">The index of the weapon to be deleted.</param>
        /// </summary>
        public void RemoveWeapon(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Weapons, "List of Weapons");
            RemoveSpriteAssociation(currentPack, currentPack.Weapons[assetIndex]);
            currentPack.Weapons.Remove(assetIndex);
            CompleteEditing();
        }

        /// <summary>
        /// Send Command to the Weapon Editor, to start editing a weapon.
        /// </summary>
        /// <param name="assetIndex">Weapon index from the list.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateWeaponEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Weapons, "List of Weapons");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Weapons.Count, "Weapon Index");
            weaponEditor.AssignAsset(CurrentPack.Weapons[assetIndex], assetIndex, prepareEditor);
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
            SafetyNet.EnsureIsNotNull(currentPack.Projectiles, "Pack Editor - List of Projectiles");
            CurrentPack.Projectiles.Add(newAsset);
            CompleteEditing();
        }

        /// <summary>
        /// Updates the projectile in the given pack.
        /// </summary>
        /// <param name="newAsset">Projectile Asset with the new details.</param>
        /// <param name="positionIndex">Which projectile to override.</param>
        /// /// <param name="lastAssociatedSpriteID">The projectile's sprite before it was edited.</param>
        public void UpdateProjectile(ProjectileAsset newAsset, int positionIndex, string lastAssociatedSpriteID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Projectiles, "List of Projectiles");
            ProcessSpriteAssociations(currentPack, newAsset, lastAssociatedSpriteID);
            CurrentPack.Projectiles.Update(positionIndex, newAsset);
        } 

        /// <summary>
        /// Deletes a projectile from the pack.
        /// <param name="assetIndex">The index of the projectile to be deleted.</param>
        /// </summary>
        public void RemoveProjectile(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Projectiles, "List of Projectiles");
            RemoveSpriteAssociation(currentPack, currentPack.Projectiles[assetIndex]);
            currentPack.Projectiles.Remove(assetIndex);
            CompleteEditing();
        }

        /// <summary>
        /// Send Command to the Projectile Editor, to start editing a tile.
        /// </summary>
        /// <param name="assetIndex">Projectile index from the list.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateProjectileEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Projectiles, "List of Projectiles");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Projectiles.Count, "Projectile Index");
            projectileEditor.AssignAsset(CurrentPack.Projectiles[assetIndex], assetIndex, prepareEditor);
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
            SafetyNet.EnsureIsNotNull(currentPack.Enemies, "Pack Editor - List of Enemies");
            CurrentPack.Enemies.Add(newAsset);
            CompleteEditing();
        }

        /// <summary>
        /// Updates the enemy in the given pack.
        /// </summary>
        /// <param name="newAsset">Enemy Asset with the new details.</param>
        /// <param name="positionIndex">Which enemy to override.</param>
        /// /// <param name="lastAssociatedSpriteID">The enemy's sprite before it was edited.</param>
        public void UpdateEnemy(EnemyAsset newAsset, int positionIndex, string lastAssociatedSpriteID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Enemies, "List of Enemies");
            ProcessSpriteAssociations(currentPack, newAsset, lastAssociatedSpriteID);
            CurrentPack.Enemies.Update(positionIndex, newAsset);
        } 

        /// <summary>
        /// Deletes a enemy from the pack.
        /// <param name="assetIndex">The index of the enemy to be deleted.</param>
        /// </summary>
        public void RemoveEnemy(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Enemies, "List of Enemies");
            RemoveSpriteAssociation(currentPack, currentPack.Enemies[assetIndex]);
            currentPack.Enemies.Remove(assetIndex);
            CompleteEditing();
        }

        /// <summary>
        /// Send Command to the Enemy Editor, to start editing a enemy.
        /// </summary>
        /// <param name="assetIndex">Enemy index from the list.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateEnemyEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Enemies, "List of Enemies");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Enemies.Count, "Enemy Index");
            enemyEditor.AssignAsset(CurrentPack.Enemies[assetIndex], assetIndex, prepareEditor);
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
            SafetyNet.EnsureIsNotNull(currentPack.Rooms, "Pack Editor - List of Rooms");

            InternalLibraryOverseer library = InternalLibraryOverseer.GetInstance();
            newAsset.ObjectGrid.SetTo(new Vector2Int(6, 5), AssetDataBuilder.ForObject(library.GetObjectByID("001")));
            newAsset.ObjectGrid.SetTo(new Vector2Int(8, 5), AssetDataBuilder.ForObject(library.GetObjectByID("002")));
            CurrentPack.Rooms.Add(newAsset);
            CompleteEditing();
        }

        /// <summary>
        /// Updates the room in the given pack.
        /// </summary>
        /// <param name="newAsset">Room Asset with the new details.</param>
        /// <param name="positionIndex">Which room to override.</param>
        public void UpdateRoom(RoomAsset newAsset, int positionIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Rooms, "List of Rooms");
            CurrentPack.Rooms.Update(positionIndex, newAsset);
        } 

        /// <summary>
        /// Deletes a room from the pack.
        /// <param name="assetIndex">The index of the room to be deleted.</param>
        /// </summary>
        public void RemoveRoom(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Rooms, "List of Rooms");
            currentPack.Rooms.Remove(assetIndex);
            CompleteEditing();
        }

        /// <summary>
        /// Send Command to Room Editor, to start editing a room.
        /// </summary>
        /// <param name="assetIndex">Room index from the list</param>
        /// <param name="prepareEditor"></param>
        public void ActivateRoomEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Rooms, "List of Rooms");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Rooms.Count, "Room Index");
            roomEditor.AssignAsset(CurrentPack.Rooms[assetIndex], assetIndex, prepareEditor);
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
            SafetyNet.EnsureIsNotNull(currentPack.Tiles, "Pack Editor - List of Tiles");
            CurrentPack.Tiles.Add(newAsset);
            CompleteEditing();
        }

        /// <summary>
        /// Updates the tile in the given pack.
        /// </summary>
        /// <param name="newAsset">Tile Asset with the new details.</param>
        /// <param name="positionIndex">Which tile to override.</param>
        /// /// <param name="lastAssociatedSpriteID">The tile's sprite before it was edited.</param>
        public void UpdateTile(TileAsset newAsset, int positionIndex, string lastAssociatedSpriteID)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Tiles, "List of Tiles");
            ProcessSpriteAssociations(currentPack, newAsset, lastAssociatedSpriteID);
            CurrentPack.Tiles.Update(positionIndex, newAsset);
        } 

        /// <summary>
        /// Deletes a tile from the pack.
        /// <param name="assetIndex">The index of the tile to be deleted.</param>
        /// </summary>
        public void RemoveTile(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Tiles, "List of Tiles");
            RemoveSpriteAssociation(currentPack, currentPack.Tiles[assetIndex]);
            currentPack.Tiles.Remove(assetIndex);
            CompleteEditing();
        }

        /// <summary>
        /// Send Command to the Tile Editor, to start editing a tile.
        /// </summary>
        /// <param name="assetIndex">Tile index from the list.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateTileEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Tiles, "List of Tiles");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Tiles.Count, "Tile Index");
            tileEditor.AssignAsset(CurrentPack.Tiles[assetIndex], assetIndex, prepareEditor);
        }

        #endregion
        
        /// <summary>
        /// Save changes.
        /// </summary>
        public void CompleteEditing() => OnSaveChanges?.Invoke(currentPack, myIndex, lastTitle, lastAuthor, lastAssociatedSpriteID);

        public PackAsset CurrentPack { get => currentPack; }
    }
}