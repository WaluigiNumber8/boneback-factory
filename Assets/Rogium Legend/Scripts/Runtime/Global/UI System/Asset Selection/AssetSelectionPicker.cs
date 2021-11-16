using Rogium.Core;
using Rogium.Editors.Core;
using System;
using System.Collections.Generic;
using BoubakProductions.Core;
using UnityEngine;

namespace Rogium.Global.UISystem.AssetSelection
{
    /// <summary>
    /// Prepares the Asset Selection System for picking assets.
    /// </summary>
    public class AssetSelectionPicker : MonoBehaviour
    {
        public event Action<IList<AssetBase>> OnConfirmSelection; 

        [SerializeField] private AssetSelectionOverseerMono assetSelection;

        private Action<IList<AssetBase>> targetMethod;
        private IList<AssetBase> selectedAssets;
        private IList<string> selectedAssetsIDs;

        private void Start()
        {
            selectedAssets = new List<AssetBase>();
        }

        private void OnEnable()
        {
            AssetCardPickerController.OnSelected += WhenAssetSelected;
            AssetCardPickerController.OnDeselected += WhenAssetDeselected;
        }

        private void OnDisable()
        {
            AssetCardPickerController.OnSelected -= WhenAssetSelected;
            AssetCardPickerController.OnDeselected -= WhenAssetDeselected;
        }

        /// <summary>
        /// Adds a selected asset to the list.
        /// </summary>
        /// <param name="asset">The newly selected asset.</param>
        public void WhenAssetSelected(AssetBase asset)
        {
            selectedAssets.Add(asset);
        }

        /// <summary>
        /// Removes a selected asset from the list.
        /// </summary>
        /// <param name="asset">The asset to remove.</param>
        public void WhenAssetDeselected(AssetBase asset)
        {
            selectedAssets.RemoveAt(selectedAssets.FindIndexFirst(asset));
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
        private void Reopen(Action<IList<AssetBase>> targetMethod, IList<AssetBase> preselectedAssets = null)
        {
            this.targetMethod = targetMethod;
            OnConfirmSelection += this.targetMethod;
            
            //Preselect Assets.
            if (preselectedAssets != null || preselectedAssets.Count > 0)
            {
                assetSelection.BeginListeningToSpawnedCards(UpdateAssetHolder);
                selectedAssets = preselectedAssets;
                selectedAssetsIDs = selectedAssets.ConvertToIDs();
            }
        }

        /// <summary>
        /// Toggle on an asset holder, if it contains same data as one of the selected assets. 
        /// </summary>
        /// <param name="assetHolder"></param>
        private void UpdateAssetHolder(IAssetHolder assetHolder)
        {
            string assetID = assetHolder.Asset.ID;
            if (selectedAssetsIDs.IsOnList(assetID))
            {
                AssetCardPickerController pickerHolder = (AssetCardPickerController) assetHolder;
                pickerHolder.Toggle(true);
            }
            
        }

        /// <summary>
        /// Returns the amount of assets currently loaded in the Selection Picker Menu.
        /// </summary>
        public int SelectionCount => selectedAssets.Count;

    }
}