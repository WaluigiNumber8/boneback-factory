using RogiumLegend.ExternalStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogiumLegend.Editors.PackData
{
    /// <summary>
    /// Overseers the in-game pack library and controls it's content.
    /// This library is also synced with pack data files located on the external harddrive.
    /// </summary>
    public class LibraryOverseer
    {
        private PackList library = new PackList();

        #region Singleton Pattern
        private static LibraryOverseer instance;
        private static readonly object padlock = new object();
        public static LibraryOverseer Instance 
        { 
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new LibraryOverseer();
                    return instance;
                }
            }
        }
        #endregion

        private LibraryOverseer() 
        {
            ReloadFromExternalStorage();
        }

        /// <summary>
        /// Repopulates the library with all packs located on external storage.
        /// </summary>
        public void ReloadFromExternalStorage()
        {
            library.Replace(ExternalStorageOverseer.Instance.LoadAllPacks());
        }

        public PackList Library { get => library; }
    }
}