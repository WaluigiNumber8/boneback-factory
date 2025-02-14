using System;
using System.Collections.Generic;
using System.IO;
using RedRats.Safety;
using RedRats.Systems.FileSystem;
using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.ExternalStorage.Serialization;

namespace Rogium.ExternalStorage
{
    public class CRUDPackOperations : ICRUDOperations<PackAsset, JSONPackAsset>
    {
        private readonly IList<PackPathInfo> packPaths = new List<PackPathInfo>();
        private SaveableData packData;
        private PackPathInfo currentPackInfo;
        
        private readonly Action<PackPathInfo> refreshAssetSaveableData;
        private readonly Func<PackAsset, PackAsset> createPack;

        public CRUDPackOperations(Action<PackPathInfo> refreshAssetSaveableData, Func<PackAsset, PackAsset> createPack)
        {
            this.refreshAssetSaveableData = refreshAssetSaveableData;
            this.createPack = createPack;
        }
        
        /// <summary>
        /// Create a new pack on external storage.
        /// </summary>
        /// <param name="pack">The data to create the pack with.</param>
        public void Save(PackAsset pack) => Load(pack);

        /// <summary>
        /// Updates information
        /// </summary>
        /// <param name="pack"></param>
        public void Update(PackAsset pack)
        {
            if (currentPackInfo.Title != pack.Title) Rename(pack.Title);
            JSONSystem.Save(currentPackInfo.FilePath, EditorAssetIDs.PackIdentifier, pack, p => new JSONPackAsset(p));
        }
        
        /// <summary>
        /// Loads all packs stored at application persistent path.
        /// </summary>
        /// <returns>A list of all <see cref="PackAsset"/>s.</returns>
        public IList<PackAsset> LoadAll()
        {
            IList<PackAsset> packs = JSONSystem.LoadAll<PackAsset, JSONPackAsset>(packData.Path, packData.Identifier, true);
            foreach (PackAsset pack in packs)
            {
                packPaths.Add(BuildPackInfo(pack));
            }
            return packs;
        }

        /// <summary>
        /// Delete a pack from external storage.
        /// </summary>
        /// <param name="pack">The pack to delete.</param>
        /// <exception cref="InvalidOperationException">Is thrown when pack doesn't exist.</exception>
        public void Delete(PackAsset pack)
        {
            try
            {
                int index = packPaths.FindIndexFirst(pack.ID);
                FileSystem.DeleteDirectory(packPaths[index].DirectoryPath);
                packPaths.RemoveAt(index);
                currentPackInfo = null;
            }
            catch (PreconditionCollectionException)
            {
                throw new InvalidOperationException("Cannot delete a pack that doesn't exist.");
            }
        }

        /// <summary>
        /// Prepares the overseer for working with a specific pack and loads it's data.
        /// </summary>
        /// <param name="pack">The pack to load.</param>
        public PackAsset Load(PackAsset pack)
        {
            //Update current pack info.
            try {currentPackInfo = packPaths.FindValueFirst(pack.ID); }
            catch (PreconditionCollectionException) { CreateSkeleton(pack); }
            refreshAssetSaveableData.Invoke(currentPackInfo);

            return createPack.Invoke(pack);
        }
        
        public void RefreshSaveableData(SaveableData packData)
        {
            FileSystem.CreateDirectory(packData.Path);
            this.packData = packData;
        }
        
        /// <summary>
        /// Renames the currently pack files.
        /// </summary>
        /// <param name="newTitle">The title to use.</param>
        private void Rename(string newTitle)
        {
            string oldPathDirectory = currentPackInfo.DirectoryPath;
            string newPathDirectory = Path.Combine(packData.Path, newTitle);
            string oldPathFile = Path.Combine(newPathDirectory, currentPackInfo.Title);
            string newPathFile = Path.Combine(packData.Path, newTitle, newTitle);
            
            currentPackInfo.UpdateTitle(newTitle);
            currentPackInfo.UpdatePath(newPathDirectory, newPathFile);
            
            FileSystem.RenameDirectory(oldPathDirectory, newPathDirectory);
            JSONSystem.RenameFile(oldPathFile, newPathFile);
            
            refreshAssetSaveableData.Invoke(currentPackInfo);
        }
        
        /// <summary>
        /// Initializes a new pack and builds it's skeleton in external storage.
        /// </summary>
        /// <param name="pack">The pack to initialize.</param>
        private void CreateSkeleton(PackAsset pack)
        {
            PackPathInfo packInfo = BuildPackInfo(pack);
            string newPackPathFile = Path.Combine(packInfo.DirectoryPath, pack.Title);
            
            FileSystem.CreateDirectory(packInfo.DirectoryPath);
            JSONSystem.Save(newPackPathFile, EditorAssetIDs.PackIdentifier, pack, p => new JSONPackAsset(p));

            FileSystem.CreateDirectory(packInfo.PalettesData.Path);
            FileSystem.CreateDirectory(packInfo.SpritesData.Path);
            FileSystem.CreateDirectory(packInfo.WeaponsData.Path);
            FileSystem.CreateDirectory(packInfo.ProjectilesData.Path);
            FileSystem.CreateDirectory(packInfo.EnemiesData.Path);
            FileSystem.CreateDirectory(packInfo.RoomsData.Path);
            FileSystem.CreateDirectory(packInfo.TilesData.Path);
            
            packPaths.Add(packInfo);
            currentPackInfo = packInfo;
        }
        
        /// <summary>
        /// Builds a <see cref="PackPathInfo"/> from a <see cref="PackAsset"/>.
        /// </summary>
        /// <param name="pack">The pack to build for.</param>
        /// <returns>A <see cref="PackPathInfo"/> with proper data.</returns>
        private PackPathInfo BuildPackInfo(PackAsset pack)
        {
            return new PackPathInfo(pack.ID,
                pack.Title,
                Path.Combine(packData.Path, pack.Title),
                Path.Combine(packData.Path, pack.Title, pack.Title));
        }
    }
}