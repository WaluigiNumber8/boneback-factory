using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Enemies;
using Rogium.Editors.Packs;
using Rogium.Editors.Tiles;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Builds <see cref="AssetData"/> based on selected asset.
    /// </summary>
    public static class AssetDataBuilder
    {
        private static string lastID;
        private static AssetType lastType;
        private static AssetData lastData;

        /// <summary>
        /// Builds Asset data for a tile in the currently open pack.
        /// </summary>
        /// <param name="id">The id of the tile to build for.</param>
        /// <returns>AssetData for that specific asset.</returns>
        public static AssetData ForTile(string id)
        {
            return ForTile(PackEditorOverseer.Instance.CurrentPack.Tiles.FindValueFirst(id));
        }
        /// <summary>
        /// Builds Asset data for a tile in the currently open pack.
        /// </summary>
        /// <param name="asset">The tile to build for..</param>
        /// <returns>AssetData for that specific asset.</returns>
        public static AssetData ForTile(IAsset asset)
        {
            if (asset.ID == EditorDefaults.EmptyAssetID) return new AssetData(ParameterDefaults.ParamsEmpty);
            if (asset.ID == lastID && lastType == AssetType.Tile) return lastData;

            TileAsset tile = (TileAsset) asset;
            ParameterInfo parameters = ParameterDefaults.ParamsTile;
            
            parameters.intValue1 = (int)tile.Type;

            lastID = asset.ID;
            lastType = AssetType.Tile;
            lastData = new AssetData(asset.ID, parameters);

            return lastData;
        }
        
        /// <summary>
        /// Builds Asset data for an enemy in the currently open pack.
        /// </summary>
        /// <param name="id">The id of the enemy to read from.</param>
        /// <returns>AssetData for that specific asset.</returns>
        public static AssetData ForEnemy(string id)
        {
            return ForEnemy(PackEditorOverseer.Instance.CurrentPack.Enemies.FindValueFirst(id));
        }
        /// <summary>
        /// Builds Asset data for an enemy in the currently open pack.
        /// </summary>
        /// <param name="asset">The enemy to read from.</param>
        /// <returns>AssetData for that specific asset.</returns>
        public static AssetData ForEnemy(IAsset asset)
        {
            if (asset.ID == EditorDefaults.EmptyAssetID) return new AssetData(ParameterDefaults.ParamsEmpty);
            if (asset.ID == lastID && lastType == AssetType.Enemy) return lastData;
            
            EnemyAsset enemy = (EnemyAsset)asset;
            ParameterInfo parameters = ParameterDefaults.ParamsEnemy;
            
            parameters.intValue1 = enemy.BaseDamage;

            lastID = asset.ID;
            lastType = AssetType.Enemy;
            lastData = new AssetData(asset.ID, parameters);

            return lastData;
        }
    }
}