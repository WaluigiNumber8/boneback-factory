using System;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.AssetSelection.PickerVariant
{
    /// <summary>
    /// Controls the graphic window, it's actions and connects it with <see cref="AssetSelectionPickerMultiple"/>.
    /// </summary>
    public class AssetPickerWindowController : MonoBehaviour
    {
        [SerializeField] private AssetSelectionPickerSingle selectionPicker;
        [SerializeField] private UIInfo ui;

        private Action<AssetBase> targetMethod;

        private void OnEnable()
        {
            ui.acceptButton.onClick.AddListener(selectionPicker.ConfirmSelection);
            ui.cancelButton.onClick.AddListener(CancelSelection);
        }

        private void OnDisable()
        {
            ui.acceptButton.onClick.RemoveListener(selectionPicker.ConfirmSelection);
            ui.cancelButton.onClick.RemoveListener(CancelSelection);
        }

        /// <summary>
        /// Open the Selection Picker menu and grab an asset.
        /// </summary>
        /// <param name="type">The type of asset to grab.</param>
        /// <param name="WhenAssetGrabbed">The method that runs when the asset is grabbed.</param>
        /// <param name="preselectedAsset">The asset that will be selected on window open.</param>
        /// <exception cref="InvalidOperationException">Is thrown when the asset is set to "None".</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when an unsupported type appears.</exception>
        public void GrabAsset(AssetType type, Action<AssetBase> WhenAssetGrabbed, AssetBase preselectedAsset = null)
        {
            targetMethod = WhenAssetGrabbed;
            Open();

            switch (type)
            {
                case AssetType.None:
                    throw new InvalidOperationException("The Asset Type is set to \"None\".");
                case AssetType.Pack:
                    selectionPicker.OpenForPacks(ConfirmSelection, preselectedAsset);
                    break;
                case AssetType.Palette:
                    selectionPicker.OpenForPalettes(ConfirmSelection, preselectedAsset);
                    break;
                case AssetType.Sprite:
                    selectionPicker.OpenForSprites(ConfirmSelection, preselectedAsset);
                    break;
                case AssetType.Weapon:
                    selectionPicker.OpenForWeapons(ConfirmSelection, preselectedAsset);
                    break;
                case AssetType.Projectile:
                    selectionPicker.OpenForProjectiles(ConfirmSelection, preselectedAsset);
                    break;
                case AssetType.Enemy:
                    selectionPicker.OpenForEnemies(ConfirmSelection, preselectedAsset);
                    break;
                case AssetType.Room:
                    selectionPicker.OpenForRooms(ConfirmSelection, preselectedAsset);
                    break;
                case AssetType.Tile:
                    selectionPicker.OpenForTiles(ConfirmSelection, preselectedAsset);
                    break;
                case AssetType.Campaign:
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported Asset Type");
            }
        }

        /// <summary>
        /// Opens the window UI.
        /// </summary>
        private void Open() => ui.windowPrefab.SetActive(true);

        /// <summary>
        /// Closes the window UI.
        /// </summary>
        private void Close() => ui.windowPrefab.SetActive(false);

        /// <summary>
        /// Runs when an asset is selected and the action is confirmed successfully.
        /// </summary>
        /// <param name="asset">The asset that was selected.</param>
        private void ConfirmSelection(AssetBase asset)
        {
            targetMethod.Invoke(asset);
            targetMethod = null;
            CancelSelection();
        }

        /// <summary>
        /// Cancel the selection.
        /// </summary>
        private void CancelSelection()
        {
            selectionPicker.CancelSelection();
            Close();
        }

        [Serializable]
        public struct UIInfo
        {
            public GameObject windowPrefab;
            public Button acceptButton;
            public Button cancelButton;
        }
    }
}