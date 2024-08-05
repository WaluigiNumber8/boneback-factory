using RedRats.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// Controls the Asset Selection Menu.
    /// </summary>
    public class AssetSelectionMenuOverseerMono : MonoSingleton<AssetSelectionMenuOverseerMono>
    {
        [SerializeField] private AssetSelector assetSelector;
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private SelectionInfoColumn infoColumn;

        private void OnEnable() => AssetCardController.OnSelect += UpdateInfoColumn;
        private void OnDisable() => AssetCardController.OnSelect -= UpdateInfoColumn;

        /// <summary>
        /// Prepares the menu for filling.
        /// </summary>
        public void Open(AssetType type)
        {
            assetSelector.BeginListeningToSpawnedCards(AssignGroup, DeselectAll);
            assetSelector.Open(type);
        }

        /// <summary>
        /// Assign the ToggleGroup to a card.
        /// </summary>
        /// <param name="holder">The card to assign to.</param>
        private void AssignGroup(AssetHolderBase holder)
        {
            holder.RegisterToggleGroup(toggleGroup);
        }

        /// <summary>
        /// Updates the Info Column of the Selection Menu.
        /// </summary>
        /// <param name="asset">The asset to update the column with.</param>
        private void UpdateInfoColumn(IAsset asset)
        {
            if (!infoColumn.isActiveAndEnabled) return;
            infoColumn.Construct(asset);
        }

        /// <summary>
        /// Deselect all toggles.
        /// </summary>
        private void DeselectAll()
        {
            toggleGroup.SetAllTogglesOff();
            if (infoColumn == null) return;
            infoColumn.ConstructEmpty();
        }
    }
}