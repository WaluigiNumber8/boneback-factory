using RogiumLegend.Editors.Core;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RogiumLegend.Editors.TileData
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

        public TileAsset()
        {
            this.title = "New Tile";
            this.author = "NO_AUTHOR";
            this.creationDate = DateTime.Now;
            this.tile = new TileObject(ScriptableObject.CreateInstance<Tile>(), TileType.Wall);
            this.tile.Tile.sprite = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));
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

        public string Title { get => title; }
        public Sprite Icon { get => tile.Tile.sprite; }
        public Tile Tile { get => tile.Tile; }
        public TileType Type { get => tile.Type;}
        public string Author { get => author;}
        public DateTime CreationDate { get => creationDate; }
    }
}