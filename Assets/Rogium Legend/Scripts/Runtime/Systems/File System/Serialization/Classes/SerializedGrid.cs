using System;
using Rogium.Systems.GridSystem;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// A Serialized form of <see cref="ObjectGrid"/>. 
    /// </summary>
    [System.Serializable]
    public class SerializedGrid<T>
    {
        private readonly T[,] serializedGrid;

        public SerializedGrid(ObjectGrid<T> grid)
        {
            serializedGrid = new T[grid.Width, grid.Height];
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    T content = grid.GetValue(x, y);
                    serializedGrid.SetValue(content, x, y);

                }
            }
        }

        public ObjectGrid<T> Deserialize(Func<T> createDefaultObject)
        {
            int width = serializedGrid.GetLength(0);
            int height = serializedGrid.GetLength(1);
            ObjectGrid<T> grid = new ObjectGrid<T>(width, height, createDefaultObject);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    T value = serializedGrid[x, y];
                    grid.SetValue(x, y, value);
                }
            }
            return grid;
        }
    }
}