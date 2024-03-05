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
    public class JSONRoomAsset : JSONAssetBase<RoomAsset>
    {
        public int difficultyLevel;
        public int type;
        public int lightness;
        public JSONGrid<AssetData> tileGrid;
        public JSONGrid<AssetData> objectGrid;
        public JSONGrid<AssetData> enemyGrid;

        public JSONRoomAsset(RoomAsset asset) : base(asset)
        {
            difficultyLevel = asset.DifficultyLevel;
            type = (int)asset.Type;
            lightness = asset.Lightness;
            tileGrid = new JSONGrid<AssetData>(asset.TileGrid);
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
            objectGrid.SetDefaultCreator(() => new AssetData(ParameterInfoConstants.ForEmpty));
            enemyGrid.SetDefaultCreator(() => new AssetData(ParameterInfoConstants.ForEnemy));
            
            return new RoomAsset(id,
                                 title,
                                 icon.Decode(),
                                 author,
                                 difficultyLevel,
                                 (RoomType)type,
                                 lightness,
                                 tileGrid.Decode(),
                                 objectGrid.Decode(),
                                 enemyGrid.Decode(),
                                 DateTime.Parse(creationDate));
        }
    }
}