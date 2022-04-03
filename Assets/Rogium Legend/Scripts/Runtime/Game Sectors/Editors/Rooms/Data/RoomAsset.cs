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
        private int lightness;
        private readonly ObjectGrid<AssetData> tileGrid;
        private readonly ObjectGrid<AssetData> objectGrid;
        private readonly ObjectGrid<AssetData> enemyGrid;

        #region Constructors
        public RoomAsset()
        {
            this.title = EditorDefaults.RoomTitle;
            this.icon = EditorDefaults.RoomIcon;
            this.author = EditorDefaults.Author;
            this.creationDate = DateTime.Now;
            
            this.difficultyLevel = EditorDefaults.RoomDifficulty;
            this.type = EditorDefaults.RoomType;
            this.lightness = EditorDefaults.RoomLightness;
            this.tileGrid = new ObjectGrid<AssetData>(EditorDefaults.RoomSize.x, EditorDefaults.RoomSize.y, () => new AssetData(ParameterDefaults.ParamsTile));
            this.objectGrid = new ObjectGrid<AssetData>(EditorDefaults.RoomSize.x, EditorDefaults.RoomSize.y, () => new AssetData(ParameterDefaults.ParamsEmpty));
            this.enemyGrid = new ObjectGrid<AssetData>(EditorDefaults.RoomSize.x, EditorDefaults.RoomSize.y, () => new AssetData(ParameterDefaults.ParamsEnemy));
            
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
            this.lightness = asset.Lightness;
            this.tileGrid = new ObjectGrid<AssetData>(asset.TileGrid);
            this.objectGrid = new ObjectGrid<AssetData>(asset.ObjectGrid);
            this.enemyGrid = new ObjectGrid<AssetData>(asset.EnemyGrid);
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
            this.lightness = EditorDefaults.RoomLightness;
            this.tileGrid = new ObjectGrid<AssetData>(EditorDefaults.RoomSize.x, EditorDefaults.RoomSize.y, () => new AssetData(ParameterDefaults.ParamsTile));
            this.objectGrid = new ObjectGrid<AssetData>(EditorDefaults.RoomSize.x, EditorDefaults.RoomSize.y, () => new AssetData(ParameterDefaults.ParamsEmpty));
            this.enemyGrid = new ObjectGrid<AssetData>(EditorDefaults.RoomSize.x, EditorDefaults.RoomSize.y, () => new AssetData(ParameterDefaults.ParamsEnemy));
            
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(string title, Sprite icon, string author, int difficultyLevel, RoomType type, int lightness,
                         ObjectGrid<AssetData> tileGrid, ObjectGrid<AssetData> objectGrid, ObjectGrid<AssetData> enemyGrid)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = DateTime.Now;
            
            this.difficultyLevel = difficultyLevel;
            this.type = type;
            this.lightness = lightness;
            this.tileGrid = new ObjectGrid<AssetData>(tileGrid);
            this.objectGrid = new ObjectGrid<AssetData>(objectGrid);
            this.enemyGrid = new ObjectGrid<AssetData>(enemyGrid);
            
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(string id, string title, Sprite icon, string author, int difficultyLevel, RoomType type, int lightness,
                         ObjectGrid<AssetData> tileGrid, ObjectGrid<AssetData> objectGrid, ObjectGrid<AssetData> enemyGrid,
                         DateTime creationDate)
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(difficultyLevel, 0, "New Room Difficulty Level");

            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            
            this.difficultyLevel = difficultyLevel;
            this.type = type;
            this.lightness = lightness;
            this.tileGrid = new ObjectGrid<AssetData>(tileGrid);
            this.objectGrid = new ObjectGrid<AssetData>(objectGrid);
            this.enemyGrid = new ObjectGrid<AssetData>(enemyGrid);
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

        public void UpdateLightness(int newLightness)
        {
            SafetyNet.EnsureIntIsInRange(newLightness, 0, 255, "Room Lightness");
            lightness = newLightness;
        }
        #endregion

        public int DifficultyLevel { get => difficultyLevel; }
        public RoomType Type {get => type;}
        public int Lightness { get => lightness; }
        public ObjectGrid<AssetData> TileGrid { get => tileGrid; }
        public ObjectGrid<AssetData> ObjectGrid { get => objectGrid; }
        public ObjectGrid<AssetData> EnemyGrid { get => enemyGrid; }

        

    }
}