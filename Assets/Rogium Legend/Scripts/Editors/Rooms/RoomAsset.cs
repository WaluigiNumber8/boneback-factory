using BoubakProductions.Safety;
using RogiumLegend.Global.GridSystem;
using UnityEngine;

namespace RogiumLegend.Editors.RoomData
{
    /// <summary>
    /// Contains all data needed for a pack room.
    /// </summary>
    public class RoomAsset
    {
        private static readonly Vector2Int gridSize = new Vector2Int(20, 15);

        private string roomName;
        private int difficultyLevel;
        private ObjectGrid<TileAsset> tileGrid;

        #region Constructors
        public RoomAsset()
        {
            roomName = "New Room";
            difficultyLevel = 1;
            tileGrid = new ObjectGrid<TileAsset>(gridSize.x, gridSize.y, () => new TileAsset());
        }
        public RoomAsset(string roomName, int difficultyLevel)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.roomName = roomName;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = new ObjectGrid<TileAsset>(gridSize.x, gridSize.y, () => new TileAsset());
        }
        public RoomAsset(string roomName, int difficultyLevel, ObjectGrid<TileAsset> tileGrid)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.roomName = roomName;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = tileGrid;
        }
        #endregion

        public string RoomName { get => roomName;  }
        public int DifficultyLevel { get => difficultyLevel; }
        public ObjectGrid<TileAsset> TileGrid { get => tileGrid; }
    }
}