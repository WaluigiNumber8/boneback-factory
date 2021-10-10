using System;
using UnityEngine;
using BoubakProductions.Core;
using BoubakProductions.UI;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.PackData;
using Rogium.Editors.RoomData;
using Rogium.Global.Input;

namespace Rogium.Global.GridSystem
{
    /// <summary>
    /// Overseers most actions happening on the Room Grid.
    /// </summary>
    [RequireComponent(typeof(FlexibleGridGroup), typeof(UIInput))]
    public class EditorGridOverseer : MonoBehaviour
    {
        public event Action<Vector2Int> OnInteractionClick;

        private UIInput input;

        [SerializeField] private GridCell cellPrefab;
        [SerializeField] private Vector2Int gridSize;

        private GridCell[,] cells;

        private void Awake()
        {
            input = GetComponent<UIInput>();
            
            cells = new GridCell[gridSize.x, gridSize.y];
            BuildGrid();
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
                    cells[x, y].Spawn(this, input, x, y, EditorDefaults.EmptyGridSprite);
                }
            }
        }

        /// <summary>
        /// Loads UI tiles into the editor grid.
        /// </summary>
        /// <param name="assetList">From which list of assets to load from.</param>
        /// <param name="room">The room based on which to load the data. </param>
        /// <typeparam name="T"></typeparam>
        public void LoadGrid<T>(AssetList<T> assetList, RoomAsset room) where T : AssetBase 
        {
            AssetGridLoader.LoadGridUI(cells, room.TileGrid, assetList);
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
        
        public Vector2Int GridSize {get => gridSize;}
    }
}