using System;
using BoubakProductions.Core;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// A type of grid the player can interact with via a pointer. Stores references to assets
    /// </summary>
    public class InteractableEditorGrid : InteractableEditorGridBase
    {
        public override event Action<Vector2Int> OnClick;

        [SerializeField] private GridCell cellPrefab;
        [SerializeField] private Vector2Int gridSize;
        
        private GridCell[,] cells;
        
        private void Awake()
        {
            cells = new GridCell[gridSize.x, gridSize.y];
            BuildGrid();
        }

        private void OnEnable() => GridCell.OnCellClick += OnCellClick;

        private void OnDisable() => GridCell.OnCellClick -= OnCellClick;

        /// <summary>
        /// When a grid cell is clicked.
        /// </summary>
        /// <param name="x">The X position of the cell.</param>
        /// <param name="y">The Y position of the cell.</param>
        public void OnCellClick(int x, int y) => OnClick?.Invoke(new Vector2Int(x, y));

        
        public override void LoadWithSprites<T>(AssetList<T> assetList, ObjectGrid<string> IDGrid)
        {
            SafetyNet.EnsureIntIsEqual(IDGrid.Width, gridSize.x, "Grid Width");
            SafetyNet.EnsureIntIsEqual(IDGrid.Height, gridSize.y, "Grid Height");
            GridLoader.LoadBySprites(cells, IDGrid, assetList);
        }
        
        
        public override void LoadWithColors(Color[] colorArray, ObjectGrid<int> indexGrid)
        {
            SafetyNet.EnsureIntIsEqual(indexGrid.Width, gridSize.x, "Grid Width");
            SafetyNet.EnsureIntIsEqual(indexGrid.Height, gridSize.y, "Grid Height");
            GridLoader.LoadByColor(cells, indexGrid, colorArray);
        }
        
        public override void UpdateCell(Vector2Int cellPosition, Sprite sprite)
        {
            cells[cellPosition.x, cellPosition.y].UpdateSprite(sprite);
        }

        public override void UpdateCell(Vector2Int cellPosition, Color color)
        {
            cells[cellPosition.x, cellPosition.y].UpdateColor(color);
        }
        
        public override void Apply() { }
        
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
                    cells[x, y].Construct(x, y, EditorDefaults.EmptyGridColor);
                }
            }
        }
        
        public Vector2Int GridSize {get => gridSize;}
    }
}