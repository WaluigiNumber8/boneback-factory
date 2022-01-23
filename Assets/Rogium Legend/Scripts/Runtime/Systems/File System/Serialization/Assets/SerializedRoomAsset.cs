using Rogium.Editors.Rooms;
using Rogium.Editors.Core.Defaults;
using System;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="RoomAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedRoomAsset : SerializedAssetBase
    {
        public readonly int difficultyLevel;
        public readonly int type;
        public readonly SerializedGrid<string> tileGrid;

        public SerializedRoomAsset(RoomAsset asset) : base(asset)
        {
            this.difficultyLevel = asset.DifficultyLevel;
            this.type = (int)asset.Type;
            this.tileGrid = new SerializedGrid<string>(asset.TileGrid);
        }

        /// <summary>
        /// Deserializes the asset into a Unity readable format.
        /// </summary>
        /// <returns>A deserialized asset.</returns>
        public RoomAsset Deserialize()
        {
            return new RoomAsset(this.id,
                                 this.title,
                                 this.icon.Deserialize(),
                                 this.author,
                                 this.difficultyLevel,
                                 (RoomType)type,
                                 this.tileGrid.Deserialize(() => EditorDefaults.EmptyAssetID),
                                 DateTime.Parse(this.creationDate));
        }
    }
}