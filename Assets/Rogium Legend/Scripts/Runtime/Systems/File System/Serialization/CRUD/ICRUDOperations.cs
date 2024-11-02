using System.Collections.Generic;
using RedRats.Systems.FileSystem;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage
{
    public interface ICRUDOperations<T, TS> where T : IDataAsset where TS : IEncodedObject<T>
    {
        /// <summary>
        /// Saves an asset under the currently edited pack.
        /// </summary>
        /// <param name="asset">The asset to save.</param>
        public void Save(T asset);
        
        /// <summary>
        /// Load an asset.
        /// </summary>
        /// <param name="asset">The asset to load.</param>
        public T Load(T asset);

        /// <summary>
        /// Loads all assets.
        /// </summary>
        /// <returns>A list of those assets.</returns>
        public IList<T> LoadAll();

        /// <summary>
        /// Updates the asset under the currently edited pack.
        /// </summary>
        /// <param name="asset">The asset to update.</param>
        public void Update(T asset);

        /// <summary>
        /// Deletes an asset from the currently edited pack.
        /// </summary>
        /// <param name="asset">The asset to delete.</param>
        public void Delete(T asset);

        /// <summary>
        /// Updates CRUD information.
        /// </summary>
        /// <param name="saveableData">New data to read from.</param>
        public void RefreshSaveableData(SaveableData saveableData);
    }
}