using RogiumLegend.Editors.PackData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogiumLegend.Editors.ExternalStorage
{
    /// <summary>
    /// Overseers files stored on external storage.
    /// </summary>
    public class ExternalStorageOverseer : MonoBehaviour
    {
        private string packFilesPath;

        private PackSaveLoadOverseer saveLoad;

        #region Singleton Pattern
        private static ExternalStorageOverseer instance;
        private static readonly object padlock = new object();
        public static ExternalStorageOverseer Instance 
        { 
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new ExternalStorageOverseer();
                    return instance;
                }
            }
        }


        #endregion

        private ExternalStorageOverseer()
        {
            saveLoad = new PackSaveLoadOverseer();
        }

        /// <summary>
        /// Is called whenever there's a need for changing the packFilesPath.
        /// </summary>
        /// <param name="newPath">New location for saving pack files.</param>
        public void UpdatePackFilesPath(string newPath)
        {
            packFilesPath = newPath;
        }

        /// <summary>
        /// Wrapper for saving a pack to external storage. Use packFilePath.
        /// </summary>
        /// <param name="pack"></param>
        public void SavePack(PackAsset pack)
        {
            saveLoad.SavePackToStorage(packFilesPath, pack);
        }

        /// <summary>
        /// Wrapper for loading all packs from external storage. Use packFilePath.
        /// </summary>
        /// <returns></returns>
        public List<PackAsset> LoadAllPacks()
        {
            return saveLoad.LoadPacksFromPath(packFilesPath);
        }
    }
}