
using UnityEngine.Tilemaps;

namespace RogiumLegend.Editors.TileData
{
    /// <summary>
    /// The Tile itself. It is used for for placement in the room.
    /// </summary>
    public class TileObject
    {
        private Tile tile;
        private TileType type;

        public TileObject()
        {
            this.tile = new Tile();
            this.type = TileType.Wall;
        }
        public TileObject(Tile tile, TileType type)
        {
            this.tile = tile;
            this.type = type;
        }

        public Tile Tile { get => tile; }
        public TileType Type { get => type; }
    }
}