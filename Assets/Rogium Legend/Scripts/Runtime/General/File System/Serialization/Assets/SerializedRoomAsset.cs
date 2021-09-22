using System;
using Rogium.Editors.RoomData;
using Rogium.Editors.TileData;
using System.Collections.Generic;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the Room Asset.
    /// </summary>
    [System.Serializable]
    public class SerializedRoomAsset
    {
        public readonly string roomName;
        public readonly SerializedSprite icon;
        public readonly string author;
        public readonly string creationDate;
        public readonly int difficultyLevel;
        public readonly SerializedGrid<int> tileGrid;

        public SerializedRoomAsset(RoomAsset rm)
        {
            this.roomName = rm.Title;
            this.icon = new SerializedSprite(rm.Icon);
            this.author = rm.Author;
            this.creationDate = rm.CreationDate.ToString();
            this.difficultyLevel = rm.DifficultyLevel;
            this.tileGrid = new SerializedGrid<int>(rm.TileGrid);
        }

        public RoomAsset Deserialize()
        {
            return new RoomAsset(this.roomName,
                                 this.icon.Deserialize(),
                                 this.author,
                                 this.difficultyLevel,
                                 this.tileGrid.Deserialize(),
                                 DateTime.Parse(this.creationDate));
        }
    }
}