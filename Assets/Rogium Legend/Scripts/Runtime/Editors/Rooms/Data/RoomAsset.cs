using System;
using UnityEngine;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Global.GridSystem;

namespace Rogium.Editors.RoomData
{
    /// <summary>
    /// Contains all data needed for a pack room.
    /// </summary>
    public class RoomAsset : AssetBase
    {
        public static readonly Vector2Int gridSize = new Vector2Int(20, 15);
        
        private int difficultyLevel;
        private ObjectGrid<string> tileGrid;

        #region Constructors
        public RoomAsset()
        {
            this.title = EditorDefaults.RoomTitle;
            this.icon = EditorDefaults.RoomIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            this.difficultyLevel = EditorDefaults.RoomDifficulty;
            this.tileGrid = new ObjectGrid<string>(gridSize.x, gridSize.y, () => EditorDefaults.DefaultTileID);
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(RoomAsset roomAsset)
        {
            this.id = roomAsset.id;
            this.title = roomAsset.Title;
            this.icon = roomAsset.Icon;
            this.author = roomAsset.author;
            this.creationDate = roomAsset.CreationDate;
            this.difficultyLevel = roomAsset.DifficultyLevel;
            this.tileGrid = roomAsset.TileGrid;
            Debug.Log(id);
        }
        public RoomAsset(string roomName, Sprite roomIcon, string author, int difficultyLevel)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = roomName;
            this.icon = roomIcon;
            this.author = author;
            this.creationDate = DateTime.Now;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = new ObjectGrid<string>(gridSize.x, gridSize.y, () => EditorDefaults.DefaultTileID);
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(string roomName, Sprite icon, string author, int difficultyLevel, ObjectGrid<string> tileGrid)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = roomName;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = tileGrid;
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(string id, string title, Sprite icon, string author, int difficultyLevel, ObjectGrid<string> tileGrid, DateTime creationDate)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = tileGrid;
        }
        #endregion

        
        #region Update Values
        public void UpdateDifficultyLevel(int newLevel)
        {
            this.difficultyLevel = newLevel;
        }

        #endregion

        public int DifficultyLevel { get => difficultyLevel; }
        public ObjectGrid<string> TileGrid { get => tileGrid; }

        

    }
}