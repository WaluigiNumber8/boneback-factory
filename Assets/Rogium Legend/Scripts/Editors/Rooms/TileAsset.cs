using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RogiumLegend.Editors.RoomData
{
    public enum TileType
    {
        Floor,
        Wall
    }

    /// <summary>
    /// Contains all data needed for a tile in a pack.
    /// </summary>
    public class TileAsset
    {
        private Tile tile;
        private TileType type;
    }
}