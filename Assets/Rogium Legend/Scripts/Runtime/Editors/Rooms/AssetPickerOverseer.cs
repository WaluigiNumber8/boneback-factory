using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.PackData;
using UnityEngine;

namespace Rogium.Editors.RoomData
{
    /// <summary>
    /// Decides on, which asset is currently picked for editing.
    /// </summary>
    public class AssetPickerOverseer
    {
        private PlaceableAsset currentAsset;

        public AssetPickerOverseer()
        {
            //TODO Replace with a better way to get the default asset.
            //TODO Make it function with the UI.
            if (LibraryOverseer.Instance.GetPacksCopy[0] == null)
            {
                Debug.LogError("TEMPORARY SOLUTION - It is required that an asset will have at least 1 tile created.");
                return;
            }
            
            currentAsset = new PlaceableAsset(LibraryOverseer.Instance.GetPacksCopy[0].Tiles[0], PlaceableAssetType.Tile);
        }
        
        public PlaceableAsset CurrentAsset { get => currentAsset;}
    }
}