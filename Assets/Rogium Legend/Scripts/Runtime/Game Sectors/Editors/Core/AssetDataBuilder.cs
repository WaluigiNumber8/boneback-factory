using Rogium.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Enemies;
using Rogium.Editors.Objects;
using Rogium.Editors.Packs;
using Rogium.Editors.Sounds;
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
        /// <param name="asset">The tile to build for..</param>
        /// <returns>AssetData for that specific asset.</returns>
        public static AssetData ForTile(IAsset asset)
        {
            if (asset.ID == EditorDefaults.EmptyAssetID) return new AssetData(ParameterInfoConstants.ForEmpty);
            if (asset.ID == lastID && lastType == AssetType.Tile) return lastData;

            TileAsset tile = (TileAsset) asset;
            ParameterInfo parameters = ParameterInfoConstants.ForTile;
            
            parameters.intValue1 = (int)tile.LayerType;

            lastID = asset.ID;
            lastType = AssetType.Tile;
            lastData = new AssetData(asset.ID, parameters);

            return lastData;
        }

        /// <summary>
        /// Builds Asset data for an interactable object.
        /// </summary>
        /// <param name="asset">The object to read from.</param>
        /// <returns>AssetData for that specific asset.</returns>
        public static AssetData ForObject(IAsset asset)
        {
            if (asset.ID == EditorDefaults.EmptyAssetID) return new AssetData(ParameterInfoConstants.ForEmpty);
            if (asset.ID == lastID && lastType == AssetType.Object) return lastData;
            
            ObjectAsset iobject = (ObjectAsset)asset;
            ParameterInfo parameters = iobject.Parameters;

            lastID = asset.ID;
            lastType = AssetType.Object;
            lastData = new AssetData(asset.ID, parameters);

            return lastData;
        }
        
        /// <summary>
        /// Builds Asset data for an enemy in the currently open pack.
        /// </summary>
        /// <param name="asset">The enemy to read from.</param>
        /// <returns>AssetData for that specific asset.</returns>
        public static AssetData ForEnemy(IAsset asset)
        {
            if (asset.ID == EditorDefaults.EmptyAssetID) return new AssetData(ParameterInfoConstants.ForEmpty);
            if (asset.ID == lastID && lastType == AssetType.Enemy) return lastData;
            
            EnemyAsset enemy = (EnemyAsset)asset;
            ParameterInfo parameters = ParameterInfoConstants.ForEnemy;
            
            parameters.intValue1 = enemy.BaseDamage;

            lastID = asset.ID;
            lastType = AssetType.Enemy;
            lastData = new AssetData(asset.ID, parameters);

            return lastData;
        }
        
        /// <summary>
        /// Builds Asset data for a <see cref="SoundAsset"/> in the currently open pack.
        /// </summary>
        /// <param name="asset">The sound asset to build for.</param>
        /// <returns>AssetData for that specific asset.</returns>
        public static AssetData ForSound(IAsset asset)
        {
            if (asset.ID == EditorDefaults.EmptyAssetID) return new AssetData(ParameterInfoConstants.ForEmpty);
            if (asset.ID == lastID && lastType == AssetType.Sound) return lastData;

            SoundAsset sound = (SoundAsset) asset;
            ParameterInfo parameters = sound.Parameters;

            lastID = asset.ID;
            lastType = AssetType.Sound;
            lastData = new AssetData(asset.ID, parameters);

            return lastData;
        }
        
    }
}