using System;
using BoubakProductions.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Input;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// A type of grid the player can interact with via a pointer. Stores references to assets
    /// </summary>
    public class InteractableEditorGrid : MonoBehaviour
    {
        public event Action<Vector2Int> OnInteractionClick;

        [SerializeField] private GridCell cellPrefab;
        [SerializeField] private Vector2Int gridSize;
        
        private GridCell[,] cells;
        private UIInput input;
        
        private void Awake()
        {
            input = GetComponent<UIInput>();
            
            cells = new GridCell[gridSize.x, gridSize.y];
            BuildGrid();
        }

        private void OnEnable()
        {
            GridCell.OnCellClick += OnCellClick;
        }

        private void OnDisable()
        {
            GridCell.OnCellClick -= OnCellClick;
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
        /// Loads sprites into the editor grid.
        /// </summary>
        /// <param name="assetList">From which list of assets to load from.</param>
        /// <param name="IDGrid">The grid of IDs to read.</param>
        /// <typeparam name="T">Is a type of Asset.</typeparam>
        public void LoadWithSprites<T>(AssetList<T> assetList, ObjectGrid<string> IDGrid) where T : AssetBase 
        {
            GridLoader.LoadBySprites(cells, IDGrid, assetList);
        }

        /// <summary>
        /// Updates the sprite of the cell. Resets the cells color to white.
        /// </summary>
        /// <param name="cellPosition">Which cell on the grid to update.</param>
        /// <param name="sprite">Cell's new sprite.</param>
        public void UpdateCellSprite(Vector2Int cellPosition, Sprite sprite)
        {
            cells[cellPosition.x, cellPosition.y].UpdateSprite(sprite);
        }
        
        /// <summary>
        /// Loads UI tiles into the editor grid.
        /// </summary>
        /// <param name="colorArray">A color array to read from.</param>
        /// <param name="indexGrid">The grid of indexes to read.</param>
        public void LoadWithColors(Color[] colorArray, ObjectGrid<int> indexGrid)
        {
            GridLoader.LoadByColor(cells, indexGrid, colorArray);
        }
        
        /// <summary>
        /// Updates the color of the cell. Removes any sprite assigned to the cell.
        /// </summary>
        /// <param name="cellPosition">Which cell on the grid to update.</param>
        /// <param name="color">Cell's new color sprite.</param>
        public void UpdateCellColor(Vector2Int cellPosition, Color color)
        {
            cells[cellPosition.x, cellPosition.y].UpdateColor(color);
        }
        
        /// <summary>
        /// Delete all children and freshly Build the grid from scratch.
        /// </summary>
        private void BuildGrid()
        {
            gameObject.KillChildren();
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    cells[x, y] = Instantiate(cellPrefab, transform);
                    cells[x, y].Construct(input, x, y, EditorDefaults.EmptyGridColor);
                }
            }
        }
        
        public Vector2Int GridSize {get => gridSize;}
    }
}