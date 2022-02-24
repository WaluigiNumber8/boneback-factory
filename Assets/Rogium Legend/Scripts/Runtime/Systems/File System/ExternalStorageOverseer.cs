using System;
using System.Collections.Generic;
using System.IO;
using BoubakProductions.Safety;
using BoubakProductions.Systems.FileSystem;
using Rogium.Editors.Sprites;
using Rogium.Editors.Campaign;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Projectiles;
using Rogium.Editors.Rooms;
using Rogium.Editors.Tiles;
using Rogium.Editors.Weapons;
using Rogium.ExternalStorage.Serialization;

namespace Rogium.ExternalStorage
{
    /// <summary>
    /// Overseers files stored on external storage.
    /// </summary>
    public class ExternalStorageOverseer
    {
        private SaveableData packData;
        private SaveableData campaignData;

        private IList<PackPathInfo> packPaths;
        private PackPathInfo currentPackInfo;

        private CRUDOperations<CampaignAsset, SerializedCampaignAsset> campaignCRUD;
        
        private CRUDOperations<PaletteAsset, SerializedPaletteAsset> paletteCRUD;
        private CRUDOperations<SpriteAsset, SerializedSpriteAsset> spriteCRUD;
        private CRUDOperations<WeaponAsset, SerializedWeaponAsset> weaponCRUD;
        private CRUDOperations<ProjectileAsset, SerializedProjectileAsset> projectileCRUD;
        private CRUDOperations<EnemyAsset, SerializedEnemyAsset> enemyCRUD;
        private CRUDOperations<RoomAsset, SerializedRoomAsset> roomCRUD;
        private CRUDOperations<TileAsset, SerializedTileAsset> tileCRUD;

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
            packData = new SaveableData("Packs", "bumpack");
            campaignData = new SaveableData("Campaigns", "bumcamp");
            
            packPaths = new List<PackPathInfo>();

            campaignCRUD = new CRUDOperations<CampaignAsset, SerializedCampaignAsset>(c => new SerializedCampaignAsset(c));
            campaignCRUD.RefreshSaveableData(campaignData);
            
            paletteCRUD = new CRUDOperations<PaletteAsset, SerializedPaletteAsset>(p => new SerializedPaletteAsset(p));
            spriteCRUD = new CRUDOperations<SpriteAsset, SerializedSpriteAsset>(s => new SerializedSpriteAsset(s));
            weaponCRUD = new CRUDOperations<WeaponAsset, SerializedWeaponAsset>(w => new SerializedWeaponAsset(w));
            projectileCRUD = new CRUDOperations<ProjectileAsset, SerializedProjectileAsset>(p => new SerializedProjectileAsset(p));
            enemyCRUD = new CRUDOperations<EnemyAsset, SerializedEnemyAsset>(e => new SerializedEnemyAsset(e));
            roomCRUD = new CRUDOperations<RoomAsset, SerializedRoomAsset>(r => new SerializedRoomAsset(r));
            tileCRUD = new CRUDOperations<TileAsset, SerializedTileAsset>(t => new SerializedTileAsset(t));
            
            FileSystem.CreateDirectory(packData.Path);
            FileSystem.CreateDirectory(campaignData.Path);
        }

        /// <summary>
        /// Create a new pack on external storage.
        /// </summary>
        /// <param name="pack">The data to create the pack with.</param>
        public void CreatePack(PackAsset pack)
        {
            LoadPack(pack);
        }

        /// <summary>
        /// Updates information
        /// </summary>
        /// <param name="pack"></param>
        public void UpdatePack(PackAsset pack)
        {
            if (currentPackInfo.PackTitle != pack.Title)
                RenameCurrentPack(pack.Title);
            
            FileSystem.SaveFile(currentPackInfo.FilePath, pack, p => new SerializedPackAsset(p));
        }
        
        /// <summary>
        /// Loads all packs stored at application persistent path.
        /// </summary>
        /// <returns>A list of all <see cref="PackAsset"/>s.</returns>
        public IList<PackAsset> LoadAllPacks()
        {
            IList<PackAsset> packs = FileSystem.LoadAllFiles<PackAsset, SerializedPackAsset>(packData.Path, packData.Extension, true);
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
        public void DeletePack(PackAsset pack)
        {
            try
            {
                int index = GetIndexFirst(packPaths, pack.ID);
                FileSystem.DeleteDirectory(packPaths[index].DirectoryPath);
                packPaths.RemoveAt(index);
                currentPackInfo = null;
            }
            catch (SafetyNetCollectionException)
            {
                throw new InvalidOperationException("Cannot delete a pack that doesn't exist.");
            }
        }
        
        /// <summary>
        /// Prepares the overseer for working with a specific pack and loads it's data.
        /// </summary>
        /// <param name="pack">The pack to load.</param>
        public PackAsset LoadPack(PackAsset pack)
        {
            //Update current pack info.
            try
            {
                currentPackInfo = GetPackInfoFirst(packPaths, pack.ID);
            }
            catch (SafetyNetCollectionException)
            {
               CreateSkeleton(pack);
            }

            PackInfoAsset packInfo = new PackInfoAsset(pack.PackInfo);
            
            paletteCRUD.RefreshSaveableData(currentPackInfo.PalettesData);
            spriteCRUD.RefreshSaveableData(currentPackInfo.SpritesData);
            weaponCRUD.RefreshSaveableData(currentPackInfo.WeaponsData);
            projectileCRUD.RefreshSaveableData(currentPackInfo.ProjectilesData);
            enemyCRUD.RefreshSaveableData(currentPackInfo.EnemiesData);
            roomCRUD.RefreshSaveableData(currentPackInfo.RoomsData);
            tileCRUD.RefreshSaveableData(currentPackInfo.TilesData);
            
            return new PackAsset(packInfo, paletteCRUD.LoadAll(), spriteCRUD.LoadAll(), weaponCRUD.LoadAll(),
                                 projectileCRUD.LoadAll(), enemyCRUD.LoadAll(), roomCRUD.LoadAll(), tileCRUD.LoadAll());
        }

        /// <summary>
        /// Initializes a new pack and builds it's skeleton in external storage.
        /// </summary>
        /// <param name="pack">The pack to initialize.</param>
        private void CreateSkeleton(PackAsset pack)
        {
            PackPathInfo packInfo = BuildPackInfo(pack);
            string newPackPathFile = Path.Combine(packInfo.DirectoryPath, $"{pack.Title}.{packData.Extension}");
            
            FileSystem.CreateDirectory(packInfo.DirectoryPath);
            FileSystem.SaveFile(newPackPathFile, pack, p => new SerializedPackAsset(p));

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
        /// Renames the currently pack files.
        /// </summary>
        /// <param name="newTitle">The title to use.</param>
        private void RenameCurrentPack(string newTitle)
        {
            string oldPathDirectory = currentPackInfo.DirectoryPath;
            string newPathDirectory = Path.Combine(packData.Path, newTitle);
            string oldPathFile = Path.Combine(newPathDirectory, $"{currentPackInfo.PackTitle}.{packData.Extension}");
            string newPathFile = Path.Combine(packData.Path, newTitle, $"{newTitle}.{packData.Extension}");
            
            currentPackInfo.UpdateTitle(newTitle);
            currentPackInfo.UpdatePath(newPathDirectory, newPathFile);
            
            FileSystem.RenameDirectory(oldPathDirectory, newPathDirectory);
            FileSystem.RenameFile(oldPathFile, newPathFile);
        }
        
        /// <summary>
        /// Finds the index of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <returns>Index of the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        private PackPathInfo GetPackInfoFirst(IList<PackPathInfo> list, string id)
        {
            try
            {
                return list[GetIndexFirst(list, id)];
            }
            catch (SafetyNetCollectionException)
            {
                throw new SafetyNetCollectionException($"No object with the ID '{id}' was not found in the list.");
            }
        }
        
        /// <summary>
        /// Finds the index of the first asset with the same ID.
        /// </summary>
        /// <param name="list">The list to search in.</param>
        /// <param name="id">The ID of the asset to search for.</param>
        /// <returns>Index of the first found asset.</returns>
        /// <exception cref="SafetyNetCollectionException">Is thrown when no asset with ID was found.</exception>
        private int GetIndexFirst(IList<PackPathInfo> list, string id)
        {
            SafetyNet.EnsureIsNotNull(list, "List ot search");
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].PackID == id)
                {
                    return i;
                }
            }

            throw new SafetyNetCollectionException($"No object with the ID '{id}' was not found in the list.");
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
                                    Path.Combine(packData.Path, pack.Title, $"{pack.Title}.{packData.Extension}"));
        }

        public CRUDOperations<CampaignAsset, SerializedCampaignAsset> Campaigns { get => campaignCRUD; }
        public CRUDOperations<PaletteAsset, SerializedPaletteAsset> Palettes { get => paletteCRUD; }
        public CRUDOperations<SpriteAsset, SerializedSpriteAsset> Sprites { get => spriteCRUD; }
        public CRUDOperations<WeaponAsset, SerializedWeaponAsset> Weapons { get => weaponCRUD; }
        public CRUDOperations<ProjectileAsset, SerializedProjectileAsset> Projectiles { get => projectileCRUD; }
        public CRUDOperations<EnemyAsset, SerializedEnemyAsset> Enemies { get => enemyCRUD; }
        public CRUDOperations<RoomAsset, SerializedRoomAsset> Rooms { get => roomCRUD; }
        public CRUDOperations<TileAsset, SerializedTileAsset> Tiles { get => tileCRUD; }

    }
}