using System.Collections.Generic;
using Rogium.Editors.TileData;
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

        public Tilemap Tilemap => tilemap;
        public TileType Type => type;
        public IList<Vector3Int> Positions => positions;
        public IList<TileBase> Tiles => tiles;
    }
}