using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;

namespace Rogium.Editors.TileData
{
    /// <summary>
    /// Contains all data needed for a tile in a pack.
    /// </summary>
    public class TileAsset : IAsset
    {
        private string title;
        private TileObject tile;
        private string author;
        private DateTime creationDate;

        #region Constructors
        public TileAsset()
        {
            this.title = EditorDefaults.tileTitle;
            this.author = EditorDefaults.author;
            this.creationDate = DateTime.Now;
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), TileType.Wall);
            this.tile.Tile.sprite = EditorDefaults.tileSprite;
        }
        public TileAsset(string title, Sprite icon, string author, TileType type)
        {
            this.title = title;
            this.author = author;
            this.creationDate = DateTime.Now;
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), type);
            this.tile.Tile.sprite = icon;
        }
        public TileAsset(string title, Sprite icon, string author, TileType type, Color tileColor, DateTime creationDate)
        {
            this.title = title;
            this.author = author;
            this.creationDate = creationDate;
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), type);
            this.tile.Tile.sprite = icon;
            this.tile.Tile.color = tileColor;
        }
        #endregion

        #region Update Values

        public void UpdateTitle(string newTitle)
        {
            this.title = newTitle;
        }

        public void UpdateIcon(Sprite newIcon)
        {
            SafetyNet.EnsureIsNotNull(tile, "TileObject");
            SafetyNet.EnsureIsNotNull(tile.Tile, "Tile in TileObject");
            this.tile.Tile.sprite = newIcon;
        }

        public void UpdateAuthor(string newAuthor)
        {
            this.author = newAuthor;
        }

        public void UpdateCreationDate(DateTime newCreationDate)
        {
            this.creationDate = newCreationDate;
        }

        #endregion

        public string Title { get => title; }
        public Sprite Icon { get => tile.Tile.sprite; }
        public Tile Tile { get => tile.Tile; }
        public TileType Type { get => tile.Type;}
        public string Author { get => author;}
        public DateTime CreationDate { get => creationDate; }
    }
}