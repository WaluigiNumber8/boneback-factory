using System;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.PackData;

namespace Rogium.Global.GridSystem
{
    /// <summary>
    /// Holds methods for loading an <see cref="ObjectGrid{T}"/> of assets (type <see cref="AssetBase"/>) into the graphical Grid.
    /// </summary>
    public static class AssetGridLoader
    {
        /// <summary>
        /// Updates sprites on a grid of <see cref="GridCell"/>s using an <see cref="ObjectGrid{T}"/> of Asset IDs.
        /// </summary>
        /// <param name="cells">The cells that will have their sprite updated.</param>
        /// <param name="IDGrid">The grid of IDs that tells which asset is assigned to which cell. </param>
        /// <param name="assetList">The list fo asset from which to pick the sprites.</param>
        public static void LoadGridUI<T>(GridCell[,] cells, ObjectGrid<string> IDGrid, AssetList<T> assetList) where T : AssetBase
        {
            AssetBase lastFoundAsset = null;
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    string id = IDGrid.GetValue(x, y);
                    //When Cell is empty
                    if (id == EditorDefaults.EmptyID)
                    {
                        cells[x, y].UpdateSprite(EditorDefaults.EmptyGridSprite);
                        continue;
                    }
                    //Cache Optimisation
                    if (id == lastFoundAsset?.ID)
                    {
                        cells[x, y].UpdateSprite(lastFoundAsset.Icon);
                        continue;
                    }
                    //Try searching for ID
                    try
                    {
                        lastFoundAsset = assetList.GetByID(id);
                        cells[x, y].UpdateSprite(lastFoundAsset.Icon);
                    }
                    //If Asset not found, replace with empty tile.
                    catch (InvalidOperationException)
                    {
                        cells[x, y].UpdateSprite(EditorDefaults.MissingSprite);
                    }
                    
                }
            }
        }
    }
}