using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;

namespace Rogium.ExternalStorage
{
    /// <summary>
    /// Stores information about usable paths for a pack.
    /// </summary>
    public class PackPathInfo : IDataAsset
    {
        private readonly string packID;
        private string packTitle;
        
        private string directoryPath;
        private string filePath;
        
        private readonly SaveableData palettesData;
        private readonly SaveableData spritesData;
        private readonly SaveableData weaponsData;
        private readonly SaveableData projectilesData;
        private readonly SaveableData enemiesData;
        private readonly SaveableData roomsData;
        private readonly SaveableData tilesData;
        
        public PackPathInfo(string packID, string packTitle, string directoryPath, string filePath)
        {
            this.packID = packID;
            this.packTitle = packTitle;
            this.directoryPath = directoryPath;
            this.filePath = filePath;
            
            palettesData = new SaveableData("Palettes", EditorAssetIDs.PaletteIdentifier, directoryPath);
            spritesData = new SaveableData("Sprites", EditorAssetIDs.SpriteIdentifier, directoryPath);
            weaponsData = new SaveableData("Weapons", EditorAssetIDs.WeaponIdentifier, directoryPath);
            projectilesData = new SaveableData("Projectiles", EditorAssetIDs.ProjectileIdentifier, directoryPath);
            enemiesData = new SaveableData("Enemies", EditorAssetIDs.EnemyIdentifier, directoryPath);
            roomsData = new SaveableData("Rooms", EditorAssetIDs.RoomIdentifier, directoryPath);
            tilesData = new SaveableData("Tiles", EditorAssetIDs.TileIdentifier, directoryPath);
        }

        /// <summary>
        /// Updates the title of the pack.
        /// </summary>
        /// <param name="newTitle">The new title.</param>
        public void UpdateTitle(string newTitle)
        {
            packTitle = newTitle;
        }
        
        /// <summary>
        /// Updates the path of the pack.
        /// </summary>
        /// <param name="newPathDirectory">The new directory path.</param>
        /// <param name="newPathFile">The new file path.</param>
        public void UpdatePath(string newPathDirectory, string newPathFile)
        {
            directoryPath = newPathDirectory;
            filePath = newPathFile;
            
            palettesData.UpdatePath(newPathDirectory);
            spritesData.UpdatePath(newPathDirectory);
            weaponsData.UpdatePath(newPathDirectory);
            projectilesData.UpdatePath(newPathDirectory);
            enemiesData.UpdatePath(newPathDirectory);
            roomsData.UpdatePath(newPathDirectory);
            tilesData.UpdatePath(newPathDirectory);
        }
        
        public string ID { get => packID; }
        public string Title { get => packTitle; }
        public string DirectoryPath { get => directoryPath; }
        public string FilePath { get => filePath; }
        
        public SaveableData PalettesData { get => palettesData; }
        public SaveableData SpritesData { get => spritesData; }
        public SaveableData WeaponsData { get => weaponsData; }
        public SaveableData ProjectilesData { get => projectilesData; }
        public SaveableData EnemiesData { get => enemiesData; }
        public SaveableData RoomsData { get => roomsData; }
        public SaveableData TilesData { get => tilesData; }
    }
}