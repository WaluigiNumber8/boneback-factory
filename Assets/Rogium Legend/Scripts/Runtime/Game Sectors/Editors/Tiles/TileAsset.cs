using System;
using BoubakProductions.Core;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Validation;

namespace Rogium.Editors.Tiles
{
    /// <summary>
    /// Contains all data needed for a tile in a pack.
    /// </summary>
    public class TileAsset : AssetBase
    {
        private readonly TileObject tile;

        #region Constructors
        public TileAsset()
        {
            this.title = EditorDefaults.TileTitle;
            this.icon = EditorDefaults.TileIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), TileType.Wall);
            this.tile.Tile.sprite = this.icon;
            GenerateID(EditorAssetIDs.TileIdentifier);
        }
        public TileAsset(TileAsset asset)
        {
            this.id = asset.ID;
            this.icon = asset.Icon;
            this.title = asset.Title;
            this.author = asset.Author;
            this.creationDate = asset.CreationDate;
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
        public TileAsset(string id, string title, Sprite icon, string author, TileType type, Color tileColor, DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            
            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), type);
            this.tile.Tile.sprite = icon;
            this.tile.Tile.color = tileColor;
        }
        #endregion

        #region Update Values
        public override void UpdateIcon(Sprite newIcon)
        {
            SafetyNet.EnsureIsNotNull(tile, "TileObject");
            SafetyNet.EnsureIsNotNull(tile.Tile, "Tile in TileObject");
            icon = newIcon;
            tile.Tile.sprite = newIcon;
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