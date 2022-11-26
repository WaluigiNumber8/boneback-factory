using System.Collections.Generic;
using Rogium.Editors.Tiles;
using Rogium.Systems.GridSystem;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.Gameplay.DataLoading
{
    /// <summary>
    /// Handles loading tile data into tilemaps.
    /// </summary>
    public class TileMapDataBuilder : MonoBehaviour
    {
        [SerializeField] private TilemapLayer[] tilemaps;
        
        /// <summary>
        /// Load tiles onto tilemaps.
        /// </summary>
        /// <param name="originPos">The offset of the origin.</param>
        /// <param name="IDGrid">The grid of tile IDs to use loading.</param>
        /// <param name="dataList">List of Tile Assets to place down.</param>
        public void Load(Vector3Int originPos, ObjectGrid<AssetData> IDGrid, IList<TileAsset> dataList)
        {
            Clear();
            
            AssetUtils.UpdateFromGridByDict(IDGrid, dataList,
                (x, y, asset) => DecideAddition(originPos + new Vector3Int(x, y, 0), asset),
                (x, y) => Debug.LogError($"Did not find tile in position [{x}, {y}]."));
            
            foreach (TilemapLayer layer in tilemaps)
            {
                layer.Refresh();
            }
        }

        /// <summary>
        /// Clear all tilemaps.
        /// </summary>
        public void Clear()
        {
            foreach (TilemapLayer layer in tilemaps)
            {
                layer.Clear();
            }
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
                if (layer.Type != tile.Type) continue;
                
                layer.Positions.Add(layer.Tilemap.WorldToCell(position));
                layer.Tiles.Add(tile.Tile);
            }
        }
    }
}