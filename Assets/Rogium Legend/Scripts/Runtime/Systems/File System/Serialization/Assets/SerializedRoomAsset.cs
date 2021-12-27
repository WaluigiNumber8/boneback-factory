using BoubakProductions.Systems.Serialization;
using Rogium.Editors.Rooms;
using Rogium.Editors.Core.Defaults;
using System;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the <see cref="RoomAsset"/>.
    /// </summary>
    [System.Serializable]
    public class SerializedRoomAsset
    {
        public readonly string id;
        public readonly string title;
        public readonly SerializedSprite icon;
        public readonly string author;
        public readonly string creationDate;
        public readonly int difficultyLevel;
        public readonly int type;
        public readonly SerializedGrid<string> tileGrid;

        public SerializedRoomAsset(RoomAsset asset)
        {
            this.id = asset.ID;
            this.title = asset.Title;
            this.icon = new SerializedSprite(asset.Icon);
            this.author = asset.Author;
            this.creationDate = asset.CreationDate.ToString();
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
                                 this.tileGrid.Deserialize(() => EditorDefaults.EmptyID),
                                 DateTime.Parse(this.creationDate));
        }
    }
}