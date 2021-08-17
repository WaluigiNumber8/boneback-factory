using System.Collections;
using UnityEngine;

namespace RogiumLegend.ExternalStorage.Serialization
{
    /// <summary>
    /// Serialized form of the Room Asset.
    /// </summary>
    [System.Serializable]
    public class SerializedRoomAsset
    {
        public readonly string roomName;
        public readonly int difficultyLevel;
        public readonly SerializedTileAsset[,] grid;

        public SerializedRoomAsset(string roomName, int difficultyLevel, SerializedTileAsset[,] grid)
        {
            this.roomName = roomName;
            this.difficultyLevel = difficultyLevel;
            this.grid = grid;
        }
    }
}