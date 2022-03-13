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
        private int difficultyLevel;
        private RoomType type;
        private readonly ObjectGrid<string> tileGrid;
        private readonly ObjectGrid<string> enemyGrid;

        #region Constructors
        public RoomAsset()
        {
            this.title = EditorDefaults.RoomTitle;
            this.icon = EditorDefaults.RoomIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            
            this.difficultyLevel = EditorDefaults.RoomDifficulty;
            this.type = EditorDefaults.RoomType;
            this.tileGrid = new ObjectGrid<string>(EditorDefaults.RoomSize.x, EditorDefaults.RoomSize.y, () => EditorDefaults.EmptyAssetID);
            this.enemyGrid = new ObjectGrid<string>(EditorDefaults.RoomSize.x, EditorDefaults.RoomSize.y, () => EditorDefaults.EmptyAssetID);
            
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(RoomAsset asset)
        {
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = asset.Icon;
            this.author = asset.Author;
            this.creationDate = asset.CreationDate;
            
            this.difficultyLevel = asset.DifficultyLevel;
            this.type = asset.Type;
            this.tileGrid = new ObjectGrid<string>(asset.TileGrid);
            this.enemyGrid = new ObjectGrid<string>(asset.EnemyGrid);
        }
        public RoomAsset(string title, Sprite roomIcon, string author, int difficultyLevel)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = title;
            this.icon = roomIcon;
            this.author = author;
            this.creationDate = DateTime.Now;
            
            this.difficultyLevel = difficultyLevel;
            this.type = EditorDefaults.RoomType;
            this.tileGrid = new ObjectGrid<string>(EditorDefaults.RoomSize.x, EditorDefaults.RoomSize.y, () => EditorDefaults.EmptyAssetID);
            this.enemyGrid = new ObjectGrid<string>(EditorDefaults.RoomSize.x, EditorDefaults.RoomSize.y, () => EditorDefaults.EmptyAssetID);
            
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(string title, Sprite icon, string author, int difficultyLevel, RoomType type, ObjectGrid<string> tileGrid,
                         ObjectGrid<string> enemyGrid)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            
            this.difficultyLevel = difficultyLevel;
            this.type = type;
            this.tileGrid = new ObjectGrid<string>(tileGrid);
            this.enemyGrid = new ObjectGrid<string>(enemyGrid);
            
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(string id, string title, Sprite icon, string author, int difficultyLevel, RoomType type,
                         ObjectGrid<string> tileGrid, ObjectGrid<string> enemyGrid, DateTime creationDate)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(difficultyLevel, 0, "New Room Difficulty Level");

            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            
            this.difficultyLevel = difficultyLevel;
            this.type = type;
            this.tileGrid = new ObjectGrid<string>(tileGrid);
            this.enemyGrid = new ObjectGrid<string>(enemyGrid);
        }
        #endregion

        #region Update Values
        public void UpdateDifficultyLevel(int newLevel)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(newLevel, 0, "New Room Level");
            difficultyLevel = newLevel;
        }

        public void UpdateType(int newType)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(newType, 0, "New Room Type");
            UpdateType((RoomType) newType);
        }

        public void UpdateType(RoomType newType) => type = newType;

        #endregion

        public int DifficultyLevel { get => difficultyLevel; }
        public RoomType Type {get => type;}
        public ObjectGrid<string> TileGrid { get => tileGrid; }
        public ObjectGrid<string> EnemyGrid { get => enemyGrid; }

        

    }
}