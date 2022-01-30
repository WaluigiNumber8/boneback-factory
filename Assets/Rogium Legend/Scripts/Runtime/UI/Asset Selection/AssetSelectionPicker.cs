using BoubakProductions.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.UserInterface.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// Prepares the Asset Selection System for picking assets.
    /// </summary>
    public class AssetSelectionPicker : MonoBehaviour
    {
        public event Action<IList<AssetBase>> OnConfirmSelection; 

        [SerializeField] private AssetSelectionOverseerMono assetSelection;
        [SerializeField] private bool manualSubscription;
        
        private Action<IList<AssetBase>> targetMethod;
        private IList<AssetBase> selectedAssets;
        private IList<string> selectedAssetsIDs;

        private ToggleList cardToggleList;
        
        private void Awake()
        {
            selectedAssets = new List<AssetBase>();
            selectedAssetsIDs = new List<string>();
            cardToggleList = new ToggleList();
        }

        private void OnEnable()
        {
            if (manualSubscription) return;
            AssetCardPickerController.OnSelected += WhenAssetSelected;
            AssetCardPickerController.OnDeselected += WhenAssetDeselected;
        }

        private void OnDisable()
        {
            if (manualSubscription) return;
            AssetCardPickerController.OnSelected -= WhenAssetSelected;
            AssetCardPickerController.OnDeselected -= WhenAssetDeselected;
        }

        /// <summary>
        /// Adds a selected asset to the list.
        /// </summary>
        /// <param name="asset">The newly selected asset.</param>
        public void WhenAssetSelected(AssetBase asset)
        {
            if (selectedAssets.ContainsAsset(asset)) return;
            selectedAssets.Add(asset);
        }

        /// <summary>
        /// Removes a selected asset from the list.
        /// </summary>
        /// <param name="asset">The asset to remove.</param>
        public void WhenAssetDeselected(AssetBase asset)
        {
            if (!selectedAssets.ContainsAsset(asset)) return;
            selectedAssets.RemoveAt(selectedAssets.FindIndexFirst(asset.ID));
        }

        /// <summary>
        /// Toggles the asset's selected status.
        /// </summary>
        public void WhenAssetSelectToggle(AssetBase asset)
        {
            if (!selectedAssets.ContainsAsset(asset))
                WhenAssetSelected(asset);
            else WhenAssetDeselected(asset);
        }

        /// <summary>
        /// Enable all the assets from the list.
        /// </summary>
        public void WhenAssetSelectAll()
        {
            cardToggleList.ToggleAll(true);
        }
        
        /// <summary>
        /// Disable all the assets from the list.
        /// </summary>
        public void WhenAssetDeselectAll()
        {
            cardToggleList.ToggleAll(false);
        }

        /// <summary>
        /// Sends the selected assets to all subjects.
        /// </summary>
        public void ConfirmSelection()
        {
            OnConfirmSelection?.Invoke(selectedAssets);
            OnConfirmSelection -= targetMethod;
        }

        /// <summary>
        /// Opens the Picker Selection Menu for Packs.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will be already selected, after opening the menu.</param>
        public void ReopenForPacks(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            Reopen(targetMethod, preselectedAssets);
            assetSelection.ReopenForPacks();
        }

        public void ReopenForPalettes()
        {
        }

        public void ReopenForSprites()
        {
        }

        public void ReopenForWeapons()
        {
        }

        public void ReopenForProjectiles()
        {
        }

        public void ReopenForEnemies()
        {
        }

        public void ReopenForRooms()
        {
        }

        public void ReopenForTiles()
        {
        }

        /// <summary>
        /// Reopens the Selection Picker Menu.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will be already selected, after opening the menu.</param>
        private void Reopen(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            //Preselect Assets.
            if (preselectedAssets != null && preselectedAssets.Count > 0)
            {
                selectedAssets = new List<AssetBase>(preselectedAssets);
                selectedAssetsIDs = new List<string>(preselectedAssets.ConvertToIDs());
            }
            else
            {
                selectedAssets = new List<AssetBase>();
                selectedAssetsIDs = new List<string>();
            }
            
            cardToggleList.Clear();
            this.targetMethod = targetMethod;
            OnConfirmSelection += this.targetMethod;
            assetSelection.BeginListeningToSpawnedCards(AddAssetHolderToList);
        }

        /// <summary>
        /// Toggle on an asset holder, if it contains the same data as one of the selected assets. 
        /// </summary>
        /// <param name="assetHolder">The Asset Holder this method works with.</param>
        private void AddAssetHolderToList(IAssetHolder assetHolder)
        {
            cardToggleList.Add((IToggleable)assetHolder);
            string assetID = assetHolder.Asset.ID;
            if (selectedAssetsIDs.ContainsAsset(assetID))
            {
                AssetCardPickerController pickerHolder = (AssetCardPickerController) assetHolder;
                pickerHolder.ChangeToggleState(true);
            }
        }

        /// <summary>
        /// Returns the amount of assets currently loaded in the Selection Picker Menu.
        /// </summary>
        public int SelectionCount => selectedAssets.Count;

    }
}