using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.GridSystem;

namespace Rogium.Core
{
    /// <summary>
    /// Contains various method that help working with assets.
    /// </summary>
    public static class AssetUtils
    {
        /// <summary>
        /// Call an action, using a loaded asset from a list based on a grid of IDs.
        /// </summary>
        /// <param name="IDGrid">The grid of IDs to read data from.</param>
        /// <param name="dataList">The list of data, corresponding to the ID grid.</param>
        /// <param name="whenIDSame">Method that gets called when an asset is found on a specific ID gird position. (x, y, value)</param>
        /// <param name="whenIDInvalid">Method that gets called when no asset with a specific ID was found on the list. (x, y)</param>
        /// <param name="whenIDEmpty">Method that gets called when ID is empty. (x, y, lastValue)</param>
        /// <param name="flipY">If on, the Y gets called flipped.</param>
        /// <typeparam name="T">Any type of <see cref="IIDHolder"/>.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IIDHolder"/>.</typeparam>
        public static void UpdateFromGridByList<T, TS>(ObjectGrid<TS> IDGrid, IList<T> dataList,
            Action<int, int, T, TS> whenIDSame,
            Action<int, int> whenIDInvalid,
            Action<int, int, T, TS> whenIDEmpty = null,
            bool flipY = false) where T : IIDHolder where TS : IIDHolder
        {
            T lastValue = default;
            for (int y = 0; y < IDGrid.Height; y++)
            {
                for (int x = 0; x < IDGrid.Width; x++)
                {
                    int Y = (flipY) ? IntUtils.Flip(y, IDGrid.Height) : y;
                    TS data = IDGrid.GetAt(x, y);

                    //Id is empty.
                    if (data.ID == EditorDefaults.EmptyAssetID)
                    {
                        if (lastValue == null) continue;
                        whenIDEmpty?.Invoke(x, Y, lastValue, data);
                        continue;
                    }

                    //Cache optimisation.
                    if (data.ID == lastValue?.ID)
                    {
                        whenIDSame?.Invoke(x, Y, lastValue, data);
                        continue;
                    }

                    //Try searching for id.
                    try
                    {
                        lastValue = dataList.FindValueFirst(data.ID);
                        whenIDSame?.Invoke(x, Y, lastValue, data);
                    }
                    //When the tile is missing.
                    catch (PreconditionCollectionException)
                    {
                        whenIDInvalid?.Invoke(x, Y);
                    }
                }
            }
        }

        /// <summary>
        /// Call an action, using a loaded asset from a list based on a grid of IDs.
        /// </summary>
        /// <param name="IDGrid">The grid of IDs to read data from.</param>
        /// <param name="dataList">The list of data, corresponding to the ID grid.</param>
        /// <param name="whenIDSame">Method that gets called when an asset is found on a specific ID gird position. (x, y, value)</param>
        /// <param name="whenIDInvalid">Method that gets called when no asset with a specific ID was found on the list. (x, y)</param>
        /// <param name="whenIDEmpty">Method that gets called when ID is empty. (x, y, lastValue)</param>
        /// <param name="flipY">If on, the Y gets called flipped.</param>
        /// <typeparam name="T">Any type of <see cref="IIDHolder"/>.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        public static void UpdateFromGridByList<T, TS>(ObjectGrid<TS> IDGrid, IList<T> dataList,
            Action<int, int, T> whenIDSame,
            Action<int, int> whenIDInvalid,
            Action<int, int, T> whenIDEmpty = null,
            bool flipY = false) where T : IIDHolder where TS : IComparable
        {
            T lastValue = default;
            for (int y = 0; y < IDGrid.Height; y++)
            {
                for (int x = 0; x < IDGrid.Width; x++)
                {
                    int Y = (flipY) ? IntUtils.Flip(y, IDGrid.Height) : y;
                    TS id = IDGrid.GetAt(x, y);

                    //Id is empty.
                    if (id.CompareTo(EditorDefaults.EmptyAssetID) == 0)
                    {
                        if (lastValue == null) continue;
                        whenIDEmpty?.Invoke(x, Y, lastValue);
                        continue;
                    }

                    //Cache optimisation.
                    if (lastValue != null && id.CompareTo(lastValue.ID) == 0)
                    {
                        whenIDSame?.Invoke(x, Y, lastValue);
                        continue;
                    }

                    //Try searching for id.
                    try
                    {
                        lastValue = dataList.FindValueFirst(id);
                        whenIDSame?.Invoke(x, Y, lastValue);
                    }
                    //When the tile is missing.
                    catch (PreconditionCollectionException)
                    {
                        whenIDInvalid?.Invoke(x, Y);
                    }
                }
            }
        }
    }
}