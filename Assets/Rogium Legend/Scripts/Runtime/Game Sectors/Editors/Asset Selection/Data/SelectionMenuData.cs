using System;
using System.Collections.Generic;
using Rogium.Editors.Core;
using Rogium.UserInterface.Interactables;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Contains data needed to setup a Selection Menu via <see cref="SelectionMenuOverseerMono"/>.
    /// </summary>
    [Serializable]
    public struct SelectionMenuData
    {
        [SerializeField] private AssetSelector assetSelector;
        [SerializeField] private ButtonType whenAssetCreate;
        [SerializeField] private ButtonType whenAssetEdit;
        [SerializeField] private ButtonType whenAssetConfig;
        [SerializeField] private ButtonType whenAssetDelete;
        private Func<IList<IAsset>> getAssetList;

        public SelectionMenuData(SelectionMenuData data, Func<IList<IAsset>> getAssetList)
        {
            assetSelector = data.assetSelector;
            whenAssetCreate = data.whenAssetCreate;
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
        
        public AssetSelector AssetSelector { get => assetSelector; }
        public ButtonType WhenAssetCreate { get => whenAssetCreate; }
        public ButtonType WhenAssetEdit { get => whenAssetEdit; }
        public ButtonType WhenAssetConfig { get => whenAssetConfig; }
        public ButtonType WhenAssetDelete { get => whenAssetDelete; }
        public Func<IList<IAsset>> GetAssetList { get => getAssetList; }
    }
}