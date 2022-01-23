using System;
using UnityEngine;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.GridSystem;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Contains all data needed for a pack room.
    /// </summary>
    public class RoomAsset : AssetBase
    {
        public static readonly Vector2Int gridSize = new Vector2Int(20, 15);
        
        private int difficultyLevel;
        private RoomType type;
        private ObjectGrid<string> tileGrid;

        #region Constructors
        public RoomAsset()
        {
            this.title = EditorDefaults.RoomTitle;
            this.icon = EditorDefaults.RoomIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            this.difficultyLevel = EditorDefaults.RoomDifficulty;
            this.type = EditorDefaults.RoomType;
            this.tileGrid = new ObjectGrid<string>(gridSize.x, gridSize.y, () => EditorDefaults.EmptyAssetID);
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(RoomAsset room)
        {
            this.id = room.id;
            this.title = room.Title;
            this.icon = room.Icon;
            this.author = room.author;
            this.creationDate = room.CreationDate;
            this.difficultyLevel = room.DifficultyLevel;
            this.type = room.type;
            this.tileGrid = room.TileGrid;
        }
        public RoomAsset(string roomName, Sprite roomIcon, string author, int difficultyLevel)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = roomName;
            this.icon = roomIcon;
            this.author = author;
            this.creationDate = DateTime.Now;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = new ObjectGrid<string>(gridSize.x, gridSize.y, () => EditorDefaults.EmptyAssetID);
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(string roomName, Sprite icon, string author, int difficultyLevel, RoomType type, ObjectGrid<string> tileGrid)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = roomName;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            this.difficultyLevel = difficultyLevel;
            this.type = type;
            this.tileGrid = tileGrid;
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(string id, string title, Sprite icon, string author, int difficultyLevel, RoomType type, ObjectGrid<string> tileGrid, DateTime creationDate)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(difficultyLevel, 0, "New Room Difficulty Level");

            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            this.difficultyLevel = difficultyLevel;
            this.type = type;
            this.tileGrid = tileGrid;
        }
        #endregion

        
        #region Update Values
        public void UpdateDifficultyLevel(int newLevel)
        {
            this.difficultyLevel = newLevel;
        }

        public void UpdateType(int newType)
        {
            UpdateType((RoomType)newType);
        }
        public void UpdateType(RoomType newType)
        {
            this.type = newType;
        }
        #endregion

        public int DifficultyLevel { get => difficultyLevel; }
        public RoomType Type {get => type;}
        public ObjectGrid<string> TileGrid { get => tileGrid; }

        

    }
}