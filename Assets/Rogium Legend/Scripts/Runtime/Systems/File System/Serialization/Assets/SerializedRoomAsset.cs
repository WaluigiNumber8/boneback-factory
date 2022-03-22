using System;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Rooms;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="RoomAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedRoomAsset : SerializedAssetBase<RoomAsset>
    {
        private readonly int difficultyLevel;
        private readonly int type;
        private readonly int lightness;
        private readonly SerializedGrid<AssetData> tileGrid;
        private readonly SerializedGrid<AssetData> objectGrid;
        private readonly SerializedGrid<AssetData> enemyGrid;

        public SerializedRoomAsset(RoomAsset asset) : base(asset)
        {
            difficultyLevel = asset.DifficultyLevel;
            type = (int)asset.Type;
            lightness = asset.Lightness;
            tileGrid = new SerializedGrid<AssetData>(asset.TileGrid);
            objectGrid = new SerializedGrid<AssetData>(asset.ObjectGrid);
            enemyGrid = new SerializedGrid<AssetData>(asset.EnemyGrid);
        }

        /// <summary>
        /// Deserializes the asset into a Unity readable format.
        /// </summary>
        /// <returns>A deserialized asset.</returns>
        public override RoomAsset Deserialize()
        {
            return new RoomAsset(id,
                                 title,
                                 icon.Deserialize(),
                                 author,
                                 difficultyLevel,
                                 (RoomType)type,
                                 lightness,
                                 tileGrid.Deserialize(() => new AssetData(ParameterDefaults.ParamsTile)),
                                 objectGrid.Deserialize(() => new AssetData(ParameterDefaults.ParamsEmpty)),
                                 enemyGrid.Deserialize(() => new AssetData(ParameterDefaults.ParamsEnemy)),
                                 DateTime.Parse(creationDate));
        }
    }
}