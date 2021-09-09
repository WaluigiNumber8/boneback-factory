using BoubakProductions.Safety;
using RogiumLegend.Editors.Core;
using RogiumLegend.Editors.TileData;
using RogiumLegend.Global.GridSystem;
using System;
using UnityEngine;

namespace RogiumLegend.Editors.RoomData
{
    /// <summary>
    /// Contains all data needed for a pack room.
    /// </summary>
    public class RoomAsset : IAsset
    {
        public static readonly Vector2Int gridSize = new Vector2Int(20, 15);

        private string title;
        private Sprite icon;
        private string author;
        private DateTime creationDate;
        private int difficultyLevel;
        private ObjectGrid<TileAsset> tileGrid;

        //TODO - Add Empty Room Icon

        #region Constructors
        public RoomAsset()
        {
            this.title = "New Room";
            this.icon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));
            this.author = "NO_AUHTOR";
            this.creationDate = DateTime.Now;
            this.difficultyLevel = 1;
            this.tileGrid = new ObjectGrid<TileAsset>(gridSize.x, gridSize.y, () => new TileAsset());
        }
        public RoomAsset(string roomName, string author, int difficultyLevel)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = roomName;
            this.icon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));
            this.author = author;
            this.creationDate = DateTime.Now;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = new ObjectGrid<TileAsset>(gridSize.x, gridSize.y, () => new TileAsset());
        }
        public RoomAsset(string roomName, Sprite icon, string author, int difficultyLevel, ObjectGrid<TileAsset> tileGrid)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = roomName;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = new ObjectGrid<TileAsset>(gridSize.x, gridSize.y, () => new TileAsset());
        }
        public RoomAsset(string title, Sprite icon, string author, int difficultyLevel, ObjectGrid<TileAsset> tileGrid, DateTime creationDate)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = tileGrid;
        }
        #endregion

        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public string Author { get => author; }
        public DateTime CreationDate { get => creationDate; }

        public int DifficultyLevel { get => difficultyLevel; }
        public ObjectGrid<TileAsset> TileGrid { get => tileGrid; }
    }
}