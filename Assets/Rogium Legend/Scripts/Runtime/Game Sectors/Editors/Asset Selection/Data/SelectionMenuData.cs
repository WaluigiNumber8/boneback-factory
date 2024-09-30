using System;
using System.Collections.Generic;
using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Contains data needed to setup a Selection Menu via <see cref="SelectionMenuOverseerMono"/>.
    /// </summary>
    [Serializable]
    public struct SelectionMenuData
    {
        public AssetSelector assetSelector;
        public ButtonType whenAssetEdit;
        public ButtonType whenAssetConfig;
        public ButtonType whenAssetDelete;
        public Func<IList<IAsset>> getAssetList;

        public SelectionMenuData(SelectionMenuData data, Func<IList<IAsset>> getAssetList)
        {
            assetSelector = data.assetSelector;
            whenAssetEdit = data.whenAssetEdit;
            whenAssetConfig = data.whenAssetConfig;
            whenAssetDelete = data.whenAssetDelete;
            this.getAssetList = getAssetList;
        }
        
        /// <summary>
        /// Load this data into it's <see cref="AssetSelector"/>.
        /// </summary>
        public void Load() => assetSelector.Load(this);
        
        /// <summary>
        /// Load this data into it's <see cref="AssetSelector"/>.
        /// </summary>
        /// <param name="assets">Override the assets.</param>
        public void Load(IList<IAsset> assets) => assetSelector.Load(this, assets);
    }
}