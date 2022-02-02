using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Campaign;
using System.Collections.Generic;
using System.Linq;
using Rogium.UserInterface.AssetSelection;

namespace Rogium.Core
{
    /// <summary>
    /// Contains various helpful methods for working with <see cref="AssetBase"/> lists. 
    /// </summary>
    public static class AssetListExtensions
    {
        /// <summary>
        /// Finds the index of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="asset">The asset to compare to.</param>
        /// <typeparam name="T">Type is <see cref="AssetBase"/> or any of it's children.</typeparam>
        /// <returns>Index of the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static int FindIndexFirst<T>(this IList<T> list, AssetBase asset) where T : AssetBase
        {
            return FindIndexFirst(list, asset.ID);
        }
        
        /// <summary>
        /// Finds the index of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <typeparam name="T">Type is <see cref="AssetBase"/> or any of it's children.</typeparam>
        /// <returns>Index of the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        public static int FindIndexFirst<T>(this IList<T> list, string id) where T : AssetBase
        {
            SafetyNet.EnsureIsNotNull(list, "List ot search");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == id)
                {
                    return i;
                }
            }

            throw new SafetyNetCollectionException($"No asset with the ID '{id}' was not found in the list.");
        }

        /// <summary>
        /// Tries to find and copy entries from one list to another.
        /// </summary>
        /// <param name="list">The list to search on for same assets.</param>
        /// <param name="ids">The list asset ids which will be used to search for.</param>
        /// <typeparam name="T">Type is <see cref="AssetBase"/> or any of it's children.</typeparam>
        /// <returns>A List of the found assets.</returns>
        public static IList<T> GrabBasedOn<T>(this IList<T> list, IList<string> ids) where T : AssetBase
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
        /// Converts a list of assets into a list of their IDs.
        /// </summary>
        /// <param name="assets">The list of assets to convert</param>
        /// <typeparam name="T">Type is <see cref="AssetBase"/> or any of it's children.</typeparam>
        /// <returns>A list of IDs (strings).</returns>
        public static IList<string> ConvertToIDs<T>(this IList<T> assets) where T : AssetBase
        {
            return assets.Select(asset => asset.ID).ToList();
        }

        /// <summary>
        /// Converts a list of assets into a list of their IDs.
        /// </summary>
        /// <param name="importInfos">The list of <see cref="PackImportInfo"/>s to take IDs from.</param>
        /// <returns>A list of IDs (strings).</returns>
        public static IList<string> ConvertToIDs(this IList<PackImportInfo> importInfos)
        {
            return importInfos.Select(importInfo => importInfo.ID).ToList();
        }
        /// <summary>
        /// Converts a list of assets into a list of their IDs.
        /// </summary>
        /// <param name="holders">The list of <see cref="AssetHolderBase"/>s to take IDs from.</param>
        /// <returns>A list of IDs (strings).</returns>
        public static IList<string> ConvertToIDList<T>(this IList<T> holders) where T : IAssetHolder
        {
            return holders.Select(holder => holder.Asset.ID).ToList();
        }

        /// <summary>
        /// Converts a list of <see cref="AssetBase"/> children into a list of their parent.
        /// </summary>
        /// <param name="list">The list to convert</param>
        /// <typeparam name="T">Should be children of <see cref="AssetBase"/>. Otherwise will skip conversion.</typeparam>
        /// <returns>The list converted fully to <see cref="AssetBase"/>.</returns>
        public static IList<AssetBase> ConvertToAssetBase<T>(this IList<T> list) where T : AssetBase
        {
            if (typeof(T) == typeof(AssetBase)) return (IList<AssetBase>)list;
            return list.Cast<AssetBase>().ToList();
        }
    }
}