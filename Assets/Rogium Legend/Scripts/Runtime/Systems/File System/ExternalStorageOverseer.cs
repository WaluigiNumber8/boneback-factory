using System;
using System.Collections.Generic;
using System.IO;
using RedRats.Core;
using RedRats.Safety;
using RedRats.Systems.FileSystem;
using Rogium.Core;
using Rogium.Editors.Sprites;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core.Defaults;
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
    public sealed class ExternalStorageOverseer : Singleton<ExternalStorageOverseer>
    {
        private readonly SaveableData packData;
        private readonly SaveableData campaignData;

        private readonly IList<PackPathInfo> packPaths;
        private PackPathInfo currentPackInfo;

        private readonly CRUDOperations<CampaignAsset, JSONCampaignAsset> campaignCRUD;
        private readonly CRUDOperations<PaletteAsset, JSONPaletteAsset> paletteCRUD;
        private readonly CRUDOperations<SpriteAsset, JSONSpriteAsset> spriteCRUD;
        private readonly CRUDOperations<WeaponAsset, JSONWeaponAsset> weaponCRUD;
        private readonly CRUDOperations<ProjectileAsset, JSONProjectileAsset> projectileCRUD;
        private readonly CRUDOperations<EnemyAsset, JSONEnemyAsset> enemyCRUD;
        private readonly CRUDOperations<RoomAsset, JSONRoomAsset> roomCRUD;
        private readonly CRUDOperations<TileAsset, JSONTileAsset> tileCRUD;

        private ExternalStorageOverseer()
        {
            packData = new SaveableData("Packs", EditorAssetIDs.PackIdentifier);
            campaignData = new SaveableData("Campaigns", EditorAssetIDs.CampaignIdentifier);
            
            packPaths = new List<PackPathInfo>();

            campaignCRUD = new CRUDOperations<CampaignAsset, JSONCampaignAsset>(c => new JSONCampaignAsset(c), EditorAssetIDs.CampaignIdentifier);
            campaignCRUD.RefreshSaveableData(campaignData);
            
            paletteCRUD = new CRUDOperations<PaletteAsset, JSONPaletteAsset>(p => new JSONPaletteAsset(p), EditorAssetIDs.PaletteIdentifier);
            spriteCRUD = new CRUDOperations<SpriteAsset, JSONSpriteAsset>(s => new JSONSpriteAsset(s), EditorAssetIDs.SpriteIdentifier);
            weaponCRUD = new CRUDOperations<WeaponAsset, JSONWeaponAsset>(w => new JSONWeaponAsset(w), EditorAssetIDs.WeaponIdentifier);
            projectileCRUD = new CRUDOperations<ProjectileAsset, JSONProjectileAsset>(p => new JSONProjectileAsset(p), EditorAssetIDs.ProjectileIdentifier);
            enemyCRUD = new CRUDOperations<EnemyAsset, JSONEnemyAsset>(e => new JSONEnemyAsset(e), EditorAssetIDs.EnemyIdentifier);
            roomCRUD = new CRUDOperations<RoomAsset, JSONRoomAsset>(r => new JSONRoomAsset(r), EditorAssetIDs.RoomIdentifier);
            tileCRUD = new CRUDOperations<TileAsset, JSONTileAsset>(t => new JSONTileAsset(t), EditorAssetIDs.TileIdentifier);
            
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
            if (currentPackInfo.Title != pack.Title) RenameCurrentPack(pack.Title);
            JSONSystem.Save(currentPackInfo.FilePath, EditorAssetIDs.PackIdentifier, pack, p => new JSONPackAsset(p));
        }
        
        /// <summary>
        /// Loads all packs stored at application persistent path.
        /// </summary>
        /// <returns>A list of all <see cref="PackAsset"/>s.</returns>
        public IList<PackAsset> LoadAllPacks()
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
        public void DeletePack(PackAsset pack)
        {
            try
            {
                int index = packPaths.FindIndexFirst(pack.ID);
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
            try {currentPackInfo = packPaths.FindValueFirst(pack.ID); }
            catch (SafetyNetCollectionException)
            {
               CreateSkeleton(pack);
            }
            RefreshAssetSaveableData();
            
            return new PackAsset(pack.ID, pack.Title, pack.Icon, pack.Author, pack.AssociatedSpriteID, pack.Description, 
                                 pack.CreationDate, paletteCRUD.LoadAll(), spriteCRUD.LoadAll(), weaponCRUD.LoadAll(),
                                 projectileCRUD.LoadAll(), enemyCRUD.LoadAll(), roomCRUD.LoadAll(), tileCRUD.LoadAll());
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
        /// Renames the currently pack files.
        /// </summary>
        /// <param name="newTitle">The title to use.</param>
        private void RenameCurrentPack(string newTitle)
        {
            string oldPathDirectory = currentPackInfo.DirectoryPath;
            string newPathDirectory = Path.Combine(packData.Path, newTitle);
            string oldPathFile = Path.Combine(newPathDirectory, currentPackInfo.Title);
            string newPathFile = Path.Combine(packData.Path, newTitle, newTitle);
            
            currentPackInfo.UpdateTitle(newTitle);
            currentPackInfo.UpdatePath(newPathDirectory, newPathFile);
            
            FileSystem.RenameDirectory(oldPathDirectory, newPathDirectory);
            JSONSystem.RenameFile(oldPathFile, newPathFile);
            
            RefreshAssetSaveableData();
        }

        /// <summary>
        /// Refreshes saveable data for all assets based on the current pack.
        /// </summary>
        private void RefreshAssetSaveableData()
        {
            paletteCRUD.RefreshSaveableData(currentPackInfo.PalettesData);
            spriteCRUD.RefreshSaveableData(currentPackInfo.SpritesData);
            weaponCRUD.RefreshSaveableData(currentPackInfo.WeaponsData);
            projectileCRUD.RefreshSaveableData(currentPackInfo.ProjectilesData);
            enemyCRUD.RefreshSaveableData(currentPackInfo.EnemiesData);
            roomCRUD.RefreshSaveableData(currentPackInfo.RoomsData);
            tileCRUD.RefreshSaveableData(currentPackInfo.TilesData);
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

        public CRUDOperations<CampaignAsset, JSONCampaignAsset> Campaigns { get => campaignCRUD; }
        public CRUDOperations<PaletteAsset, JSONPaletteAsset> Palettes { get => paletteCRUD; }
        public CRUDOperations<SpriteAsset, JSONSpriteAsset> Sprites { get => spriteCRUD; }
        public CRUDOperations<WeaponAsset, JSONWeaponAsset> Weapons { get => weaponCRUD; }
        public CRUDOperations<ProjectileAsset, JSONProjectileAsset> Projectiles { get => projectileCRUD; }
        public CRUDOperations<EnemyAsset, JSONEnemyAsset> Enemies { get => enemyCRUD; }
        public CRUDOperations<RoomAsset, JSONRoomAsset> Rooms { get => roomCRUD; }
        public CRUDOperations<TileAsset, JSONTileAsset> Tiles { get => tileCRUD; }
    }
}