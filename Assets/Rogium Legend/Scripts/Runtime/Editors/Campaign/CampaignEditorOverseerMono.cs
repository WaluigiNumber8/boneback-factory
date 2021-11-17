using BoubakProductions.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.Global.UISystem.AssetSelection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Works like a bridge between UI and <see cref="CampaignEditorOverseer"/>
    /// </summary>
    public class CampaignEditorOverseerMono : MonoSingleton<CampaignEditorOverseerMono>
    {
        [SerializeField] private AssetSelectionPicker selectionPicker;

        private CampaignEditorOverseer overseer;
        private LibraryOverseer lib;

        private IList<AssetBase> selectedAssets;

        protected override void Awake()
        {
            base.Awake();
            overseer = CampaignEditorOverseer.Instance;
            lib = LibraryOverseer.Instance;;
            selectedAssets = new List<AssetBase>();
        }

        /// <summary>
        /// Uses the currently selected packs from the editor to combine them into a campaign.
        /// </summary>
        public void FillMenu()
        {
            RefillSelectedAssets(overseer.CurrentCampaign);
            SelectionPicker.ReopenForPacks(UpdatePacksFromSelection, selectedAssets);
        }

        /// <summary>
        /// Calls for applying current selection.
        /// </summary>
        public void CompleteSelection()
        {
            SelectionPicker.ConfirmSelection();
        }

        /// <summary>
        /// Calls for updating packs in the current campaign from the selection picker.
        /// </summary>
        /// <param name="finalSelectedAssets">The packs to update with.</param>
        private void UpdatePacksFromSelection(IList<AssetBase> finalSelectedAssets)
        {
            IList<PackAsset> finalSelectedPacks = finalSelectedAssets.Cast<PackAsset>().ToList();
            overseer.UpdatePacks(finalSelectedPacks);
        }

        /// <summary>
        /// Updates the list of selected assets based on the currently edited campaign.
        /// </summary>
        /// <param name="campaign">The currently edited campaign.</param>
        private void RefillSelectedAssets(CampaignAsset campaign)
        {
            selectedAssets.Clear();
            
            if (campaign.ReferencedIDs == null || campaign.ReferencedIDs.Count <= 0) return;
            
            IList<AssetBase> allPacks = lib.GetPacksCopy.ConvertToAssetBase();
            selectedAssets = allPacks.GrabFromList(campaign.ReferencedIDs);
        }

        public AssetSelectionPicker SelectionPicker { get => selectionPicker; }
    }
}
