using RogiumLegend.Editors.PackData;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RogiumLegend.ExternalStorage
{
    /// <summary>
    /// Overseers files stored on external storage.
    /// </summary>
    public class ExternalStorageOverseer
    {
        public readonly string localPacksFolderName = "Packs";
        public readonly string packExtension = ".pck";

        public readonly string packDirectoryPath;

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
            packDirectoryPath = Path.Combine(Application.persistentDataPath, localPacksFolderName);

            //Create pack directory if not exists.
            if (!Directory.Exists(packDirectoryPath))
                Directory.CreateDirectory(packDirectoryPath);
        }

        /// <summary>
        /// Wrapper for saving a pack to external storage. Uses packDirectoryPath.
        /// </summary>
        /// <param name="pack">The pack to save.</param>
        public void Save(PackAsset pack)
        {
            string savePath = Path.Combine(packDirectoryPath, pack.packName + packExtension);
            FileSystem.SavePackAsset(savePath, pack);
        }

        /// <summary>
        /// Wrapper for loading all packs from external storage. Uses packDirectoryPath.
        /// </summary>
        /// <returns></returns>
        public IList<PackAsset> LoadAllPacks()
        {
            return FileSystem.LoadPackAssets(packDirectoryPath);
        }

        /// <summary>
        /// Wrapper for removing a pack from external storage.
        /// <param name="pack">The pack to remove.</param>
        /// </summary>
        public void Delete(PackAsset pack)
        {
            string removePath = Path.Combine(packDirectoryPath, pack.packName + packExtension);
            FileSystem.DeletePack(removePath);
        }
    }
}