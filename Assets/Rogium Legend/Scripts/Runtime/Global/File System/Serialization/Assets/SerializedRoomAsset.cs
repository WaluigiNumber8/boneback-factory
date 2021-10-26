using System;
using Rogium.Editors.Rooms;
using Rogium.Editors.TileData;
using System.Collections.Generic;
using Rogium.Editors.Core.Defaults;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the Room Asset.
    /// </summary>
    [System.Serializable]
    public class SerializedRoomAsset
    {
        public readonly string id;
        public readonly string roomName;
        public readonly SerializedSprite icon;
        public readonly string author;
        public readonly string creationDate;
        public readonly int difficultyLevel;
        public readonly int type;
        public readonly SerializedGrid<string> tileGrid;

        public SerializedRoomAsset(RoomAsset rm)
        {
            this.id = rm.ID;
            this.roomName = rm.Title;
            this.icon = new SerializedSprite(rm.Icon);
            this.author = rm.Author;
            this.creationDate = rm.CreationDate.ToString();
            this.difficultyLevel = rm.DifficultyLevel;
            this.type = (int)rm.Type;
            this.tileGrid = new SerializedGrid<string>(rm.TileGrid);
        }

        public RoomAsset Deserialize()
        {
            return new RoomAsset(this.id,
                                 this.roomName,
                                 this.icon.Deserialize(),
                                 this.author,
                                 this.difficultyLevel,
                                 (RoomType)type,
                                 this.tileGrid.Deserialize(() => EditorDefaults.EmptyID),
                                 DateTime.Parse(this.creationDate));
        }
    }
}