using System;
using Rogium.Editors.Core;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    public abstract class InteractableEditorGridBase : MonoBehaviour
    {
        public virtual event Action<Vector2Int> OnClick;
        
        /// <summary>
        /// Loads sprites into the editor grid.
        /// </summary>
        /// <param name="assetList">From which list of assets to load from.</param>
        /// <param name="IDGrid">The grid of IDs to read.</param>
        /// <typeparam name="T">Is a type of Asset.</typeparam>
        public abstract void LoadWithSprites<T>(AssetList<T> assetList, ObjectGrid<string> IDGrid) where T : AssetBase;

        /// <summary>
        /// Loads UI tiles into the editor grid.
        /// </summary>
        /// <param name="colorArray">A color array to read from.</param>
        /// <param name="indexGrid">The grid of indexes to read.</param>
        public abstract void LoadWithColors(Color[] colorArray, ObjectGrid<int> indexGrid);

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
    }
}