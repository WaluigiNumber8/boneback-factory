using BoubakProductions.Safety;
using Rogium.Editors.Packs;
using Rogium.Editors.Tiles;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Decides which asset is will be be picked for editing.
    /// </summary>
    public class AssetPickerOverseer
    {
        private PlaceableAsset currentAsset;

        public AssetPickerOverseer()
        {
            //TODO Replace with a better way to get the default asset.
            //TODO Make it function with the UI.
        }

        /// <summary>
        /// Assigns a tile as a current asset,
        /// </summary>
        /// <param name="tile">The tile asset to use.</param>
        public void AssignTile(TileAsset tile)
        {
            SafetyNet.EnsureIsNotNull(tile, "Asset Picker - assigned tile.");
            this.currentAsset = new PlaceableAsset(new TileAsset(tile), PlaceableAssetType.Tile);
        }
        
        public PlaceableAsset CurrentAsset { get => currentAsset;}
    }
}