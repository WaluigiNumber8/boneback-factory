using Rogium.Editors.Rooms;
using Rogium.Editors.Core.Defaults;
using System;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="RoomAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedRoomAsset : SerializedAssetBase<RoomAsset>
    {
        public readonly int difficultyLevel;
        public readonly int type;
        public readonly SerializedGrid<string> tileGrid;
        public readonly SerializedGrid<string> enemyGrid;

        public SerializedRoomAsset(RoomAsset asset) : base(asset)
        {
            difficultyLevel = asset.DifficultyLevel;
            type = (int)asset.Type;
            tileGrid = new SerializedGrid<string>(asset.TileGrid);
            enemyGrid = new SerializedGrid<string>(asset.EnemyGrid);
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
                                 tileGrid.Deserialize(() => EditorDefaults.EmptyAssetID),
                                 enemyGrid.Deserialize(() => EditorDefaults.EmptyAssetID),
                                 DateTime.Parse(creationDate));
        }
    }
}