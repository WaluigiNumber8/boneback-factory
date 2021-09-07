using RogiumLegend.Editors.PackData;
using RogiumLegend.Editors.RoomData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the PackAsset Class, ready for saving.
    /// </summary>
    [System.Serializable]
    public class SerializedPackAsset
    {
        public readonly SerializedPackInfoAsset packInfo;
        public readonly IList<SerializedRoomAsset> rooms; 

        public SerializedPackAsset(PackAsset pack)
        {
            this.packInfo = new SerializedPackInfoAsset(pack.PackInfo);
            this.rooms = SerializationFuncs.SerializeRooms(pack.Rooms);
        }
    }
}