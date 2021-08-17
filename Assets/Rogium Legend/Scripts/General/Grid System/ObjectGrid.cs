using BoubakProductions.Safety;
using System;

namespace RogiumLegend.Global.GridSystem
{
    /// <summary>
    /// An object that stores T-type in a grid array.
    /// </summary>
    public class ObjectGrid<T>
    {
        private readonly int width;
        private readonly int height;
        private readonly T[,] cellArray;

        public ObjectGrid(int width, int height, Func<T> CreateDefaultObject)
        {
            this.width = width;
            this.height = height;
            cellArray = new T[width, height];

            InitiliazeGrid(CreateDefaultObject);
        }

        /// <summary>
        /// Upon Creation, initiliaze the grid a default form of T.
        /// </summary>
        /// <param name="CreateDefaultObject"></param>
        private void InitiliazeGrid(Func<T> CreateDefaultObject)
        {
            for (int i = 0; i < cellArray.GetLength(0); i++)
            {
                for (int j = 0; j < cellArray.GetLength(1); j++)
                {
                    cellArray[i, j] = CreateDefaultObject();
                }
            }
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
        /// Get an INT value from a specific grid cell.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public T GetValue(int x, int y)
        {
            SafetyNet.EnsureIntIsInRange(x, 0, width, "Grid X");
            SafetyNet.EnsureIntIsInRange(y, 0, height, "Grid Y");
            return cellArray[x, y];
        }

        public int Width { get => width; }
        public int Height { get => height; }
        public T[,] CellArray => cellArray;
    }
}