using System;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection.PickerVariant
{
    /// <summary>
    /// Allows the user to pick an asset from a selection.
    /// </summary>
    public class AssetSelectionPickerSingle : MonoBehaviour, IAssetSelectionPicker
    {
        public event Action<IAsset> OnAssetSelect;
        
        [SerializeField] private AssetSelectionOverseerMono assetSelection;
        [SerializeField] private ToggleGroup toggleGroup;
        
        private Action<IAsset> targetMethod;
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
            SafetyNet.EnsureIsNotNull(targetMethod, "Method to Run");
            targetMethod.Invoke(selectedAsset);
            CancelSelection();
        }

        public void CancelSelection()
        {
            selectedAsset = null;
            previousID = "";
            targetMethod = null;
        }

        #region Open Selection
        
        /// <summary>
        /// Opens the Picker Selection Menu for Packs.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAsset">The asset that will already be selected, after opening the menu.</param>
        public void OpenForPacks(Action<IAsset> targetMethod, IAsset preselectedAsset = null)
        {
            Open(targetMethod, preselectedAsset);
            assetSelection.OpenForPacks();
        }
        
        /// <summary>
        /// Opens the Picker Selection Menu for Palettes.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAsset">The asset that will already be selected, after opening the menu.</param>
        public void OpenForPalettes(Action<IAsset> targetMethod, IAsset preselectedAsset = null)
        {
            Open(targetMethod, preselectedAsset);
            assetSelection.OpenForPalettes();
        }
        
        /// <summary>
        /// Opens the Picker Selection Menu for Sprites.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAsset">The asset that will already be selected, after opening the menu.</param>
        public void OpenForSprites(Action<IAsset> targetMethod, IAsset preselectedAsset = null)
        {
            Open(targetMethod, preselectedAsset);
            assetSelection.OpenForSprites();
        }
        
        /// <summary>
        /// Opens the Picker Selection Menu for Weapons.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAsset">The asset that will already be selected, after opening the menu.</param>
        public void OpenForWeapons(Action<IAsset> targetMethod, IAsset preselectedAsset = null)
        {
            Open(targetMethod, preselectedAsset);
            assetSelection.OpenForWeapons();
        }
        
        /// <summary>
        /// Opens the Picker Selection Menu for Projectiles.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAsset">The asset that will already be selected, after opening the menu.</param>
        public void OpenForProjectiles(Action<IAsset> targetMethod, IAsset preselectedAsset = null)
        {
            Open(targetMethod, preselectedAsset);
            assetSelection.OpenForProjectiles();
        }
        
        /// <summary>
        /// Opens the Picker Selection Menu for Enemies.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAsset">The asset that will already be selected, after opening the menu.</param>
        public void OpenForEnemies(Action<IAsset> targetMethod, IAsset preselectedAsset = null)
        {
            Open(targetMethod, preselectedAsset);
            assetSelection.OpenForEnemies();
        }
        
        /// <summary>
        /// Opens the Picker Selection Menu for Rooms.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAsset">The asset that will already be selected, after opening the menu.</param>
        public void OpenForRooms(Action<IAsset> targetMethod, IAsset preselectedAsset = null)
        {
            Open(targetMethod, preselectedAsset);
            assetSelection.OpenForRooms();
        }
        
        /// <summary>
        /// Opens the Picker Selection Menu for Tiles.
        /// </summary>
        /// <param name="targetMethod">The method, that requires results of the selection and
        /// will run only after ConfirmSelection() has been called.</param>
        /// <param name="preselectedAsset">The asset that will already be selected, after opening the menu.</param>
        public void OpenForTiles(Action<IAsset> targetMethod, IAsset preselectedAsset = null)
        {
            Open(targetMethod, preselectedAsset);
            assetSelection.OpenForTiles();
        }

        #endregion

        /// <summary>
        /// Prepares the picker for selection menu opening.
        /// </summary>
        /// <param name="targetMethod">The method that will run once </param>
        /// <param name="preselectedAsset"></param>
        private void Open(Action<IAsset> targetMethod, IAsset preselectedAsset)
        {
            if (preselectedAsset != null) selectedAsset = preselectedAsset;
            this.targetMethod = targetMethod;
            assetSelection.BeginListeningToSpawnedCards(RegisterAssetHolder);
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