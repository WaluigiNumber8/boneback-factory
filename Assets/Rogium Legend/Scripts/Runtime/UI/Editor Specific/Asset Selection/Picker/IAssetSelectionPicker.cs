using System;
using Rogium.Editors.Core;

namespace Rogium.UserInterface.Editors.AssetSelection.PickerVariant
{
    public interface IAssetSelectionPicker
    {
        public event Action<IAsset> OnAssetSelect; 
        
        /// <summary>
        /// Runs when an asset is selected.
        /// </summary>
        /// <param name="asset">The asset that was selected.</param>
        public void WhenAssetSelected(IAsset asset);
        
        /// <summary>
        /// Runs when an asset is deselected.
        /// </summary>
        /// <param name="asset">The asset that was deselected.</param>
        public void WhenAssetDeselected(IAsset asset);
        
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