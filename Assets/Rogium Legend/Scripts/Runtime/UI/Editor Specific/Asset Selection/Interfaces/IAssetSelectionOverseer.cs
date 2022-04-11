namespace Rogium.UserInterface.Editors.AssetSelection
{
    public interface IAssetSelectionOverseer
    {
        /// <summary>
        /// Opens a selection menu for Pack Assets.
        /// </summary>
        void OpenForPacks();

        /// <summary>
        /// Opens a selection menu for Palette Assets.
        /// </summary>
        void OpenForPalettes();
        
        /// <summary>
        /// Opens a selection menu for Sprite Assets.
        /// </summary>
        void OpenForSprites();
        
        /// <summary>
        /// Opens a selection menu for Weapon Assets.
        /// </summary>
        void OpenForWeapons();

        /// <summary>
        /// Opens a selection menu for Projectile Assets.
        /// </summary>
        void OpenForProjectiles();
        
        /// <summary>
        /// Opens a selection menu for Enemy Assets.
        /// </summary>
        void OpenForEnemies();
        
        /// <summary>
        /// Opens a selection menu for Room Assets.
        /// </summary>
        void OpenForRooms();

        /// <summary>
        /// Opens a selection menu for Tile Assets.
        /// </summary>
        void OpenForTiles();
        
        /// <summary>
        /// Opens a selection menu for Interactable Object Assets.
        /// </summary>
        void OpenForObjects();
        
        /// <summary>
        /// Opens a selection menu for Sound Assets.
        /// </summary>
        void OpenForSounds();

    }
}