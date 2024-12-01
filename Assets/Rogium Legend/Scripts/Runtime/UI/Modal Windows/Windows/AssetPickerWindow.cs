using System;
using RedRats.UI.Core;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Systems.ThemeSystem;
using Rogium.UserInterface.Interactables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.ModalWindows
{
    /// <summary>
    /// Controls the graphic window, it's actions and connects it with <see cref="AssetSelectionPickerSingle"/>.
    /// </summary>
    public class AssetPickerWindow : ModalWindowBase
    {
        [SerializeField] private AssetSelectionPickerSingle picker;
        [SerializeField] private UIInfo ui;

        private Action<IAsset> whenAssetPicked;

        private void OnEnable()
        {
            ui.footer.acceptButton.onClick.AddListener(ConfirmSelection);
            ui.footer.cancelButton.onClick.AddListener(CancelSelection);
        }

        private void OnDisable()
        {
            ui.footer.acceptButton.onClick.RemoveListener(ConfirmSelection);
            ui.footer.cancelButton.onClick.RemoveListener(CancelSelection);
        }

        /// <summary>
        /// Open the Selection Picker menu and grab an asset.
        /// </summary>
        /// <param name="type">What types of assets to grab.</param>
        /// <param name="whenAssetPicked">The method that runs when the asset is picked.</param>
        /// <param name="preselectedAsset">The asset that will be selected on window open.</param>
        /// <param name="canSelectEmpty">Allows selection of empty asset.</param>
        /// <exception cref="InvalidOperationException">Is thrown when the asset is set to "None".</exception>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown when an unsupported type appears.</exception>
        public void Construct(AssetType type, Action<IAsset> whenAssetPicked, IAsset preselectedAsset = null, bool canSelectEmpty = false)
        {
            this.whenAssetPicked = whenAssetPicked;
            
            generalUI.entireArea.GetComponentInParent<Transform>().SetAsLastSibling();
            ui.header.text.text = $"Select a {type.ToString().ToLower()}";
            picker.Pick(type, WhenAssetPicked, preselectedAsset, canSelectEmpty);
            ui.layout.emptyMessage.gameObject.SetActive(picker.Selector.Content.childCount == 0);
            Open();
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
            UIExtensions.ChangeFont(ui.layout.emptyMessage, emptyTextFont);
            UIExtensions.ChangeFont(ui.footer.acceptButtonText, headerFont);
            UIExtensions.ChangeFont(ui.footer.cancelButtonText, headerFont);
            ThemeUpdaterRogium.UpdateScrollbar(ui.layout.scrollbar);
            ui.header.headerImage.sprite = headerSprite;
            generalUI.windowArea.sprite = backgroundSprite;
        }
        
        public void Select(int index) => picker.Select(index);
        
        public void ConfirmSelection() => picker.ConfirmSelection();

        protected override void UpdateTheme() => ThemeUpdaterRogium.UpdateAssetPickerWindow(this);

        /// <summary>
        /// Runs when an asset is selected and the action is confirmed successfully.
        /// </summary>
        /// <param name="asset">The asset that was selected.</param>
        private void WhenAssetPicked(IAsset asset)
        {
            whenAssetPicked.Invoke(asset);
            whenAssetPicked = null;
            CancelSelection();
        }

        /// <summary>
        /// Cancel the selection.
        /// </summary>
        private void CancelSelection() => Close();

        public RectTransform SelectorContent { get => picker.Selector.Content; }
        public int SelectedAssetsCount { get => picker.SelectedAssetsCount; }

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
            public TextMeshProUGUI emptyMessage;
            public InteractableScrollbar scrollbar;
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