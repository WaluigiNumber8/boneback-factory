using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.ExternalStorage;

namespace Rogium.Editors.PackData
{
    /// <summary>
    /// Special distinct List variant. Is synced with external storage.
    /// </summary>
    public class AssetList<T> : IList<T> where T : AssetBase
    {
        private IList<T> list;
        private readonly PackAsset pack;

        public AssetList(PackAsset pack)
        {
            SafetyNet.EnsureIsNotNull(pack, "Pack I belong to");

            list = new List<T>();
            this.pack = pack;
        }
        public AssetList(PackAsset pack, IList<T> original)
        {
            SafetyNet.EnsureIsNotNull(pack, "Pack I belong to");

            list = new List<T>(original);
            this.pack = pack;
        }

        /// <summary>
        /// Adds a new pack to the library.
        /// </summary>
        /// <param name="asset">the PackAsset to add.</param>
        public void Add(T asset)
        {
            SafetyNet.EnsureIsNotNull(asset, "Asset to add");
            if (TryFinding(asset.Title, asset.Author) != null)
                throw new FoundDuplicationException("You are trying to create an asset, that already exists. Cannot have the same title and author!");
            
            list.Add(asset);
            ExternalStorageOverseer.Instance.Save(pack);
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
                throw new FoundDuplicationException("You are trying to update an asset with a name and author that is already taken. Cannot have the same title and author!");
            list[index] = asset;
            
            ExternalStorageOverseer.Instance.Save(pack);
        }
        
        /// <summary>
        /// Remove pack from the library with a specific name.
        /// </summary>
        /// <param name="name">the name of the pack to remove.</param>
        /// <param name="author">the author of the asset.</param>
        public void Remove(string name, string author)
        {
            T foundAsset = TryFinding(name, author);
            SafetyNet.EnsureIsNotNull(foundAsset, "Asset to Remove");

            list.Remove(foundAsset);
            ExternalStorageOverseer.Instance.Save(pack);
        }

        /// <summary>
        /// Remove pack from the library with a specific name.
        /// </summary>
        /// <param name="packIndex"></param>
        public void Remove(int packIndex)
        {
            SafetyNet.EnsureIntIsInRange(packIndex, 0, list.Count, "Pack Index when removing");
            T foundAsset = list[packIndex];
            SafetyNet.EnsureIsNotNull(foundAsset, "Asset to Remove");

            list.Remove(foundAsset);
            ExternalStorageOverseer.Instance.Save(pack);
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
            IList<T> foundAssets = list.Where((asset, counter) => counter != excludePos)
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
            list.RemoveAt(index);
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