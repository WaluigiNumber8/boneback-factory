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
        private readonly List<T> list;
        private readonly Action<T> saveToExternalStorage;
        private readonly Action<T> updateOnExternalStorage;
        private readonly Action<T> deleteFromExternalStorage;

        #region Constructors
        public AssetList(Action<T> saveToExternalStorage, Action<T> updateOnExternalStorage, Action<T> deleteFromExternalStorage) : this(saveToExternalStorage, updateOnExternalStorage, deleteFromExternalStorage, new List<T>()) { }
        public AssetList(Action<T> saveToExternalStorage, Action<T> updateOnExternalStorage, Action<T> deleteFromExternalStorage, IList<T> original)
        {
            Preconditions.IsNotNull(original, "List to use/copy");
            Preconditions.IsNotNull(saveToExternalStorage, "Saving Method");
            Preconditions.IsNotNull(updateOnExternalStorage, "Updating Method");
            Preconditions.IsNotNull(deleteFromExternalStorage, "Deleting Method");

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
            Preconditions.IsNotNull(asset, "Asset to add");
            if (TryFinding(asset.Title, asset.Author) != null)
            {
                Preconditions.ThrowMessage("The asset cannot have the same name as an already existing one.");
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
            Preconditions.IsNotNull(assets, "List of assets to add.");
            
            List<T> newAssets = new List<T>(list);
            newAssets.AddRange(assets);
            
            Preconditions.isListWithoutDuplicates(newAssets, "Newly Added Assets");
            
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
            Preconditions.IsNotNull(asset, "Asset to add");
            Preconditions.IsIntInRange(index ,0, list.Count, "Asset List");
            if (TryFinding(asset.Title, asset.Author, index) != null)
            {
                Preconditions.ThrowMessage("The asset cannot have the same name as an already existing one.");
                throw new FoundDuplicationException("You are trying to create an asset, that already exists. Cannot have the same title and author!");
            }
            list[index] = asset;
            
            updateOnExternalStorage.Invoke(asset);
        }
        
        /// <summary>
        /// Remove an asset from the library with a specific name.
        /// </summary>
        /// <param name="name">the name of the asset to remove.</param>
        /// <param name="author">the author of the asset.</param>
        public void Remove(string name, string author)
        {
            T foundAsset = TryFinding(name, author);
            Preconditions.IsNotNull(foundAsset, "Asset to Remove");

            list.Remove(foundAsset);
            deleteFromExternalStorage.Invoke(foundAsset);
        }

        /// <summary>
        /// Remove an asset from the library with a specific name.
        /// </summary>
        /// <param name="assetIndex"></param>
        public void Remove(int assetIndex)
        {
            Preconditions.IsIntInRange(assetIndex, 0, list.Count, "Pack Index when removing");
            T foundAsset = list[assetIndex];
            Preconditions.IsNotNull(foundAsset, "Asset to Remove");

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

            Preconditions.IsListIsNotLongerThan(foundAssets, 1, "Found Packs by name & author");
            return (foundAssets.Count == 0) ? default : foundAssets[0];
        }

        /// <summary>
        /// Replaces the current list with a new one.
        /// </summary>
        /// <param name="newList">the list to replace it with.</param>
        public void ReplaceAll(IList<T> newList)
        {
            Preconditions.IsNotNull(newList, "New List");
            list.Clear();
            list.AddRange(newList);
        }

        public override string ToString() => $"Count: {list.Count}";

        #region Untouched Overrides
        public int Count => list.Count;
        public bool IsReadOnly => false;

        T IList<T>.this[int index] { get => list[index]; set => throw new System.InvalidOperationException("Cannot assign to this list directly. Use Update() instead."); }
        public T this[int index] { get => list[index]; set => list[index] = value; }

        public int IndexOf(T item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, T asset)
        {
            Preconditions.IsNotNull(asset, "Asset to add");
            if (TryFinding(asset.Title, asset.Author) != null)
            {
                Preconditions.ThrowMessage("The asset cannot have the same name as an already existing one.");
                throw new FoundDuplicationException("You are trying to create an asset, that already exists. Cannot have the same title and author!");
            }
            list.Insert(index, asset);
            saveToExternalStorage.Invoke(asset);
        }

        public void RemoveAt(int index)
        {
            Preconditions.IsIntInRange(index, 0, list.Count, "Index");
            Remove(index);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item) => list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        public bool Remove(T asset)
        {
            Preconditions.IsNotNull(asset, "Asset to add");
            if (TryFinding(asset.Title, asset.Author) != null)
            {
                Preconditions.ThrowMessage("The asset cannot have the same name as an already existing one.");
                throw new FoundDuplicationException("You are trying to create an asset, that already exists. Cannot have the same title and author!");
            }
            if (!list.Remove(asset)) return false;
            deleteFromExternalStorage.Invoke(asset);
            return true;
        }

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => list.GetEnumerator();

        #endregion
    }
}