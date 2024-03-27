using System;
using RedRats.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Editors.AssetSelection.PickerVariant
{
    /// <summary>
    /// Allows the user to pick an asset from a selection.
    /// </summary>
    public class AssetSelectionPickerSingle : MonoBehaviour
    {
        public event Action<IAsset> OnAssetSelect;
        
        [SerializeField] private AssetSelector assetSelector;
        [SerializeField] private ToggleGroup toggleGroup;
        
        private Action<IAsset> whenSelected;
        private IAsset selectedAsset;
        private string previousID;

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

        public void WhenAssetSelected(IAsset asset)
        {
            if (asset.ID == previousID)
            {
                previousID = "";
                ConfirmSelection();
                return;
            }
            
            selectedAsset = asset;
            previousID = asset.ID;
            OnAssetSelect?.Invoke(asset);
        }
        
        public void WhenAssetDeselected(IAsset asset)
        {
            if (selectedAsset == null || asset.ID != selectedAsset.ID) return;
            selectedAsset = null;
        }
        
        public void ConfirmSelection()
        {
            SafetyNet.EnsureIsNotNull(whenSelected, "Method to Run");
            whenSelected.Invoke(selectedAsset);
            CancelSelection();
        }

        /// <summary>
        /// Confirms the selection with no selected asset.
        /// </summary>
        public void ConfirmSelectionNone()
        {
            SafetyNet.EnsureIsNotNull(whenSelected, "Method to Run");
            whenSelected.Invoke(null);
            CancelSelection();
        }
        
        public void CancelSelection()
        {
            selectedAsset = null;
            previousID = "";
            whenSelected = null;
        }
        

        /// <summary>
        /// Opens the Selection Picker.
        /// </summary>
        /// <param name="whenSelected">The method that will run when the asset is selected.</param>
        /// <param name="preselectedAsset">An asset that starts as selected.</param>
        public void Open(AssetType type, Action<IAsset> whenSelected, IAsset preselectedAsset = null, bool canSelectEmpty = false)
        {
            if (preselectedAsset != null) selectedAsset = preselectedAsset;
            this.whenSelected = whenSelected;
            assetSelector.BeginListeningToSpawnedCards(RegisterAssetHolder);
            assetSelector.Open(type);
        }
        
        /// <summary>
        /// Toggle on an asset holder, if it contains the same data as one of the selected assets. 
        /// </summary>
        /// <param name="assetHolder">The Asset Holder this method works with.</param>
        private void RegisterAssetHolder(AssetHolderBase assetHolder)
        {
            AssetPickerCardController holder = (AssetPickerCardController) assetHolder;
            holder.RegisterToggleGroup(toggleGroup);

            if (selectedAsset == null)
            {
                selectedAsset = holder.Asset;
                holder.SetToggle(true);
                return;
            }
            
            if (holder.Asset.ID == selectedAsset.ID)
                holder.SetToggle(true);
        }
    }
}