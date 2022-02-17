using BoubakProductions.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.UserInterface.Core;
using System;
using System.Collections.Generic;
using BoubakProductions.Safety;
using UnityEngine;

namespace Rogium.UserInterface.AssetSelection.PickerVariant
{
    /// <summary>
    /// Prepares the Asset Selection System for picking multiple assets.
    /// </summary>
    public class AssetSelectionPickerMultiple : MonoBehaviour, IAssetSelectionPicker
    {
        public event Action<AssetBase> OnAssetSelect; 

        [SerializeField] private AssetSelectionOverseerMono assetSelection;
        
        private Action<IList<AssetBase>> targetMethod;
        private IList<AssetBase> selectedAssets;
        private IList<string> selectedAssetsIDs;

        private ToggleList cardToggleList;
        private bool defaultWasSet;

        private void Awake()
        {
            selectedAssets = new List<AssetBase>();
            selectedAssetsIDs = new List<string>();
            cardToggleList = new ToggleList();
        }

        private void OnEnable()
        {
            AssetPickerCardController.OnSelected += WhenAssetSelected;
            AssetPickerCardController.OnDeselected += WhenAssetDeselected;
        }

        private void OnDisable()
        {
            AssetPickerCardController.OnSelected -= WhenAssetSelected;
            AssetPickerCardController.OnDeselected -= WhenAssetDeselected;
        }

        public void WhenAssetSelected(AssetBase asset)
        {
            if (selectedAssets.ContainsValue(asset)) return;
            selectedAssets.Add(asset);
            selectedAssetsIDs.Add(asset.ID);
            OnAssetSelect?.Invoke(asset);
        }

        public void WhenAssetDeselected(AssetBase asset)
        {
            if (!selectedAssets.ContainsValue(asset)) return;
            selectedAssets.RemoveAt(selectedAssets.FindIndexFirst(asset.ID));
        }

        /// <summary>
        /// Toggles the asset's selected status.
        /// </summary>
        public void WhenAssetSelectToggle(AssetBase asset)
        {
            if (!selectedAssets.ContainsValue(asset))
                WhenAssetSelected(asset);
            else WhenAssetDeselected(asset);
        }

        /// <summary>
        /// Enable all the assets from the list.
        /// </summary>
        public void WhenAssetSelectAll() => cardToggleList.ToggleAll(true);

        /// <summary>
        /// Disable all the assets from the list.
        /// </summary>
        public void WhenAssetDeselectAll() => cardToggleList.ToggleAll(false);

        public void ConfirmSelection()
        {
            SafetyNet.EnsureIsNotNull(targetMethod, "Method to Run");
            targetMethod.Invoke(selectedAssets);
            targetMethod = null;
        }

        public void CancelSelection()
        {
            selectedAssets = null;
            selectedAssetsIDs = null;
            targetMethod = null;
        }

        #region Open Picker Selection

        /// <summary>
        /// Opens the Picker Selection Menu for Packs.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will already be selected, after opening the menu.</param>
        public void OpenForPacks(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            Open(targetMethod, preselectedAssets);
            assetSelection.OpenForPacks();
        }

        /// <summary>
        /// Opens the Picker Selection Menu for Palettes.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will already be selected, after opening the menu.</param>
        public void OpenForPalettes(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            Open(targetMethod, preselectedAssets);
            assetSelection.OpenForPalettes();
        }

        /// <summary>
        /// Opens the Picker Selection Menu for Sprites.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will already be selected, after opening the menu.</param>
        public void OpenForSprites(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            Open(targetMethod, preselectedAssets);
            assetSelection.OpenForSprites();
        }

        /// <summary>
        /// Opens the Picker Selection Menu for Weapons.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will already be selected, after opening the menu.</param>
        public void OpenForWeapons(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            Open(targetMethod, preselectedAssets);
            assetSelection.OpenForWeapons();
        }

        /// <summary>
        /// Opens the Picker Selection Menu for Projectiles.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will already be selected, after opening the menu.</param>
        public void OpenForProjectiles(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            Open(targetMethod, preselectedAssets);
            assetSelection.OpenForProjectiles();
        }

        /// <summary>
        /// Opens the Picker Selection Menu for Enemies.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will already be selected, after opening the menu.</param>
        public void OpenForEnemies(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            Open(targetMethod, preselectedAssets);
            assetSelection.OpenForEnemies();
        }

        /// <summary>
        /// Opens the Picker Selection Menu for Rooms.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will already be selected, after opening the menu.</param>
        public void OpenForRooms(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            Open(targetMethod, preselectedAssets);
            assetSelection.OpenForRooms();
        }
        
        /// <summary>
        /// Opens the Picker Selection Menu for Tiles.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will already be selected, after opening the menu.</param>
        public void OpenForTiles(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            Open(targetMethod, preselectedAssets);
            assetSelection.OpenForTiles();
        }

        #endregion

        /// <summary>
        /// Reopens the Selection Picker Menu.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAssets">The assets that will be already selected, after opening the menu.</param>
        private void Open(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets)
        {
            //Preselect Assets.
            if (preselectedAssets?.Count > 0)
            {
                selectedAssets = new List<AssetBase>(preselectedAssets);
                selectedAssetsIDs = new List<string>(preselectedAssets.ConvertToIDs());
            }
            else
            {
                selectedAssets.Clear();
                selectedAssetsIDs.Clear();
            }

            defaultWasSet = false;
            cardToggleList.Clear();
            this.targetMethod = targetMethod;
            assetSelection.BeginListeningToSpawnedCards(AddAssetHolderToList);
        }

        /// <summary>
        /// Toggle on an asset holder, if it contains the same data as one of the selected assets. 
        /// </summary>
        /// <param name="assetHolder">The Asset Holder this method works with.</param>
        private void AddAssetHolderToList(AssetHolderBase assetHolder)
        {
            AssetPickerCardController holder = (AssetPickerCardController)assetHolder;
            string assetID = holder.Asset.ID;
            
            cardToggleList.Add(holder);
            if (selectedAssets.Count <= 0)
            {
                WhenAssetSelected(holder.Asset);
                holder.SetToggle(true);
                defaultWasSet = true;
            }

            if (defaultWasSet) return;
            
            if (selectedAssetsIDs.ContainsValue(assetID))
            {
                holder.SetToggle(true);
            }
        }

        /// <summary>
        /// Returns the amount of assets currently loaded in the Selection Picker Menu.
        /// </summary>
        public int SelectionCount => selectedAssets.Count;

    }
}