using RogiumLegend.Editors.PackData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogiumLegend.Editors.ExternalStorage
{
    /// <summary>
    /// Handles Saving and Loading packs from external storage.
    /// </summary>
    public class PackSaveLoadOverseer
    {

        /// <summary>
        /// Save pack to external storage.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        /// <param name="pack">The pack to save.</param>
        public void SavePackToStorage(string path, PackAsset pack)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Loads all packs under a path.
        /// </summary>
        /// <param name="path">Destination of the save.</param>
        public List<PackAsset> LoadPacksFromPath(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}