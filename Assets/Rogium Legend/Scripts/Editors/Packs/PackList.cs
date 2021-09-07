using BoubakProductions.Safety;
using RogiumLegend.ExternalStorage;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RogiumLegend.Editors.PackData
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

        /// <summary>
        /// Adds a new pack to the library.
        /// </summary>
        /// <param name="pack">The PackAsset to add.</param>
        public void Add(PackAsset pack)
        {
            SafetyNet.EnsureListNotContains(this, pack, "List of Packs");
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
        /// Finds a pack in the library by a given name.
        /// 
        /// TODO - Solution for multiple packs with the same name.
        /// 
        /// </summary>
        /// <param name="packName">The name by which we are searching</param>
        /// <returns>The pack asset with the given name</returns>
        public IList<PackAsset> TryFinding(string packName)
        {
            IList<PackAsset> foundPacks = this.Where(pack => pack.PackInfo.packName == packName).ToList();
            return new List<PackAsset>(foundPacks);
        }

        /// <summary>
        /// Finds a pack in the library by a given name and a given author.
        /// If no pack is found, returns NULL.
        /// </summary>
        /// <param name="packName">The name by which we are searching</param>
        /// <param name="author">The name of the author who created the pack.</param>
        /// <returns>The pack asset with the given name</returns>
        public PackAsset TryFinding(string packName, string author)
        {
            IList<PackAsset> foundPacks = this.Where(pack => pack.PackInfo.packName == packName
                                                        && pack.PackInfo.author == author).ToList();

            SafetyNet.EnsureListIsNotLongerThan(foundPacks, 1, "Found Packs by name & author");
            if (foundPacks.Count == 0) return null;
            return foundPacks[0];
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