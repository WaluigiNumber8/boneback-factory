using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using System;
using System.Collections.Generic;
using Rogium.Core;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Holds methods for loading an <see cref="ObjectGrid{T}"/> of assets (type <see cref="AssetBase"/>) into the graphical Grid.
    /// </summary>
    public static class GridLoader
    {
        /// <summary>
        /// Builds the grid as a grid of sprites.
        /// </summary>
        /// <param name="cells">The cells that will have their sprite updated.</param>
        /// <param name="IDGrid">The grid of IDs that tells which asset is assigned to which cell.</param>
        /// <param name="assetList">A list of assets from which to pick the sprites.</param>
        public static void LoadBySprites<T>(GridCell[,] cells, ObjectGrid<string> IDGrid, IList<T> assetList) where T : IAsset
        {
            AssetUtils.UpdateFromGridByList(IDGrid, assetList, 
                                            (x, y, asset) => cells[x, y].UpdateSprite(asset.Icon),
                                            (x, y) => cells[x, y].UpdateSprite(EditorDefaults.MissingSprite), 
                                            (x, y, _) => cells[x, y].Clear());
            
        }
        
        /// <summary>
        /// Builds the grid as grid of colors.
        /// </summary>
        /// <param name="cells">The cells that will have their color updated.</param>
        /// <param name="indexGrid">The grid of indexes that tells which color is assigned to which cell.</param>
        /// <param name="colorArray">An array of colors from which to pick the colors.</param>
        public static void LoadByColor(GridCell[,] cells, ObjectGrid<int> indexGrid, Color[] colorArray)
        {
            ClearAllCells(cells);

            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    int index = indexGrid.GetValue(x, y);
                    //When Cell is empty
                    if (index == -1)
                    {
                        cells[x, y].Clear();
                        continue;
                    }

                    //Try updating with color.
                    try
                    {
                        cells[x, y].UpdateColor(colorArray[index]);
                    }
                    //If Asset not found, replace with empty tile.
                    catch (InvalidOperationException)
                    {
                        cells[x, y].UpdateColor(EditorDefaults.MissingColor);
                    }
                }
            }
        }

        /// <summary>
        /// Resets all cells to their empty state.
        /// </summary>
        /// <param name="cells">The cell array to clear.</param>
        private static void ClearAllCells(GridCell[,] cells)
        {
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y].Clear();
                }
            }
        }
    }
}