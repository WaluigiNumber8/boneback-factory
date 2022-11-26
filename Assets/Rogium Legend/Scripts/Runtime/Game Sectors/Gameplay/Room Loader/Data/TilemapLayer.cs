using System.Collections.Generic;
using System.Linq;
using Rogium.Editors.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Rogium.Gameplay.DataLoading
{
    [System.Serializable]
    public class TilemapLayer
    {
        [SerializeField] private string title;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileType type;
        
        private IList<Vector3Int> positions;
        private IList<TileBase> tiles;

        public TilemapLayer()
        {
            positions = new List<Vector3Int>();
            tiles = new List<TileBase>();
        }
        
        public TilemapLayer(Tilemap tilemap)
        {
            this.tilemap = tilemap;
            positions = new List<Vector3Int>();
            tiles = new List<TileBase>();
        }

        /// <summary>
        /// Refreshes the stored tilemap by setting stored tiles to it.
        /// </summary>
        public void Refresh()
        {
            tilemap.SetTiles(positions.ToArray(), tiles.ToArray());
            tilemap.RefreshAllTiles();
        }

        /// <summary>
        /// Clears all the data.
        /// </summary>
        public void Clear()
        {
            positions.Clear();
            tiles.Clear();
            tilemap.ClearAllTiles();
        }
        
        public Tilemap Tilemap => tilemap;
        public TileType Type => type;
        public IList<Vector3Int> Positions => positions;
        public IList<TileBase> Tiles => tiles;
    }
}