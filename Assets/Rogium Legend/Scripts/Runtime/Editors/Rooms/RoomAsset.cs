using System;
using UnityEngine;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.TileData;
using Rogium.Global.GridSystem;

namespace Rogium.Editors.RoomData
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

        #region Constructors
        public RoomAsset()
        {
            this.title = EditorDefaults.roomTitle;
            this.icon = EditorDefaults.roomSprite;
            this.author = EditorDefaults.author;
            this.creationDate = DateTime.Now;
            this.difficultyLevel = 1;
            this.tileGrid = new ObjectGrid<TileAsset>(gridSize.x, gridSize.y, () => new TileAsset());
        }
        public RoomAsset(string roomName, string author, int difficultyLevel)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = roomName;
            this.icon = EditorDefaults.roomSprite;
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

        #region Update Values

        public void UpdateTitle(string newTitle)
        {
            this.title = newTitle;
        }

        public void UpdateIcon(Sprite newIcon)
        {
            this.icon = newIcon;
        }

        public void UpdateAuthor(string newAuthor)
        {
            this.author = newAuthor;
        }

        public void UpdateCreationDate(DateTime newCreationDate)
        {
            this.creationDate = newCreationDate;
        }

        public void UpdateDifficultyLevel(int newLevel)
        {
            this.difficultyLevel = newLevel;
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