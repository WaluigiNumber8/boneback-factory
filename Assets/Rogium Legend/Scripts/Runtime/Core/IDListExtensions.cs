using System;
using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;
using Rogium.Editors.Core;

namespace Rogium.Core
{
    /// <summary>
    /// Contains various helpful methods for working with <see cref="IIDHolder"/> lists. 
    /// </summary>
    public static class IDListExtensions
    {
        /// <summary>
        /// Finds the index of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="asset">The asset to compare to.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <returns>Index of the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static int FindIndexFirst<T>(this IList<T> list, IIDHolder asset) where T : IIDHolder
        {
            return FindIndexFirst(list, asset.ID);
        }
        
        /// <summary>
        /// Finds the index of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <returns>Index of the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static int FindIndexFirst<T, TS>(this IList<T> list, TS id) where T : IIDHolder where TS : IComparable
        {
            SafetyNet.EnsureIsNotNull(list, "List ot search");
            for (int i = 0; i < list.Count; i++)
            {
                if (id.CompareTo(list[i].ID) != 0) continue;
                return i;
            }

            throw new SafetyNetCollectionException($"No asset position with the ID '{id}' was found in the list.");
        }

        /// <summary>
        /// Finds the value of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <returns>Value the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static T FindValueFirst<T, TS>(this IEnumerable<T> list, TS id) where T : IIDHolder where TS : IComparable
        {
            SafetyNet.EnsureIsNotNull(list, "List ot search");
            foreach (T value in list)
            {
                if (id.CompareTo(value.ID) != 0) continue;
                return value;
            }

            throw new SafetyNetCollectionException($"No asset with the ID '{id}' was found in the list.");
        }
        

        /// <summary>
        /// Tries to find and copy entries from one list to another.
        /// </summary>
        /// <param name="list">The list to search on for same assets.</param>
        /// <param name="ids">The list asset ids which will be used to search for.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <returns>A List of the found assets.</returns>
        public static IList<T> GrabBasedOn<T>(this IList<T> list, IList<string> ids) where T : IIDHolder
        {
            IList<T> foundAssets = new List<T>();
            IList<string> idList = new List<string>(ids);
            for (int i = 0; i < list.Count; i++)
            {
                if (idList.Count <= 0) break;
                for (int j = 0; j < idList.Count; j++)
                {
                    if (idList[j] == list[i].ID)
                    {
                        foundAssets.Add(list[i]);
                        idList.RemoveAt(j);
                        break;
                    }
                }
            }

            return foundAssets;
        }

        /// <summary>
        /// Converts a list of ID holders into a list of their IDs.
        /// </summary>
        /// <param name="assets">The list of holders to convert.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <returns>A list of IDs (strings).</returns>
        public static IList<string> ConvertToIDs<T>(this IEnumerable<T> assets) where T : IIDHolder
        {
            return assets.Select(asset => asset.ID).ToList();
        }
        
    }
}