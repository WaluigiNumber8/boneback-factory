using System.Collections.Generic;
using System.Linq;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;

namespace Rogium.Core
{
    /// <summary>
    /// Contains extension methods for Assets.
    /// </summary>
    public static class AssetExtensions
    {
        /// <summary>
        /// Converts a list of IDs into a list of assets.
        /// </summary>
        /// <param name="ids">The collection of IDs to convert.</param>
        /// <param name="allAssets">Where to take assets from.</param>
        /// <param name="keepEmpty">If on, will add assets with empty or not found IDs as null.</param>
        /// <typeparam name="T">Any type of <see cref="IIDHolder"/>.</typeparam>
        /// <returns>A List of assets.</returns>
        public static IList<T> ConvertToAssets<T>(this IEnumerable<string> ids, ICollection<T> allAssets, bool keepEmpty = false) where T : IIDHolder
        {
            if (allAssets == null || allAssets.Count == 0) return new List<T>();

            IList<T> assets = new List<T>();
            string lastID = EditorDefaults.EmptyAssetID;
            T lastAsset = default;

            foreach (string id in ids)
            {
                if (id == EditorDefaults.EmptyAssetID)
                {
                    if (!keepEmpty) assets.Add(default);
                    continue;
                }

                if (lastID != EditorDefaults.EmptyAssetID && lastID == id)
                {
                    assets.Add(lastAsset);
                    continue;
                }

                try
                {
                    lastAsset = allAssets.FindValueFirst(id);
                    lastID = lastAsset.ID;
                    assets.Add(lastAsset);
                }
                catch (SafetyNetCollectionException)
                {
                    if (!keepEmpty) assets.Add(default);
                }
            }

            return assets;
        }

        public static T ConvertToAsset<T>(this string id, ICollection<T> allAssets, bool keepEmpty = false) where T : IIDHolder
        {
            if ((allAssets == null || allAssets.Count == 0) && !keepEmpty) return default;
            if (id == EditorDefaults.EmptyAssetID && !keepEmpty) return default;
            try
            {
                return allAssets.FindValueFirst(id);
            }
            catch (SafetyNetCollectionException)
            {
                if (!keepEmpty) return default;
            }
            throw new SafetyNetCollectionException($"Asset with id '{id}' not found.");
        }

        /// <summary>
        /// Tries to find all assets based on the IDs. <br/>
        /// If an asset is not found, returns an empty asset.
        /// </summary>
        /// <param name="ids">The IDs of the assets to find.</param>
        /// <param name="assets">The collection of assets to search through</param>
        public static IList<IAsset> TryGetAssets<T>(this ICollection<string> ids, ICollection<T> assets) where T : IAsset
        {
            if (assets == null || assets.Count == 0) return new List<IAsset>();
            return ids.Select(id => id.TryGetAsset(assets)).ToList();
        }

        /// <summary>
        /// Tries to get the first asset with the given ID from the list. <br/>
        /// If not found, returns an empty asset.
        /// </summary>
        /// <param name="id">The ID of the asset to find.</param>
        /// <param name="assets">The collection of assets to search through</param>
        /// <returns></returns>
        public static IAsset TryGetAsset<T>(this string id, ICollection<T> assets) where T : IAsset
        {
            IAsset grabbedAsset = id.ConvertToAsset(assets);
            grabbedAsset = (grabbedAsset.IsEmpty()) ? new EmptyAsset() : grabbedAsset;
            return grabbedAsset;
        }
        
        /// <summary>
        /// Returns TRUE if asset is null or has an empty ID.
        /// </summary>
        public static bool IsEmpty(this IIDHolder asset) => (asset == null || asset.ID == EditorDefaults.EmptyAssetID);

        /// <summary>
        /// Returns TRUE if the ID is empty or equal to the default empty ID.
        /// </summary>
        public static bool IsEmpty(this string id) => (string.IsNullOrEmpty(id) || id == EditorDefaults.EmptyAssetID);
    }
}