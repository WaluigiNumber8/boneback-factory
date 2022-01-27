using BoubakProductions.Safety;
using System;
using UnityEngine;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// An object that stores T-type in a grid array.
    /// </summary>
    public class ObjectGrid<T>
    {
        private readonly int width;
        private readonly int height;
        private readonly T[,] cellArray;
        
        public ObjectGrid(int width, int height, Func<T> createDefaultObject)
        {
            this.width = width;
            this.height = height;
            cellArray = new T[width, height];

            InitializeGrid(createDefaultObject);
        }

        public ObjectGrid(ObjectGrid<T> grid)
        {
            this.width = grid.Width;
            this.height = grid.Height;
            this.cellArray = grid.cellArray;
        }

        /// <summary>
        /// Upon Creation, initialize the grid a default form of T.
        /// </summary>
        /// <param name="createDefaultObject"></param>
        private void InitializeGrid(Func<T> createDefaultObject)
        {
            for (int i = 0; i < cellArray.GetLength(0); i++)
            {
                for (int j = 0; j < cellArray.GetLength(1); j++)
                {
                    cellArray[i, j] = createDefaultObject();
                }
            }
        }

        /// <summary>
        /// Change a value in a specific position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="value">Value to set.</param>
        public void SetValue(Vector2Int position, T value)
        {
            SetValue(position.x, position.y, value);
        }
        /// <summary>
        /// Change a value in a specific position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value">Value to set.</param>
        public void SetValue(int x, int y, T value)
        {
            SafetyNet.EnsureIntIsInRange(x, 0, width, "Grid X");
            SafetyNet.EnsureIntIsInRange(y, 0, height, "Grid Y");
            cellArray[x, y] = value;
        }

        /// <summary>
        /// Get a value from a specific grid cell.
        /// </summary>
        /// <param name="position">The position on the grid.</param>
        /// <returns>The value on that position.</returns>
        public T GetValue(Vector2Int position)
        {
            return GetValue(position.x, position.y);
        }
        /// <summary>
        /// Get a value from a specific grid cell.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public T GetValue(int x, int y)
        {
            SafetyNet.EnsureIntIsInRange(x, 0, width-1, "Grid X");
            SafetyNet.EnsureIntIsInRange(y, 0, height-1, "Grid Y");
            return cellArray[x, y];
        }

        public int Width { get => width; }
        public int Height { get => height; }
    }
}