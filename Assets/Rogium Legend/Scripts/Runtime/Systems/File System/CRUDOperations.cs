using System;
using System.Collections.Generic;
using BoubakProductions.Safety;
using BoubakProductions.Systems.FileSystem;
using BoubakProductions.Systems.FileSystem.Serialization;
using Rogium.Editors.Core;

namespace Rogium.ExternalStorage
{
    /// <summary>
    /// Enables CRUD operations for assets on external storage.
    /// </summary>
    /// <typeparam name="T">The type of asset.</typeparam>
    /// <typeparam name="TS">Serialized form of the asset.</typeparam>
    public class CRUDOperations<T, TS> where T : AssetBase where TS : ISerializedObject<T>
    {
        private SaveableData data;
        private readonly Func<T,TS> newSerializedObject;

        public CRUDOperations(Func<T,TS> newSerializedObject) => this.newSerializedObject = newSerializedObject;

        /// <summary>
        /// Saves a room under the currently edited pack.
        /// </summary>
        /// <param name="asset">The room to save.</param>
        public void Save(T asset)
        {
            SafetyNet.EnsureIsNotNull(data, "Saveable Data");
            
            data.AddFilePath(asset.ID, asset.Title);
            FileSystem.SaveFile(data.GetFilePath(asset.ID), asset, r => newSerializedObject(r));
        }

        /// <summary>
        /// Loads all assets.
        /// </summary>
        /// <returns>A list of those assets.</returns>
        public IList<T> LoadAll()
        {
            IList<T> loadedData = FileSystem.LoadAllFiles<T, TS>(data.Path, data.Extension);
            foreach (T piece in loadedData)
            {
                data.AddFilePath(piece.ID, piece.Title);
            }
            return loadedData;
        }
        
        /// <summary>
        /// Updates the title of a room under the currently edited pack.
        /// </summary>
        /// <param name="asset">The room, whose title to update.</param>
        public void UpdateTitle(T asset)
        {
            SafetyNet.EnsureIsNotNull(data, "Saveable Data");
            
            data.GetFileTitleAndPath(asset.ID, out string title, out string oldPath);
            if (asset.Title == title) return;

            data.UpdateFileTitle(asset.ID, asset.Title);
            FileSystem.RenameFile(oldPath, data.GetFilePath(asset.ID));
        }
        
        /// <summary>
        /// Deletes a room from the currently edited pack.
        /// </summary>
        /// <param name="asset">The room to delete.</param>
        public void Delete(T asset)
        {
            SafetyNet.EnsureIsNotNull(data, "Saveable Data");
            
            FileSystem.DeleteFile(data.GetFilePath(asset.ID));
            data.RemoveFilePath(asset.ID);
        }
        
        /// <summary>
        /// Updates CRUD information.
        /// </summary>
        /// <param name="saveableData">New data to read from.</param>
        public void RefreshSaveableData(SaveableData saveableData)
        {
            FileSystem.CreateDirectory(saveableData.Path);
            data = saveableData;
        }
    }
}