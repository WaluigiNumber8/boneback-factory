using RogiumLegend.Global.GridSystem;
using System.Collections.Generic;

namespace RogiumLegend.ExternalStorage.Serialization
{
    /// <summary>
    /// A Serialized form of <see cref="ObjectGrid"/>. 
    /// </summary>
    [System.Serializable]
    public class SerializedGrid<T> where T : new ()
    {
        private readonly IList<T> objects = new List<T>();
        private readonly int[,] serializedGrid;

        public SerializedGrid(ObjectGrid<T> grid)
        {
            serializedGrid = new int[grid.Width, grid.Height];
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    T content = grid.GetValue(x, y);
                    int value = CheckID(content);
                    serializedGrid.SetValue(value, x, y);

                }
            }
        }

        /// <summary>
        /// If the given gridObject is registered in the list of IDs, return it's ID.
        /// Otherwise add it to the list.
        /// </summary>
        /// <param name="gridObject">The object to check.</param>
        /// <returns>gridObject's ID.</returns>
        private int CheckID(T gridObject)
        {
            //Return 0 if list is empty.
            if (objects.Count < 1)
            {
                objects.Add(gridObject);
                return 0;
            }

            //Check if object is in the list. If yes return it's index.
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i].Equals(gridObject))
                    return i;
            }

            //Add object to list.
            objects.Add(gridObject);
            return objects.Count;
        }

        /// <summary>
        /// Deserializes the grid to a Unity acceptable format.
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="objectList">The List of object to read from.</param>
        public ObjectGrid<T> Deserialize(IList<T> objectList)
        {
            int width = serializedGrid.GetLength(0);
            int height = serializedGrid.GetLength(1);
            ObjectGrid<T> grid = new ObjectGrid<T>(width, height, () => new T());

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int index = serializedGrid[x, y];
                    grid.SetValue(x, y, objectList[index]);
                }
            }
            return grid;
        }
    }
}