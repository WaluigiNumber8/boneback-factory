using RogiumLegend.Editors.ExternalStorage;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RogiumLegend.Editors.PackData
{
    /// <summary>
    /// Overseers the in-game pack library and controls it's content.
    /// </summary>
    public class PackLibraryOverseer
    {
        private List<PackAsset> library;

        #region Singleton Pattern
        private static PackLibraryOverseer instance;
        private static readonly object padlock = new object();
        public static PackLibraryOverseer Instance 
        { 
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new PackLibraryOverseer();
                    return instance;
                }
            }
        }


        #endregion

        private PackLibraryOverseer() 
        {
            library = new List<PackAsset>();
        }

        /// <summary>
        /// Adds a new pack to the library.
        /// </summary>
        /// <param name="newPack">The PackAsset to add.</param>
        public void AddPackToLibrary(PackAsset newPack)
        {
            library.Add(newPack);
            ExternalStorageOverseer.Instance.SavePack(newPack);
        }

        public void RemovePackByName(string name)
        {
            PackAsset foundPack = FindPackByName(name);
            library.Remove(foundPack);
        }
        
        /// <summary>
        /// Finds a pack in the library by a given name.
        /// 
        /// TODO - Solution for multiple packs with the same name.
        /// 
        /// </summary>
        /// <param name="packName">The name by which we are searching</param>
        /// <returns>The pack asset with the given name</returns>
        public PackAsset FindPackByName(string packName)
        {
            PackAsset foundPack = library.Where(pack => pack.packName == packName).SingleOrDefault();

            if (foundPack == null)
                throw new MissingReferenceException($"Pack '{packName}' was not found in the library.");
            else return foundPack;
        }

        /// <summary>
        /// Returns the number of packs currently loaded in the library.
        /// </summary>
        public int GetPackAmount => library.Count;
    }
}