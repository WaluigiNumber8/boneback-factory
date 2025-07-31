using System;
using RedRats.Systems.FileSystem.JSON.Serialization;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Rooms;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="RoomAsset"/>.
    /// </summary>
    [System.Serializable]
    public class JSONRoomAsset : JSONAssetBase<RoomAsset>
    {
        public JSONSprite banner;
        public int difficultyLevel;
        public int type;
        public int lightness;
        public JSONColor lightnessColor;
        public JSONGrid<AssetData> tileGrid;
        public JSONGrid<AssetData> decorGrid;
        public JSONGrid<AssetData> objectGrid;
        public JSONGrid<AssetData> enemyGrid;

        public JSONRoomAsset(RoomAsset asset) : base(asset)
        {
            banner = new JSONSprite(asset.Banner);
            difficultyLevel = asset.DifficultyLevel;
            type = (int)asset.Type;
            lightness = asset.Lightness;
            lightnessColor = new JSONColor(asset.LightnessColor);
            tileGrid = new JSONGrid<AssetData>(asset.TileGrid);
            decorGrid = new JSONGrid<AssetData>(asset.DecorGrid);
            objectGrid = new JSONGrid<AssetData>(asset.ObjectGrid);
            enemyGrid = new JSONGrid<AssetData>(asset.EnemyGrid);
        }

        /// <summary>
        /// Deserializes the asset into a Unity readable format.
        /// </summary>
        /// <returns>A deserialized asset.</returns>
        public override RoomAsset Decode()
        {
            tileGrid.SetDefaultCreator(() => new AssetData(ParameterInfoConstants.ForTile));
            decorGrid.SetDefaultCreator(() => new AssetData(ParameterInfoConstants.ForDecor));
            objectGrid.SetDefaultCreator(() => new AssetData(ParameterInfoConstants.ForEmpty));
            enemyGrid.SetDefaultCreator(() => new AssetData(ParameterInfoConstants.ForEnemy));

            return new RoomAsset.Builder()
                .WithID(id)
                .WithTitle(title)
                .WithIcon(icon.Decode())
                .WithAuthor(author)
                .WithCreationDate(DateTime.Parse(creationDate))
                .WithBanner(banner.Decode())
                .WithDifficultyLevel(difficultyLevel)
                .WithType((RoomType)type)
                .WithLightness(lightness)
                .WithLightnessColor(lightnessColor.Decode())
                .WithTileGrid(tileGrid.Decode())
                .WithDecorGrid(decorGrid.Decode())
                .WithObjectGrid(objectGrid.Decode())
                .WithEnemyGrid(enemyGrid.Decode())
                .Build();
        }
    }
}