using System;
using RedRats.Systems.FileSystem;
using Rogium.Systems.GridSystem;

namespace Rogium.ExternalStorage.Serialization
{
    /// <summary>
    /// The <see cref="ObjectGrid{T}"/> object converted to JSON-understandable format.
    /// </summary>
    [System.Serializable]
    public class JSONGrid<T>
    {
        public int width, height;
        public T[] serializedGrid;
        private Func<T> newDefaultObject;

        public JSONGrid(ObjectGrid<T> grid)
        {
            width = grid.Width;
            height = grid.Height;
            serializedGrid = new T[width * height];

            int index = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    T content = grid.GetAt(x, y);
                    serializedGrid[index] = content;
                    index++;
                }
            }
        }

        /// <summary>
        /// Sets the method for creating a default object in the grid.
        /// </summary>
        /// <param name="newDefaultObject">The method that creates a new default object..</param>
        public void SetDefaultCreator(Func<T> newDefaultObject)
        {
            this.newDefaultObject = newDefaultObject;
        }
        
        public ObjectGrid<T> Decode()
        {
            ObjectGrid<T> grid = new(width, height, newDefaultObject);

            int index = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    T value = serializedGrid[index];
                    grid.SetTo(x, y, value);
                    index++;
                }
            }
            return grid;
        }
    }
}