using UnityEngine;
using UnityEngine.Tilemaps;

namespace Rogium.Editors.Tiles
{
    /// <summary>
    /// The Tile itself. It is used for for placement in the room.
    /// </summary>
    public class TileObject
    {
        private Tile tile;
        private TileType type;

        #region Constructors
        public TileObject()
        {
            this.tile = ScriptableObject.CreateInstance<Tile>();
            // this.tile.color = Color.white;
            this.type = TileType.Wall;
        }
        public TileObject(TileObject tileObject)
        {
            this.tile = tileObject.Tile;
            // this.tile.color = Color.white;
            this.type = tileObject.Type;
        }
        public TileObject(Tile tile, TileType type)
        {
            this.tile = tile;
            // this.tile.color = Color.white;
            this.type = type;
        }
        #endregion

        #region Update Values
        public void UpdateType(TileType newType)
        {
            type = newType;
        }

        #endregion
        
        public Tile Tile { get => tile; }
        public TileType Type { get => type; }
    }
}