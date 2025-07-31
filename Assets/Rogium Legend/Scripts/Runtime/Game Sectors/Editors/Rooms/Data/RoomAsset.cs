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
        private Sprite banner;
        private int difficultyLevel;
        private RoomType type;
        private int lightness;
        private Color lightnessColor;
        private ObjectGrid<AssetData> tileGrid;
        private ObjectGrid<AssetData> decorGrid;
        private ObjectGrid<AssetData> objectGrid;
        private ObjectGrid<AssetData> enemyGrid;

        private RoomAsset() { }

        #region Update Values
        public void UpdateBanner(Sprite newBanner) => banner = newBanner;

        public void UpdateDifficultyLevel(int newLevel)
        {
            newLevel = Mathf.Clamp(newLevel, 0, 10);
            difficultyLevel = newLevel;
        }

        public void UpdateType(int newType) => UpdateType((RoomType) newType);
        public void UpdateType(RoomType newType) => type = newType;

        public void UpdateLightness(int newLightness)
        {
            Preconditions.IsIntInRange(newLightness, 0, 255, "Room Lightness");
            lightness = newLightness;
        }
        
        public void UpdateLightnessColor(Color newColor) => lightnessColor = newColor;

        #endregion

        public Sprite Banner { get => banner; }
        public int DifficultyLevel { get => difficultyLevel; }
        public RoomType Type {get => type;}
        public int Lightness { get => lightness; }
        public Color LightnessColor { get => lightnessColor; }
        public ObjectGrid<AssetData> TileGrid { get => tileGrid; }
        public ObjectGrid<AssetData> DecorGrid { get => decorGrid; }
        public ObjectGrid<AssetData> ObjectGrid { get => objectGrid; }
        public ObjectGrid<AssetData> EnemyGrid { get => enemyGrid; }

        public class Builder : BaseBuilder<RoomAsset, Builder>
        {
            public Builder()
            {
                Asset.title = EditorDefaults.Instance.RoomTitle;
                Asset.icon = EditorDefaults.Instance.EmptySprite;
                Asset.author = EditorDefaults.Instance.Author;
                Asset.creationDate = DateTime.Now;
                Asset.GenerateID();

                Asset.difficultyLevel = EditorDefaults.Instance.RoomDifficulty;
                Asset.type = EditorDefaults.Instance.RoomType;
                Asset.lightness = EditorDefaults.Instance.RoomLightness;
                Asset.lightnessColor = EditorDefaults.Instance.RoomLightnessColor;
                Asset.tileGrid = new ObjectGrid<AssetData>(EditorDefaults.Instance.RoomSize.x, EditorDefaults.Instance.RoomSize.y, () => new AssetData(ParameterInfoConstants.ForTile));
                Asset.decorGrid = new ObjectGrid<AssetData>(EditorDefaults.Instance.RoomSize.x, EditorDefaults.Instance.RoomSize.y, () => new AssetData(ParameterInfoConstants.ForDecor));
                Asset.objectGrid = new ObjectGrid<AssetData>(EditorDefaults.Instance.RoomSize.x, EditorDefaults.Instance.RoomSize.y, () => new AssetData(ParameterInfoConstants.ForEmpty));
                Asset.enemyGrid = new ObjectGrid<AssetData>(EditorDefaults.Instance.RoomSize.x, EditorDefaults.Instance.RoomSize.y, () => new AssetData(ParameterInfoConstants.ForEnemy));
            }

            public Builder WithBanner(Sprite banner)
            {
                Asset.banner = banner;
                return This;
            }
            
            public Builder WithDifficultyLevel(int difficultyLevel)
            {
                Asset.difficultyLevel = difficultyLevel;
                return This;
            }

            public Builder WithType(RoomType type)
            {
                Asset.type = type;
                return This;
            }

            public Builder WithLightness(int lightness)
            {
                Asset.lightness = lightness;
                return This;
            }

            public Builder WithLightnessColor(Color lightnessColor)
            {
                Asset.lightnessColor = lightnessColor;
                return This;
            }

            public Builder WithTileGrid(ObjectGrid<AssetData> tileGrid)
            {
                Asset.tileGrid = new ObjectGrid<AssetData>(tileGrid);
                return This;
            }

            public Builder WithDecorGrid(ObjectGrid<AssetData> decorGrid)
            {
                Asset.decorGrid = new ObjectGrid<AssetData>(decorGrid);
                return This;
            }

            public Builder WithObjectGrid(ObjectGrid<AssetData> objectGrid)
            {
                Asset.objectGrid = new ObjectGrid<AssetData>(objectGrid);
                return This;
            }

            public Builder WithEnemyGrid(ObjectGrid<AssetData> enemyGrid)
            {
                Asset.enemyGrid = new ObjectGrid<AssetData>(enemyGrid);
                return This;
            }

            public override Builder AsClone(RoomAsset asset)
            {
                AsCopy(asset);
                Asset.GenerateID();
                return This;
            }

            public override Builder AsCopy(RoomAsset asset)
            {
                Asset.id = asset.ID;
                Asset.title = asset.Title;
                Asset.icon = asset.Icon;
                Asset.author = asset.Author;
                Asset.creationDate = asset.CreationDate;
                Asset.difficultyLevel = asset.DifficultyLevel;
                Asset.type = asset.Type;
                Asset.lightness = asset.Lightness;
                Asset.lightnessColor = asset.LightnessColor;
                Asset.tileGrid = new ObjectGrid<AssetData>(asset.TileGrid);
                Asset.decorGrid = new ObjectGrid<AssetData>(asset.DecorGrid);
                Asset.objectGrid = new ObjectGrid<AssetData>(asset.ObjectGrid);
                Asset.enemyGrid = new ObjectGrid<AssetData>(asset.EnemyGrid);
                return This;
            }

            protected sealed override RoomAsset Asset { get; } = new();
        }
    }
}