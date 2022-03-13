using Rogium.Editors.Tiles;
using Rogium.Systems.GridSystem;
using System.Collections.Generic;
using System.Linq;
using Rogium.Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Rogium.Gameplay.DataLoading
{
    /// <summary>
    /// Handles loading tile data into tilemaps.
    /// </summary>
    public class TileMapLoader : MonoBehaviour
    {
        [SerializeField] private TilemapLayer[] tilemaps;
        
        /// <summary>
        /// Load tiles onto tilemaps.
        /// </summary>
        /// <param name="originPos">The offset of the origin.</param>
        /// <param name="IDGrid">The grid of tile IDs to use loading.</param>
        /// <param name="dataList">List of Tile Assets to place down.</param>
        public void Load(Vector3Int originPos, ObjectGrid<string> IDGrid, IList<TileAsset> dataList)
        {
            Clear();
            AssetUtils.UpdateFromGridByList(IDGrid, dataList,
                (x, y, asset) => DecideAddition(originPos + new Vector3Int(x, y, 0), asset),
                (x, y) => Debug.LogError($"Did not find tile in position [{x}, {y}]."));
            
            Draw();
        }

        /// <summary>
        /// Decides on, to which <see cref="TilemapLayer"/> to add a specific tile.
        /// </summary>
        /// <param name="position">Position of the tile in the grid.</param>
        /// <param name="tile">The tile itself.</param>
        private void DecideAddition(Vector3Int position, TileAsset tile)
        {
            foreach (TilemapLayer layer in tilemaps)
            {
                if (layer.Type == tile.Type)
                {
                    AddTileToLayer(layer, position, tile.Tile);
                }
            }
        }

        /// <summary>
        /// Adds a tile to a <see cref="TilemapLayer"/>.
        /// </summary>
        /// <param name="layer">The layer teh tile will be added to.</param>
        /// <param name="position">Tile's position on the grid.</param>
        /// <param name="tile">The Tile itself.</param>
        private void AddTileToLayer(TilemapLayer layer, Vector3Int position, TileBase tile)
        {
            layer.Positions.Add(layer.Tilemap.WorldToCell(position));
            layer.Tiles.Add(tile);
        }
        
        /// <summary>
        /// Draws tiles to grids.
        /// </summary>
        private void Draw()
        {
            foreach (TilemapLayer layer in tilemaps)
            {
                layer.Tilemap.SetTiles(layer.Positions.ToArray(), layer.Tiles.ToArray());
                layer.Tilemap.RefreshAllTiles();
            }
        }
        
        /// <summary>
        /// Cleans the entire room.
        /// </summary>
        private void Clear()
        {
            foreach (TilemapLayer layer in tilemaps)
            {
                layer.Tilemap.ClearAllTiles();
                layer.Positions.Clear();
            }
        }
    }
}