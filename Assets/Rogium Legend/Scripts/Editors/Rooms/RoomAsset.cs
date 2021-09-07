using BoubakProductions.Safety;
using RogiumLegend.Editors.TileData;
using RogiumLegend.Global.GridSystem;
using UnityEngine;

namespace RogiumLegend.Editors.RoomData
{
    /// <summary>
    /// Contains all data needed for a pack room.
    /// </summary>
    public class RoomAsset : IAsset
    {
        public static readonly Vector2Int gridSize = new Vector2Int(20, 15);

        private string title;
        private Sprite icon;
        private int difficultyLevel;
        private ObjectGrid<TileAsset> tileGrid;

        #region Constructors
        public RoomAsset()
        {
            title = "New Room";
            difficultyLevel = 1;
            tileGrid = new ObjectGrid<TileAsset>(gridSize.x, gridSize.y, () => new TileAsset());
        }
        public RoomAsset(string roomName, int difficultyLevel)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = roomName;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = new ObjectGrid<TileAsset>(gridSize.x, gridSize.y, () => new TileAsset());
        }
        public RoomAsset(string roomName, int difficultyLevel, ObjectGrid<TileAsset> tileGrid)
        {
            SafetyNet.EnsureIntIsBiggerThan(difficultyLevel, 0, "New Room Difficulty Level");

            this.title = roomName;
            this.difficultyLevel = difficultyLevel;
            this.tileGrid = tileGrid;
        }
        #endregion

        public string Title { get => title; }
        public Sprite Icon { get => icon; }
        public int DifficultyLevel { get => difficultyLevel; }
        public ObjectGrid<TileAsset> TileGrid { get => tileGrid; }
    }
}