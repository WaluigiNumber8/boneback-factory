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
    public static class IDCollectionExtensions
    {
        /// <summary>
        /// Finds the index of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="asset">The asset to compare to.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <returns>Index of the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static int FindIndex<T>(this IList<T> list, IIDHolder asset) where T : IIDHolder
        {
            return FindIndex(list, asset.ID);
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
        public static int FindIndex<T, TS>(this IList<T> list, TS id) where T : IIDHolder where TS : IComparable
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
        /// <typeparam name="T">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <typeparam name="TS">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <returns>Value the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static TS FindValue<T, TS>(this IEnumerable<TS> list, T id) where T : IComparable where TS : IIDHolder
        {
            SafetyNet.EnsureIsNotNull(list, "List ot search");
            foreach (TS value in list)
            {
                if (id.CompareTo(value.ID) != 0) continue;
                return value;
            }

            throw new SafetyNetCollectionException($"No asset with the ID '{id}' was found in the list.");
        }

        
        /// <summary>
        /// Finds the value of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="title">The Title of the asset to search for.</param>
        /// <param name="author">The Author of the asset to search for.</param>
        /// <typeparam name="T">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <returns>Value the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static T FindAsset<T, TS>(this IEnumerable<T> list, TS title, TS author) where T : IAsset where TS : IComparable
        {
            SafetyNet.EnsureIsNotNull(list, "List ot search");
            foreach (T value in list)
            {
                if (title.CompareTo(value.Title) != 0) continue;
                if (author.CompareTo(value.Author) != 0) continue;
                return value;
            }

            throw new SafetyNetCollectionException($"No asset with the Title '{title}' and Author '{author}' was found in the list.");
        }
        
        /// <summary>
        /// Tries to finds the value of the first asset with the same ID. If it fails then tries to return the first value of the collection.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <typeparam name="T">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <typeparam name="TS">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <returns>Value of the first found asset or the first value of the collection.</returns>
        public static TS FindValueOrReturnFirst<T, TS>(this IList<TS> list, T id) where T : IComparable where TS : IIDHolder
        {
            SafetyNet.EnsureIsNotNull(list, "List ot search");
            SafetyNet.EnsureIntIsBiggerOrEqualTo(list.Count, 1, nameof(list));
            
            try { return list.FindValue(id); }
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
        public static T FindValueOrDefault<T, TS>(this IList<T> list, TS id) where T : IIDHolder where TS : IComparable
        {
            SafetyNet.EnsureIsNotNull(list, "List ot search");
            
            try { return list.FindValue(id); }
            catch (SafetyNetCollectionException)
            {
                return default;
            }

        }
        
        /// <summary>
        /// Tries to finds the value of the first asset with the same ID. If it fails then tries to return the first value of the collection.
        /// If that fails as well, returns default.
        /// </summary>
        /// <param name="collection">The collection to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <typeparam name="T">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <typeparam name="TS">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <returns>Value of the first found asset or the first value of the collection or default.</returns>
        public static TS FindValueOrReturnFirstOrDefault<T, TS>(this IList<TS> collection, T id) where T : IComparable where TS : IIDHolder
        {
            SafetyNet.EnsureIsNotNull(collection, "List ot search");
            
            try { return collection.FindValueOrReturnFirst(id); }
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
        /// <typeparam name="T">Any type of <see cref="IComparable"/>. (string, int, etc.)</typeparam>
        /// <typeparam name="TS">Type is <see cref="IIDHolder"/> or any of it's children.</typeparam>
        /// <returns>A List of the found assets.</returns>
        public static IList<TS> GrabBasedOn<T, TS>(this IList<TS> list, IList<T> ids) where T : IComparable where TS : IIDHolder
        {
            IList<TS> foundAssets = new List<TS>();
            foreach (T id in ids)
            {
                foreach (TS asset in list)
                {
                    if (id.CompareTo(asset.ID) != 0) continue;
                    foundAssets.Add(asset);
                    break;
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