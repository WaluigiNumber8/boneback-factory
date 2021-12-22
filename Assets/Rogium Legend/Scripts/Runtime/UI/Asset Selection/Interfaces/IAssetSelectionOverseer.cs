namespace Rogium.UserInterface.AssetSelection
{
    public interface IAssetSelectionOverseer
    {
        /// <summary>
        /// Opens a selection menu for Pack Assets.
        /// </summary>
        void ReopenForPacks();

        /// <summary>
        /// Opens a selection menu for Palette Assets.
        /// </summary>
        void ReopenForPalettes();
        
        /// <summary>
        /// Opens a selection menu for Sprite Assets.
        /// </summary>
        void ReopenForSprites();
        
        /// <summary>
        /// Opens a selection menu for Weapon Assets.
        /// </summary>
        void ReopenForWeapons();

        /// <summary>
        /// Opens a selection menu for Projectile Assets.
        /// </summary>
        void ReopenForProjectiles();
        
        /// <summary>
        /// Opens a selection menu for Enemy Assets.
        /// </summary>
        void ReopenForEnemies();
        
        /// <summary>
        /// Opens a selection menu for Room Assets.
        /// </summary>
        void ReopenForRooms();

        /// <summary>
        /// Opens a selection menu for Tile Assets.
        /// </summary>
        void ReopenForTiles();

    }
}