using RedRats.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using System.Collections.Generic;
using System.Linq;
using RedRats.Safety;
using Rogium.Editors.NewAssetSelection;
using UnityEngine;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Works like a bridge between UI and <see cref="CampaignEditorOverseer"/>
    /// </summary>
    public class CampaignEditorOverseerMono : MonoSingleton<CampaignEditorOverseerMono>
    {
        [SerializeField] private AssetSelectionPicker selectionPicker;
        [SerializeField] private CampaignSelectedPackPropertyController propertyColumn;
        
        private CampaignEditorOverseer editor;
        private ExternalLibraryOverseer lib;

        private ISet<IAsset> selectedAssets;

        protected override void Awake()
        {
            base.Awake();
            editor = CampaignEditorOverseer.Instance;
            lib = ExternalLibraryOverseer.Instance;
            selectedAssets = new HashSet<IAsset>();
        }
        
        private void OnEnable()
        {
            editor.OnAssignAsset += PrepareEditor;
            // selectionPicker.OnAssetSelect += PreparePropertyColumn;
            // selectionPicker.OnAssetDeselect += PreparePropertyColumn;
        }
        
        private void OnDisable()
        {
            editor.OnAssignAsset -= PrepareEditor;
            // selectionPicker.OnAssetSelect -= PreparePropertyColumn;
            // selectionPicker.OnAssetDeselect -= PreparePropertyColumn;
        }

        /// <summary>
        /// Selects/Deselects an asset.
        /// </summary>
        /// <param name="assetIndex">The position of the asset on the list.</param>
        public void ChangeSelectStatus(int assetIndex) => selectionPicker.SelectorContent.GetChild(assetIndex).GetComponent<AssetCardControllerV2>().Toggle();

        /// <summary>
        /// Calls for applying current selection.
        /// </summary>
        public void CompleteSelection() => SelectionPicker.ConfirmSelection();

        private void PrepareEditor(CampaignAsset asset)
        {
            PreselectAssetsFrom(asset);
            selectionPicker.Pick(AssetType.Pack, UpdatePacksFromSelection, selectedAssets);
            if (selectedAssets != null && selectedAssets.Count > 0) PreparePropertyColumn(selectedAssets.First());
        }
        
        /// <summary>
        /// Calls for updating packs in the current campaign from the selection picker.
        /// </summary>
        /// <param name="finalSelectedAssets">The packs to update with.</param>
        private void UpdatePacksFromSelection(ISet<IAsset> finalSelectedAssets)
        {
            SafetyNet.EnsureSetIsNotNullOrEmpty(finalSelectedAssets, "Selected Packs");
            IList<PackAsset> finalSelectedPacks = finalSelectedAssets.Cast<PackAsset>().ToList();
            editor.UpdateDataPack(finalSelectedPacks);
        }

        /// <summary>
        /// Updates the list of selected assets based on the currently edited campaign.
        /// </summary>
        /// <param name="campaign">The currently edited campaign.</param>
        private void PreselectAssetsFrom(CampaignAsset campaign)
        {
            selectedAssets.Clear();
            
            if (campaign.PackReferences == null || campaign.PackReferences.Count <= 0) return;
            
            IList<IAsset> allPacks = lib.Packs.Cast<IAsset>().ToList();
            selectedAssets = allPacks.GrabBasedOn(campaign.PackReferences).ToHashSet();
        }

        /// <summary>
        /// Loads the proper pack into the Property Column.
        /// </summary>
        /// <param name="asset">The Position of the pack on the Library List.</param>
        private void PreparePropertyColumn(IAsset asset)
        {
            propertyColumn.AssignAsset((PackAsset)asset, new PackImportInfo());
        }
        
        public AssetSelectionPicker SelectionPicker { get => selectionPicker; }
    }
}
