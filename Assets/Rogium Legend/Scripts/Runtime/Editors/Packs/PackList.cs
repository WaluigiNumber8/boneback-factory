using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;
using Rogium.ExternalStorage;

namespace Rogium.Editors.PackData
{
    /// <summary>
    /// Special distinct List variant. Is synced with external storage.
    /// </summary>
    public class PackList : IList<PackAsset>
    {
        private IList<PackAsset> list;

        public PackList()
        {
            list = new List<PackAsset>();
        }
        public PackList(PackList original)
        {
            list = new List<PackAsset>(original);
        }

        /// <summary>
        /// Adds a new pack to the library.
        /// </summary>
        /// <param name="pack">The PackAsset to add.</param>
        public void Add(PackAsset pack)
        {
            if (TryFinding(pack.PackInfo.Title, pack.PackInfo.Author) != null)
                throw new FoundDuplicationException("You are trying to create a pack, that already exists. Cannot have the same title and author!");
            
            ExternalStorageOverseer.Instance.Save(pack);
            list.Add(pack);
        }

        /// <summary>
        /// Remove pack from the library with a specific name.
        /// </summary>
        /// <param name="name">The name of the pack to remove.</param>
        public void Remove(string name, string author)
        {
            PackAsset foundPack = TryFinding(name, author);
            SafetyNet.EnsureIsNotNull(foundPack, "Pack to Remove");

            ExternalStorageOverseer.Instance.Delete(foundPack);
            list.Remove(foundPack);
        }

        /// <summary>
        /// Remove pack from the library with a specific name.
        /// </summary>
        /// <param name="packIndex">The position of the pack to remove on the list.</param>
        public void Remove(int packIndex)
        {
            SafetyNet.EnsureIntIsInRange(packIndex, 0, list.Count, "Pack Index when removing");
            PackAsset foundPack = list[packIndex];
            SafetyNet.EnsureIsNotNull(foundPack, "Pack to Remove");

            ExternalStorageOverseer.Instance.Delete(foundPack);
            list.Remove(foundPack);
        }

        /// <summary>
        /// Finds a pack in the library by a given name and a given author.
        /// If no pack is found, returns NULL.
        /// </summary>
        /// <param name="packName">The name by which we are searching</param>
        /// <param name="author">The name of the author who created the pack.</param>
        /// <param name="excludePos">The position in the list to exclude.</param>
        /// <returns>The pack asset with the given name</returns>
        public PackAsset TryFinding(string packName, string author, int excludePos = -1)
        {
            IList<PackAsset> foundPacks = list.Where((pack, counter) => counter != excludePos)
                                              .Where((pack) => pack.PackInfo.Title == packName && pack.PackInfo.Author == author)
                                              .ToList();

            SafetyNet.EnsureListIsNotLongerThan(foundPacks, 1, "Found Packs by name & author");
            return (foundPacks.Count == 0) ? null : foundPacks[0];
        }

        /// <summary>
        /// Replaces the current list with a new one.
        /// </summary>
        /// <param name="newList">The list to replace it with.</param>
        public void ReplaceAll(IList<PackAsset> newList)
        {
            list = new List<PackAsset>(newList);
        }

        #region Untouched Overrides
        public int Count => list.Count;
        public bool IsReadOnly => false;
        public PackAsset this[int index] { get => list[index]; set => list[index] = value; }

        public int IndexOf(PackAsset item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, PackAsset item)
        {
            SafetyNet.EnsureListNotContains(this, item, "List of Packs");
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(PackAsset item)
        {
            return list.Contains(item);
        }

        public void CopyTo(PackAsset[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(PackAsset item)
        {
            return list.Remove(item);
        }

        public IEnumerator<PackAsset> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
        #endregion
    }
}