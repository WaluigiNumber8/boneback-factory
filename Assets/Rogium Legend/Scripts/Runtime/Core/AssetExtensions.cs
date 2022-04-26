using System.Collections.Generic;
using BoubakProductions.Safety;
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
        public static IList<T> ConvertToAssets<T>(this IEnumerable<string> ids, IList<T> allAssets, bool keepEmpty = false) where T : IIDHolder
        {
            IList<T> assets = new List<T>();
            string lastID = EditorDefaults.EmptyAssetID;
            T lastAsset = default;
            
            foreach (string id in ids)
            {
                if (id != EditorDefaults.EmptyAssetID)
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
    }
}