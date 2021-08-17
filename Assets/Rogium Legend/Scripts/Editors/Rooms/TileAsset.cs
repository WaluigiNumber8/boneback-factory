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

        public TileAsset()
        {
            this.type = TileType.Wall;
            this.tile = ScriptableObject.CreateInstance<Tile>();
            this.tile.sprite = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));
        }
        public TileAsset(Tile tile, TileType type)
        {
            this.type = type;
            this.tile = tile;
        }

        public Tile Tile { get => tile; }
        public TileType Type { get => type;}
    }
}