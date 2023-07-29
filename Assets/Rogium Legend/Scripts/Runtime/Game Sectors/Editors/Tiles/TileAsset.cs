using System;
using RedRats.Core;
using UnityEngine;
using UnityEngine.Tilemaps;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Sprites;
using Rogium.Systems.Validation;

namespace Rogium.Editors.Tiles
{
    /// <summary>
    /// Contains all data needed for a tile in a pack.
    /// </summary>
    public class TileAsset : AssetWithReferencedSpriteBase
    {
        private readonly TileObject tile;

        #region Constructors
        public TileAsset()
        {
            this.title = EditorConstants.TileTitle;
            this.icon = EditorConstants.TileIcon;
            this.author = EditorConstants.Author;
            this.creationDate = DateTime.Now;
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), TileType.Wall);
            this.tile.Tile.sprite = this.icon;
            GenerateID(EditorAssetIDs.TileIdentifier);
        }
        public TileAsset(TileAsset asset)
        {
            AssetValidation.ValidateTitle(asset.title);
            
            this.id = asset.ID;
            this.icon = asset.Icon;
            this.title = asset.Title;
            this.author = asset.Author;
            this.creationDate = asset.CreationDate;

            this.associatedSpriteID = asset.AssociatedSpriteID;
            
            this.tile = new TileObject(asset.Tile, asset.Type);
            this.tile.Tile.sprite = this.icon;
        }
        public TileAsset(string title, Sprite icon, string author, TileType type)
        {
            AssetValidation.ValidateTitle(title);
            
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), type);
            this.tile.Tile.sprite = icon;
            GenerateID(EditorAssetIDs.TileIdentifier);
        }
        public TileAsset(string id, string title, Sprite icon, string author, string associatedSpriteID, TileType type,
                         Color tileColor, DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            
            this.associatedSpriteID = associatedSpriteID;
            
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), type);
            this.tile.Tile.sprite = icon;
            this.tile.Tile.color = tileColor;
        }
        #endregion

        #region Update Values
        public override void UpdateIcon(IAsset newSprite)
        {
            base.UpdateIcon(newSprite);
            SafetyNet.EnsureIsNotNull(tile, "TileObject");
            SafetyNet.EnsureIsNotNull(tile.Tile, "Tile in TileObject");
            tile.Tile.sprite = newSprite.Icon;
        }

        public void UpdateTileType(int newType)
        {
            tile.UpdateType((TileType) newType);
        }
        #endregion

        public Tile Tile { get => tile.Tile; }
        public TileType Type { get => tile.Type;}
    }
}