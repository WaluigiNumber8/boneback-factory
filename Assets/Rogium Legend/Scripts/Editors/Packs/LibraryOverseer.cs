using BoubakProductions.Safety;
using RogiumLegend.ExternalStorage;

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
            library.ReplaceAll(ExternalStorageOverseer.Instance.LoadAllPacks());
        }

        /// <summary>
        /// Creates a new Pack, and adds it to the library.
        /// </summary>
        public void CreateAndAddPack()
        {
            PackAsset newPack = new PackAsset();
            library.Add(newPack);
        }

        /// <summary>
        /// Prepare one of the packs in the library for editing.
        /// </summary>
        public void ActivatePackEditor(int packIndex)
        {
            SafetyNet.EnsureListIsNotEmptyOrNull(library, "Pack Library");
            SafetyNet.EnsureIntIsInRange(packIndex, 0, library.Count, "packIndex for activating Pack Editor");
            PackEditorOverseer.Instance.AssignNewPack(library[packIndex]);
        }

        public PackList Library { get => library; }
    }
}