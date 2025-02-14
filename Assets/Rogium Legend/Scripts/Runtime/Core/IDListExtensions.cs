using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Safety;
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
            Preconditions.IsNotNull(list, "List ot search");
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
            Preconditions.IsNotNull(list, "List ot search");
            Preconditions.IsNotNull(id, "Value to find cannot be null");
            foreach (T value in list)
            {
                if (id.CompareTo(value.ID) != 0) continue;
                return value;
            }

            throw new SafetyNetCollectionException($"No asset with the ID '{id}' was found in the list.");
        }
        
        /// <summary>
        /// Finds the value and index of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <returns>Value the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static (T, int) FindValueAndIndexFirst<T, TS>(this IList<T> list, TS id) where T : IIDHolder where TS : IComparable
        {
            Preconditions.IsNotNull(list, "List ot search");
            for (int i = 0; i < list.Count; i++)
            {
                if (id.CompareTo(list[i].ID) != 0) continue;
                return (list[i], i);
            }

            throw new SafetyNetCollectionException($"No asset position with the ID '{id}' was found in the list.");
        }

        /// <summary>
        /// Tries to finds the value of the first asset with the same ID. If it fails then tries to return the first value of the collection.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <returns>Value of the first found asset or the first value of the collection.</returns>
        public static T FindValueFirstOrReturnFirst<T, TS>(this IList<T> list, TS id) where T : IIDHolder where TS : IComparable
        {
            Preconditions.IsNotNull(list, "List ot search");
            Preconditions.IsIntBiggerOrEqualTo(list.Count, 1, nameof(list));
            
            try { return list.FindValueFirst(id); }
            catch (SafetyNetCollectionException)
            {
                return list[0];
            }

        }
        
        /// <summary>
        /// Tries to finds the value of the first asset with the same ID. If it fails returns the default value.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <returns>Value of the first found asset or default.</returns>
        public static T FindValueFirstOrDefault<T, TS>(this IList<T> list, TS id) where T : IIDHolder where TS : IComparable
        {
            Preconditions.IsNotNull(list, "List ot search");
            
            try { return list.FindValueFirst(id); }
            catch (SafetyNetCollectionException)
            {
                return default;
            }

        }
        
        /// <summary>
        /// Tries to finds the value of the first asset with the same ID. If it fails then tries to return the first value of the collection.
        /// If that fails as well, returns default.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <returns>Value of the first found asset or the first value of the collection or default.</returns>
        public static T FindValueFirstOrReturnFirstOrDefault<T, TS>(this IList<T> list, TS id) where T : IIDHolder where TS : IComparable
        {
            Preconditions.IsNotNull(list, "List ot search");
            
            try { return list.FindValueFirstOrReturnFirst(id); }
            catch (SafetyNetCollectionException)
            {
                return default;
            }

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
        /// Copies entries from one set to another.
        /// </summary>
        /// <param name="set">The set to search on for same assets.</param>
        /// <param name="ids">The set asset ids which will be used to search for.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <returns>A Set of the found assets.</returns>
        public static ISet<T> GrabBasedOn<T>(this ISet<T> set, ISet<string> ids) where T : IIDHolder => new HashSet<T>(set.Where(asset => ids.Contains(asset.ID)));

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
        
        /// <summary>
        /// Converts a list of Data Assets into a list of their Titles.
        /// </summary>
        /// <param name="assets">The list of data assets to convert.</param>
        /// <typeparam name="T">Type is <see cref="IDataAsset"/> or any of it's children.</typeparam>
        /// <returns>A list of Titles (strings).</returns>
        public static IList<string> ConvertToTitles<T>(this IEnumerable<T> assets) where T : IDataAsset
        {
            return assets.Select(asset => asset.Title).ToList();
        }
        
    }
}