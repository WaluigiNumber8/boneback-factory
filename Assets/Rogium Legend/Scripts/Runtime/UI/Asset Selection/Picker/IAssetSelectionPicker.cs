using System;
using Rogium.Editors.Core;

namespace Rogium.UserInterface.AssetSelection.PickerVariant
{
    public interface IAssetSelectionPicker
    {
        public event Action<AssetBase> OnAssetSelect; 
        
        /// <summary>
        /// Runs when an asset is selected.
        /// </summary>
        /// <param name="asset">The asset that was selected.</param>
        public void WhenAssetSelected(AssetBase asset);
        
        /// <summary>
        /// Runs when an asset is deselected.
        /// </summary>
        /// <param name="asset">The asset that was deselected.</param>
        public void WhenAssetDeselected(AssetBase asset);
        
        /// <summary>
        /// Sends the selected assets to all subjects.
        /// </summary>
        public void ConfirmSelection();

        /// <summary>
        /// Cancel the Selection Process.
        /// </summary>
        public void CancelSelection();
    }
}