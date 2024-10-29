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
    public class SelectionMenuData
    {
        [SerializeField] private AssetSelector assetSelector;
        [SerializeField] private ButtonType whenAssetCreate;
        [SerializeField] private ButtonType whenAssetEdit;
        [SerializeField] private ButtonType whenAssetConfig;
        [SerializeField] private ButtonType whenAssetDelete;
        private Func<IList<IAsset>> getAssetList;
        private Action<int> whenCardSelected;
        private Action<int> whenCardDeselected;

        private SelectionMenuData() { }
        
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
        public Action<int> WhenCardSelected { get => whenCardSelected; }
        public Action<int> WhenCardDeselected { get => whenCardDeselected; }

        public class Builder
        {
            private readonly SelectionMenuData data = new()
            {
                assetSelector = null,
                whenAssetCreate = ButtonType.None,
                whenAssetEdit = ButtonType.None,
                whenAssetConfig = ButtonType.None,
                whenAssetDelete = ButtonType.None,
                getAssetList = Array.Empty<IAsset>
            };

            public Builder WithAssetSelector(AssetSelector assetSelector)
            {
                data.assetSelector = assetSelector;
                return this;
            }

            public Builder WithWhenAssetCreate(ButtonType whenAssetCreate)
            {
                data.whenAssetCreate = whenAssetCreate;
                return this;
            }

            public Builder WithWhenAssetEdit(ButtonType whenAssetEdit)
            {
                data.whenAssetEdit = whenAssetEdit;
                return this;
            }

            public Builder WithWhenAssetConfig(ButtonType whenAssetConfig)
            {
                data.whenAssetConfig = whenAssetConfig;
                return this;
            }

            public Builder WithWhenAssetDelete(ButtonType whenAssetDelete)
            {
                data.whenAssetDelete = whenAssetDelete;
                return this;
            }

            public Builder WithGetAssetList(Func<IList<IAsset>> getAssetList)
            {
                data.getAssetList = getAssetList;
                return this;
            }
            
            public Builder WithWhenCardSelected(Action<int> whenCardSelected)
            {
                data.whenCardSelected = whenCardSelected;
                return this;
            }
            
            public Builder WithWhenCardDeselected(Action<int> whenCardDeselected)
            {
                data.whenCardDeselected = whenCardDeselected;
                return this;
            }
            
            public Builder AsCopy(SelectionMenuData other)
            {
                data.assetSelector = other.assetSelector;
                data.whenAssetCreate = other.whenAssetCreate;
                data.whenAssetEdit = other.whenAssetEdit;
                data.whenAssetConfig = other.whenAssetConfig;
                data.whenAssetDelete = other.whenAssetDelete;
                data.getAssetList = other.getAssetList;
                data.whenCardSelected = other.whenCardSelected;
                data.whenCardDeselected = other.whenCardDeselected;
                return this;
            }

            public SelectionMenuData Build() => data;
        }
    }
}