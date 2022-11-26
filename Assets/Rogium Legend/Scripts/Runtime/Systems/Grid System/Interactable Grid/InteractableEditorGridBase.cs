using System.Collections.Generic;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    public abstract class InteractableEditorGridBase : MonoBehaviour
    {
        /// <summary>
        /// Loads sprites into the editor grid.
        /// </summary>
        /// <param name="assets">From which list of assets to load from.</param>
        /// <param name="IDGrid">The grid of IDs to read.</param>
        /// <typeparam name="T">Is a type of Asset.</typeparam>
        public abstract void LoadWithSprites<T>(ObjectGrid<string> IDGrid, IDictionary<string, T> assets) where T : IAsset;

        /// <summary>
        /// Loads UI tiles into the editor grid.
        /// </summary>
        /// <param name="colors">A color array to read from.</param>
        /// <param name="indexGrid">The grid of indexes to read.</param>
        public abstract void LoadWithColors(ObjectGrid<int> indexGrid, Color[] colors);

        /// <summary>
        /// Updates a cell with a new value.
        /// </summary>
        /// <param name="position">The grid cell to update.</param>
        /// <param name="value">The value to set.</param>
        public abstract void UpdateCell(Vector2Int position, Color value);

        /// <summary>
        /// Updates a cell with a new value.
        /// </summary>
        /// <param name="position">The grid cell to update.</param>
        /// <param name="value">The value to set.</param>
        public abstract void UpdateCell(Vector2Int position, Sprite value);

        /// <summary>
        /// Applies grid changes.
        /// </summary>
        public abstract void Apply();

        /// <summary>
        /// Clears all elements on the active grid.
        /// </summary>
        public abstract void ClearAllCells();
    }
}