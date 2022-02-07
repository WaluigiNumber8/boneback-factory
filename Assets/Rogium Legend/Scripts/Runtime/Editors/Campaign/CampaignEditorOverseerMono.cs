using BoubakProductions.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;
using Rogium.UserInterface.AssetSelection.PickerVariant;
using UnityEngine;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Works like a bridge between UI and <see cref="CampaignEditorOverseer"/>
    /// </summary>
    public class CampaignEditorOverseerMono : MonoSingleton<CampaignEditorOverseerMono>
    {
        [SerializeField] private AssetSelectionPickerMultiple selectionPicker;
        [SerializeField] private CampaignSelectedPackPropertyController propertyColumn;
        
        private CampaignEditorOverseer overseer;
        private LibraryOverseer lib;

        private IList<AssetBase> selectedAssets;

        protected override void Awake()
        {
            base.Awake();
            overseer = CampaignEditorOverseer.Instance;
            lib = LibraryOverseer.Instance;
            selectedAssets = new List<AssetBase>();
        }
        
        private void OnEnable() => selectionPicker.OnAssetSelect += PreparePropertyColumn;
        private void OnDisable() => selectionPicker.OnAssetSelect -= PreparePropertyColumn;

        /// <summary>
        /// Uses the currently selected packs from the editor to combine them into a campaign.
        /// </summary>
        public void FillMenu()
        {
            PreselectAssetsFrom(overseer.CurrentCampaign);
            SelectionPicker.OpenForPacks(UpdatePacksFromSelection, selectedAssets);
        }

        /// <summary>
        /// Selects/Deselects an asset.
        /// </summary>
        /// <param name="assetIndex">The position of the asset on the list.</param>
        public void ChangeSelectStatus(int assetIndex) => SelectionPicker.WhenAssetSelectToggle(lib.GetPacksCopy[assetIndex]);

        /// <summary>
        /// Calls for applying current selection.
        /// </summary>
        public void CompleteSelection() => SelectionPicker.ConfirmSelection();

        /// <summary>
        /// Calls for updating packs in the current campaign from the selection picker.
        /// </summary>
        /// <param name="finalSelectedAssets">The packs to update with.</param>
        private void UpdatePacksFromSelection(IList<AssetBase> finalSelectedAssets)
        {
            SafetyNet.EnsureListIsNotNullOrEmpty(finalSelectedAssets, "Selected Packs");
            IList<PackAsset> finalSelectedPacks = finalSelectedAssets.Cast<PackAsset>().ToList();
            overseer.UpdateDataPack(finalSelectedPacks);
        }

        /// <summary>
        /// Updates the list of selected assets based on the currently edited campaign.
        /// </summary>
        /// <param name="campaign">The currently edited campaign.</param>
        private void PreselectAssetsFrom(CampaignAsset campaign)
        {
            selectedAssets.Clear();
            
            if (campaign.PackReferences == null || campaign.PackReferences.Count <= 0) return;
            
            IList<AssetBase> allPacks = lib.GetPacksCopy.ConvertToAssetBase();
            selectedAssets = allPacks.GrabBasedOn(campaign.PackReferences);
        }

        /// <summary>
        /// Loads the proper pack into the Property Column.
        /// </summary>
        /// <param name="asset">The Position of the pack on the Library List.</param>
        private void PreparePropertyColumn(AssetBase asset)
        {
            propertyColumn.AssignAsset((PackAsset)asset, new PackImportInfo());
        }
        
        public AssetSelectionPickerMultiple SelectionPicker { get => selectionPicker; }
    }
}
