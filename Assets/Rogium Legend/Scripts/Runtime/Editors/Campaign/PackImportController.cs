using Rogium.UserInterface.AssetSelection;
using Rogium.UserInterface.AssetSelection.PickerVariant;
using Rogium.Editors.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rogium.Editors.Campaign
{
    /// <summary>
    /// Allows packs to be imported into a campaign.
    /// </summary>
    public class PackImportController : MonoBehaviour
    {
        [SerializeField] private AssetSelectionPickerMultiple selectionPicker;
        [SerializeField] private CampaignSelectedPackPropertyController propertyColumn;

        private IList<PackImportInfo> importedPacks;

        private void Awake()
        {
            importedPacks = new List<PackImportInfo>();
        }

        private void OnEnable()
        {
            AssetPickerCardController.OnSelected += PreparePropertyColumn;
        }

        private void OnDisable()
        {
            AssetPickerCardController.OnSelected -= PreparePropertyColumn;
        }

        public void AddToImportList(PackImportInfo importInfo)
        {
            if (IsOnList(importedPacks, importInfo.ID)) return;
            importedPacks.Add(importInfo);
        }
        
        /// <summary>
        /// Loads the proper pack into the Property Column.
        /// </summary>
        /// <param name="asset">The Position of the pack on the Library List.</param>
        private void PreparePropertyColumn(AssetBase asset)
        {
            // if (IsOnList(importedPacks, asset.ID))
            // propertyColumn.AssignAsset(asset, );
        }

        /// <summary>
        /// Checks if a given value is registered under any of the Pack Import Infos.
        /// </summary>
        /// <param name="importInfos">The list of import infos to check.</param>
        /// <param name="ID">The positionIndex on the list.</param>
        /// <returns>Returns true if a given pack is already registered as imported.</returns>
        private bool IsOnList(IList<PackImportInfo> importInfos, string ID)
        {
            return importInfos.Any(importInfo => importInfo.ID == ID);
        }
        
        public IList<PackImportInfo> ImportedPacks { get => importedPacks; }
    }
}