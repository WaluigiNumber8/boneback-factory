using System;
using System.Linq;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.TileData;
using Rogium.Global.GridSystem;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Rogium.Gameplay.DataLoading
{
    /// <summary>
    /// Handles loading data into tilemaps.
    /// </summary>
    public class TilemapLoader
    {
        public void LoadTiles(TilemapLayer[] layers, ObjectGrid<string> IDgrid, Vector3Int originPos, AssetList<TileAsset> dataList)
        {
            Organize(layers, IDgrid, originPos, dataList);
            DrawTiles(layers);
        }

        /// <summary>
        /// Loads tiles into correct tilemaps.
        /// </summary>
        /// <param name="layers">The tilemaps, that will contain the tiles.</param>
        /// <param name="IDgrid">The list of IDs, based on which to load the tiles.</param>
        /// <param name="offsetPos">Offset position on the grid.</param>
        /// <param name="dataList">List of Tile Assets from which the data is grabbed.</param>
        private void Organize(TilemapLayer[] layers, ObjectGrid<string> IDgrid, Vector3Int offsetPos, AssetList<TileAsset> dataList)
        {
            TileAsset lastFoundAsset = null;
            for (int y = 0; y < IDgrid.Height; y++)
            {
                for (int x = 0; x < IDgrid.Width; x++)
                {
                    string id = IDgrid.GetValue(x, y);
                    //Id is empty.
                    if (id == EditorDefaults.EmptyID)
                    {
                        continue;
                    }

                    //Cache optimisation.
                    if (id == lastFoundAsset?.ID)
                    {
                        DecideAddition(layers, offsetPos + new Vector3Int(x, y, 0), lastFoundAsset);
                        continue;
                    }

                    //Try searching for id.
                    try
                    {
                        lastFoundAsset = dataList.GetByID(id);
                        DecideAddition(layers, offsetPos + new Vector3Int(x, y, 0), lastFoundAsset);
                    }
                    //When the tile is missing.
                    catch (InvalidOperationException)
                    {
                        //TODO Create a 'Missing' Tile Asset.
                        Debug.LogError($"Did not find tile with id {id}.");
                    }

                }
            }
        }

        /// <summary>
        /// Decides on, to which <see cref="TilemapLayer"/> to add a specific tile.
        /// </summary>
        /// <param name="layers">Array of layers.</param>
        /// <param name="position">Position of the tile in the grid.</param>
        /// <param name="tile">The tile itself.</param>
        private void DecideAddition(TilemapLayer[] layers, Vector3Int position, TileAsset tile)
        {
            foreach (TilemapLayer layer in layers)
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
        /// <param name="layers">All the Tilemaps, that tiles will be drawn on.</param>
        private void DrawTiles(TilemapLayer[] layers)
        {
            foreach (TilemapLayer layer in layers)
            {
                layer.Tilemap.SetTiles(layer.Positions.ToArray(), layer.Tiles.ToArray());
                layer.Tilemap.RefreshAllTiles();
            }
        }
    }
}