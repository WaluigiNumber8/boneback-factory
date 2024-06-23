using RedRats.Safety;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Special distinct List variant. Is synced with external storage.
    /// </summary>
    public class AssetList<T> : IList<T> where T : IAsset
    {
        private List<T> list;
        private readonly Action<T> saveToExternalStorage;
        private readonly Action<T> updateOnExternalStorage;
        private readonly Action<T> deleteFromExternalStorage;

        #region Constructors
        public AssetList(Action<T> saveToExternalStorage, Action<T> updateOnExternalStorage, Action<T> deleteFromExternalStorage) : this(saveToExternalStorage, updateOnExternalStorage, deleteFromExternalStorage, new List<T>()) { }
        public AssetList(Action<T> saveToExternalStorage, Action<T> updateOnExternalStorage, Action<T> deleteFromExternalStorage, IList<T> original)
        {
            SafetyNet.EnsureIsNotNull(original, "List to use/copy");
            SafetyNet.EnsureIsNotNull(saveToExternalStorage, "Saving Method");
            SafetyNet.EnsureIsNotNull(updateOnExternalStorage, "Updating Method");
            SafetyNet.EnsureIsNotNull(deleteFromExternalStorage, "Deleting Method");

            list = new List<T>(original);
            this.saveToExternalStorage = saveToExternalStorage;
            this.updateOnExternalStorage = updateOnExternalStorage;
            this.deleteFromExternalStorage = deleteFromExternalStorage;
        }

        #endregion

        /// <summary>
        /// Adds a new pack to the library.
        /// </summary>
        /// <param name="asset">the PackAsset to add.</param>
        public void Add(T asset)
        {
            SafetyNet.EnsureIsNotNull(asset, "Asset to add");
            if (TryFinding(asset.Title, asset.Author) != null)
            {
                SafetyNet.ThrowMessage("The asset cannot have the same name as an already existing one.");
                throw new FoundDuplicationException("You are trying to create an asset, that already exists. Cannot have the same title and author!");
            }
            list.Add(asset);
            saveToExternalStorage.Invoke(asset);
        }

        /// <summary>
        /// Adds a list of assets to this list without saving.
        /// </summary>
        /// <param name="assets"></param>
        public void AddAllWithoutSave(IList<T> assets)
        {
            SafetyNet.EnsureIsNotNull(assets, "List of assets to add.");
            
            List<T> newAssets = new List<T>(list);
            newAssets.AddRange(assets);
            
            SafetyNet.EnsureListDoesNotHaveDuplicities(newAssets, "Newly Added Assets");
            
            list.AddRange(assets);
        }
        
        /// <summary>
        /// Change an asset on a specific index in the list.
        /// </summary>
        /// <param name="index">Index of the asset to change.</param>
        /// <param name="asset">The new asset</param>
        /// <exception cref="FoundDuplicationException"></exception>
        public void Update(int index, T asset)
        {
            SafetyNet.EnsureIsNotNull(asset, "Asset to add");
            SafetyNet.EnsureIntIsInRange(index ,0, list.Count, "Asset List");
            if (TryFinding(asset.Title, asset.Author, index) != null)
            {
                SafetyNet.ThrowMessage("The asset cannot have the same name as an already existing one.");
                throw new FoundDuplicationException("You are trying to create an asset, that already exists. Cannot have the same title and author!");
            }
            list[index] = asset;
            
            updateOnExternalStorage.Invoke(asset);
            saveToExternalStorage.Invoke(asset);
        }
        
        /// <summary>
        /// Remove an asset from the library with a specific name.
        /// </summary>
        /// <param name="name">the name of the asset to remove.</param>
        /// <param name="author">the author of the asset.</param>
        public void Remove(string name, string author)
        {
            T foundAsset = TryFinding(name, author);
            SafetyNet.EnsureIsNotNull(foundAsset, "Asset to Remove");

            list.Remove(foundAsset);
            deleteFromExternalStorage.Invoke(foundAsset);
        }

        /// <summary>
        /// Remove an asset from the library with a specific name.
        /// </summary>
        /// <param name="assetIndex"></param>
        public void Remove(int assetIndex)
        {
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, list.Count, "Pack Index when removing");
            T foundAsset = list[assetIndex];
            SafetyNet.EnsureIsNotNull(foundAsset, "Asset to Remove");

            list.Remove(foundAsset);
            deleteFromExternalStorage.Invoke(foundAsset);
        }
        
        /// <summary>
        /// Finds a pack in the library by a given name and a given author.
        /// If no pack is found, returns NULL.
        /// </summary>
        /// <param name="title">the name by which we are searching</param>
        /// <param name="author">the name of the author who created the pack.</param>
        /// <param name="excludePos">The position on the list exclude from checking.</param>
        /// <returns>the pack asset with the given name.</returns>
        private T TryFinding(string title, string author, int excludePos = -1)
        {
            if (list.Count == 0) return default;
            IList<T> foundAssets = list.Where((_, counter) => counter != excludePos)
                                       .Where(asset => asset.Title == title && asset.Author == author)
                                       .ToList();

            SafetyNet.EnsureListIsNotLongerThan(foundAssets, 1, "Found Packs by name & author");
            return (foundAssets.Count == 0) ? default : foundAssets[0];
        }

        /// <summary>
        /// Replaces the current list with a new one.
        /// </summary>
        /// <param name="newList">the list to replace it with.</param>
        public void ReplaceAll(IList<T> newList)
        {
            list = new List<T>(newList);
        }

        #region Untouched Overrides
        public int Count => list.Count;
        public bool IsReadOnly => false;

        T IList<T>.this[int index] { get => list[index]; set => throw new System.InvalidOperationException("Cannot assign to this list directly. Use Update() instead."); }
        public T this[int index] { get => list[index]; set => list[index] = value; }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            SafetyNet.EnsureListNotContains(this, item, "List of Packs");
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            SafetyNet.EnsureIntIsInRange(index, 0, list.Count, "Index");
            Remove(index);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            return list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return list.GetEnumerator();
        }
        #endregion
    }
}