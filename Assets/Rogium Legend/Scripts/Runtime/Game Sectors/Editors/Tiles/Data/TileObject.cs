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
        private TileLayerType layerType;

        #region Constructors
        public TileObject()
        {
            this.tile = ScriptableObject.CreateInstance<Tile>();
            // this.tile.color = Color.white;
            this.layerType = TileLayerType.Wall;
        }
        public TileObject(TileObject tileObject)
        {
            this.tile = tileObject.Tile;
            // this.tile.color = Color.white;
            this.layerType = tileObject.LayerType;
        }
        public TileObject(Tile tile, TileLayerType layerType)
        {
            this.tile = tile;
            // this.tile.color = Color.white;
            this.layerType = layerType;
        }
        #endregion

        #region Update Values
        public void UpdateType(TileLayerType newLayerType) => layerType = newLayerType;

        #endregion
        
        public Tile Tile { get => tile; }
        public TileLayerType LayerType { get => layerType; }
    }
}