using System;
using RedRats.UI.Core;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Systems.ThemeSystem;
using Rogium.UserInterface.Editors.PropertyModalWindows;
using Rogium.UserInterface.Interactables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Editors.AssetSelection.PickerVariant
{
    /// <summary>
    /// Controls the graphic window, it's actions and connects it with <see cref="AssetSelectionPickerMultiple"/>.
    /// </summary>
    public class AssetPickerWindow : ModalWindowBase
    {
        [SerializeField] private AssetSelectionPickerSingle selectionPicker;
        [SerializeField] private UIInfo ui;
        
        private Action<IAsset> targetMethod;

        private void OnEnable()
        {
            ui.footer.acceptButton.onClick.AddListener(selectionPicker.ConfirmSelection);
            ui.footer.cancelButton.onClick.AddListener(CancelSelection);
        }

        private void OnDisable()
        {
            ui.footer.acceptButton.onClick.RemoveListener(selectionPicker.ConfirmSelection);
            ui.footer.cancelButton.onClick.RemoveListener(CancelSelection);
        }

        /// <summary>
        /// Open the Selection Picker menu and grab an asset.
        /// </summary>
        /// <param name="type">The type of asset to grab.</param>
        /// <param name="whenAssetGrabbed">The method that runs when the asset is grabbed.</param>
        /// <param name="preselectedAsset">The asset that will be selected on window open.</param>
        /// <exception cref="InvalidOperationException">Is thrown when the asset is set to "None".</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when an unsupported type appears.</exception>
        public void GrabAsset(AssetType type, Action<IAsset> whenAssetGrabbed, IAsset preselectedAsset = null)
        {
            targetMethod = whenAssetGrabbed;
            
            generalUI.entireArea.GetComponentInParent<Transform>().SetAsLastSibling();
            ui.header.text.text = $"Select a {type.ToString().ToLower()}";
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
                case AssetType.Sound:
                    selectionPicker.OpenForSounds(ConfirmSelection, preselectedAsset);
                    break;
                case AssetType.Campaign:
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported Asset Type");
            }
        }

        /// <summary>
        /// Fills the Asset Picker Window with a proper theme.
        /// </summary>
        /// <param name="backgroundSprite">The background of the window.</param>
        /// <param name="headerSprite">The bar around the header.</param>
        /// <param name="buttonSet">The buttons of the window.</param>
        /// <param name="headerFont">Font of the header text.</param>
        /// <param name="emptyTextFont">Empty window text.</param>
        public void UpdateTheme(Sprite backgroundSprite, Sprite headerSprite, InteractableSpriteInfo buttonSet, FontInfo headerFont, FontInfo emptyTextFont)
        {
            UIExtensions.ChangeInteractableSprites(ui.footer.acceptButton, ui.footer.acceptButton.image, buttonSet);
            UIExtensions.ChangeInteractableSprites(ui.footer.cancelButton, ui.footer.cancelButton.image, buttonSet);
            UIExtensions.ChangeFont(ui.header.text, headerFont);
            UIExtensions.ChangeFont(ui.layout.emptyMessageCard, emptyTextFont);
            UIExtensions.ChangeFont(ui.layout.emptyMessageList, emptyTextFont);
            UIExtensions.ChangeFont(ui.footer.acceptButtonText, headerFont);
            UIExtensions.ChangeFont(ui.footer.cancelButtonText, headerFont);
            ThemeUpdaterRogium.UpdateScrollbar(ui.layout.scrollbarList);
            ThemeUpdaterRogium.UpdateScrollbar(ui.layout.scrollbarCard);
            ui.header.headerImage.sprite = headerSprite;
            generalUI.windowArea.sprite = backgroundSprite;
        }
        
        protected override void UpdateTheme() => ThemeUpdaterRogium.UpdateAssetPickerWindow(this);

        /// <summary>
        /// Runs when an asset is selected and the action is confirmed successfully.
        /// </summary>
        /// <param name="asset">The asset that was selected.</param>
        private void ConfirmSelection(IAsset asset)
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
            public HeaderInfo header;
            public LayoutInfo layout;
            public FooterInfo footer;
        }
        
        [Serializable]
        public struct HeaderInfo
        {
            public Transform area;
            public Image headerImage;
            public TextMeshProUGUI text;
        }

        [Serializable]
        public struct LayoutInfo
        {
            public Transform area;
            public TextMeshProUGUI emptyMessageCard;
            public TextMeshProUGUI emptyMessageList;
            public InteractableScrollbar scrollbarCard;
            public InteractableScrollbar scrollbarList;
        }

        [Serializable]
        public struct FooterInfo
        {
            public Transform area;
            public Button acceptButton;
            public Button cancelButton;
            public TextMeshProUGUI acceptButtonText;
            public TextMeshProUGUI cancelButtonText;

        }
    }
}