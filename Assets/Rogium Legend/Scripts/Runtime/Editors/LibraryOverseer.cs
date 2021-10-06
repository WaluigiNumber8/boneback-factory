using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.ExternalStorage;

namespace Rogium.Editors.PackData
{
    /// <summary>
    /// Overseers the in-game pack library and controls it's content.
    /// This library is also synced with pack data files located on the external hard drive.
    /// </summary>
    public class LibraryOverseer
    {
        private readonly PackList library;

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
            library = new PackList();
            ReloadFromExternalStorage();
        }

        /// <summary>
        /// Repopulates the library with all packs located on external storage.
        /// </summary>
        public void ReloadFromExternalStorage()
        {
            library.ReplaceAll(ExternalStorageOverseer.Instance.LoadAllPacks());
        }
        
        /// <summary>
        /// Creates a new Pack, and adds it to the library.
        /// </summary>
        /// <param name="packInfo">Information about the pack.</param>
        public void CreateAndAddPack(PackInfoAsset packInfo)
        {
            PackAsset newPack = new PackAsset(packInfo);
            library.Add(newPack);
        }

        /// <summary>
        /// Remove pack from the library.
        /// </summary>
        /// <param name="packIndex">Pack ID in the list.</param>
        public void RemovePack(int packIndex)
        {
            library.Remove(packIndex);
        }
        /// <summary>
        /// Remove pack from the library.
        /// </summary>
        /// <param name="title">Pack's Title</param>
        /// <param name="author">Pack's Author</param>
        public void RemovePack(string title, string author)
        {
            library.Remove(title, author);
        }

        /// <summary>
        /// Prepare one of the packs in the library for editing.
        /// </summary>
        public void ActivatePackEditor(int packIndex)
        {
            SafetyNet.EnsureListIsNotEmptyOrNull(library, "Pack Library");
            SafetyNet.EnsureIntIsInRange(packIndex, 0, library.Count, "packIndex for activating Pack Editor");
            EditorOverseer.Instance.AssignNewPack(library[packIndex]);
        }

        /// <summary>
        /// Amount of packs, stored in the library.
        /// </summary>
        public int PackCount => library.Count;

        /// <summary>
        /// Returns a copy of the list of packs stored here.
        /// </summary>
        /// <returns>A copy of Pack Library.</returns>
        public PackList GetCopy => new PackList(library);
    }
}