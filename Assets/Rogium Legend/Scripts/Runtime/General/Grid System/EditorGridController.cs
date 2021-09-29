using System;
using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Global.GridSystem
{
    /// <summary>
    /// Loads the Editor Grid by spawning all the Grid Cells.
    /// </summary>
    [RequireComponent(typeof(FlexibleGridGroup))]
    public class EditorGridController : MonoBehaviour
    {
        public event Action<Vector2Int> OnInteractionClick; 

        [SerializeField] private GridCell cellPrefab;
        [SerializeField] private Vector2Int gridSize;

        private GridCell[,] cells;

        private void Start()
        {
            cells = new GridCell[gridSize.x, gridSize.y];
            BuildGrid();
        }

        /// <summary>
        /// Delete all children and freshly Build the grid from scratch.
        /// </summary>
        private void BuildGrid()
        {
            gameObject.KillChildren();
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    cells[x, y] = Instantiate(cellPrefab, transform);
                    cells[x, y].Spawn(this, x, y, EditorDefaults.EmptyGridSprite);
                }
            }
        }

        /// <summary>
        /// When a grid cell is clicked.
        /// </summary>
        /// <param name="x">The X position of the cell.</param>
        /// <param name="y">The Y position of the cell.</param>
        public void OnCellClick(int x, int y)
        {
            OnInteractionClick?.Invoke(new Vector2Int(x, y));
        }

        /// <summary>
        /// Updates the sprite of the cell.
        /// </summary>
        /// <param name="cellPosition">Which cell on the grid to update.</param>
        /// <param name="sprite">Cell's new sprite.</param>
        public void UpdateCellSprite(Vector2Int cellPosition, Sprite sprite)
        {
            cells[cellPosition.x, cellPosition.y].UpdateSprite(sprite);
        }
        
    }
}