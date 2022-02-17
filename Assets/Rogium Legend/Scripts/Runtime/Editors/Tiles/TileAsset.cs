using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;

namespace Rogium.Editors.Tiles
{
    /// <summary>
    /// Contains all data needed for a tile in a pack.
    /// </summary>
    public class TileAsset : AssetBase
    {
        private TileObject tile;

        #region Constructors
        public TileAsset()
        {
            this.title = EditorDefaults.TileTitle;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), TileType.Wall);
            this.tile.Tile.sprite = EditorDefaults.TileIcon;
            GenerateID(EditorAssetIDs.TileIdentifier);
        }
        public TileAsset(TileAsset tileAsset)
        {
            this.id = tileAsset.ID;
            this.title = tileAsset.Title;
            this.author = tileAsset.Author;
            this.creationDate = tileAsset.CreationDate;
            this.tile = new TileObject(tileAsset.Tile, tileAsset.Type);
            this.tile.Tile.sprite = tileAsset.Tile.sprite;
        }
        public TileAsset(string title, Sprite icon, string author, TileType type)
        {
            this.title = title;
            this.author = author;
            this.creationDate = DateTime.Now;
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), type);
            this.tile.Tile.sprite = icon;
            GenerateID(EditorAssetIDs.TileIdentifier);
        }
        public TileAsset(string id, string title, Sprite icon, string author, TileType type, Color tileColor, DateTime creationDate)
        {
            this.id = id;
            this.title = title;
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
            tile.Tile.sprite = newIcon;
        }

        public void UpdateTileType(int newType)
        {
            tile.UpdateType((TileType) newType);
        }
        #endregion

        public override Sprite Icon { get => tile.Tile.sprite; }
        public Tile Tile { get => tile.Tile; }
        public TileType Type { get => tile.Type;}
    }
}