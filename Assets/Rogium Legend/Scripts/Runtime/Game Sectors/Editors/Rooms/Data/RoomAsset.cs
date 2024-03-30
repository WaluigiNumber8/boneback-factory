using System;
using UnityEngine;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.GridSystem;
using Rogium.Systems.Validation;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Contains all data needed for a pack room.
    /// </summary>
    public class RoomAsset : AssetWithDirectSpriteBase
    {
        private int difficultyLevel;
        private RoomType type;
        private int lightness;
        private Color lightnessColor;
        private readonly ObjectGrid<AssetData> tileGrid;
        private readonly ObjectGrid<AssetData> decorGrid;
        private readonly ObjectGrid<AssetData> objectGrid;
        private readonly ObjectGrid<AssetData> enemyGrid;

        #region Constructors
        public RoomAsset()
        {
            this.title = EditorConstants.RoomTitle;
            this.icon = EditorConstants.RoomIcon;
            this.author = EditorConstants.Author;
            this.creationDate = DateTime.Now;
            
            this.difficultyLevel = EditorConstants.RoomDifficulty;
            this.type = EditorConstants.RoomType;
            this.lightness = EditorConstants.RoomLightness;
            this.lightnessColor = EditorConstants.RoomLightnessColor;
            this.tileGrid = new ObjectGrid<AssetData>(EditorConstants.RoomSize.x, EditorConstants.RoomSize.y, () => new AssetData(ParameterInfoConstants.ForTile));
            this.decorGrid = new ObjectGrid<AssetData>(EditorConstants.RoomSize.x, EditorConstants.RoomSize.y, () => new AssetData(ParameterInfoConstants.ForDecor));
            this.objectGrid = new ObjectGrid<AssetData>(EditorConstants.RoomSize.x, EditorConstants.RoomSize.y, () => new AssetData(ParameterInfoConstants.ForEmpty));
            this.enemyGrid = new ObjectGrid<AssetData>(EditorConstants.RoomSize.x, EditorConstants.RoomSize.y, () => new AssetData(ParameterInfoConstants.ForEnemy));
            
            GenerateID(EditorAssetIDs.RoomIdentifier);
        }
        public RoomAsset(RoomAsset asset)
        {
            AssetValidation.ValidateTitle(asset.title);
            
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = asset.Icon;
            this.author = asset.Author;
            this.creationDate = asset.CreationDate;
            
            this.difficultyLevel = asset.DifficultyLevel;
            this.type = asset.Type;
            this.lightness = asset.Lightness;
            this.lightnessColor = asset.LightnessColor;
            
            this.tileGrid = new ObjectGrid<AssetData>(asset.TileGrid);
            this.decorGrid = new ObjectGrid<AssetData>(asset.DecorGrid);
            this.objectGrid = new ObjectGrid<AssetData>(asset.ObjectGrid);
            this.enemyGrid = new ObjectGrid<AssetData>(asset.EnemyGrid);
        }
        
        public RoomAsset(string id, string title, Sprite icon, string author, int difficultyLevel, RoomType type, int lightness,
                         Color lightnessColor, ObjectGrid<AssetData> tileGrid, ObjectGrid<AssetData> decorGrid, 
                         ObjectGrid<AssetData> objectGrid, ObjectGrid<AssetData> enemyGrid, DateTime creationDate)
        {
            AssetValidation.ValidateTitle(title);
            SafetyNet.EnsureIntIsBiggerOrEqualTo(difficultyLevel, 0, "New Room Difficulty Level");

            this.id = id;
            this.title = title;
            this.icon = icon;
            this.author = author;
            this.creationDate = creationDate;
            
            this.difficultyLevel = difficultyLevel;
            this.type = type;
            this.lightness = lightness;
            this.lightnessColor = lightnessColor;
            
            this.tileGrid = new ObjectGrid<AssetData>(tileGrid);
            this.decorGrid = new ObjectGrid<AssetData>(decorGrid);
            this.objectGrid = new ObjectGrid<AssetData>(objectGrid);
            this.enemyGrid = new ObjectGrid<AssetData>(enemyGrid);
        }
        #endregion

        #region Update Values
        public void UpdateDifficultyLevel(int newLevel)
        {
            newLevel = Mathf.Clamp(newLevel, 0, 10);
            difficultyLevel = newLevel;
        }

        public void UpdateType(int newType)
        {
            UpdateType((RoomType) newType);
        }

        public void UpdateType(RoomType newType) => type = newType;

        public void UpdateLightness(int newLightness)
        {
            SafetyNet.EnsureIntIsInRange(newLightness, 0, 255, "Room Lightness");
            lightness = newLightness;
        }
        
        public void UpdateLightnessColor(Color newColor) => lightnessColor = newColor;

        #endregion

        public int DifficultyLevel { get => difficultyLevel; }
        public RoomType Type {get => type;}
        public int Lightness { get => lightness; }
        public Color LightnessColor { get => lightnessColor; }
        public ObjectGrid<AssetData> TileGrid { get => tileGrid; }
        public ObjectGrid<AssetData> DecorGrid { get => decorGrid; }
        public ObjectGrid<AssetData> ObjectGrid { get => objectGrid; }
        public ObjectGrid<AssetData> EnemyGrid { get => enemyGrid; }

    }
}