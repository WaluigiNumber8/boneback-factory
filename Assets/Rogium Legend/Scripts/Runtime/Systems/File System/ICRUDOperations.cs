using System.Collections.Generic;
using RedRats.Systems.FileSystem;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage
{
    public interface ICRUDOperations<T, TS> where T : IDataAsset where TS : IEncodedObject<T>
    {
        /// <summary>
        /// Saves a room under the currently edited pack.
        /// </summary>
        /// <param name="asset">The room to save.</param>
        void Save(T asset);

        /// <summary>
        /// Loads all assets.
        /// </summary>
        /// <returns>A list of those assets.</returns>
        IList<T> LoadAll();

        /// <summary>
        /// Updates the title of a room under the currently edited pack.
        /// </summary>
        /// <param name="asset">The room, whose title to update.</param>
        void UpdateTitle(T asset);

        /// <summary>
        /// Deletes a room from the currently edited pack.
        /// </summary>
        /// <param name="asset">The room to delete.</param>
        void Delete(T asset);

        /// <summary>
        /// Updates CRUD information.
        /// </summary>
        /// <param name="saveableData">New data to read from.</param>
        void RefreshSaveableData(SaveableData saveableData);
    }
}