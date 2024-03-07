using System;
using RedRats.UI;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.UserInterface.Core;
using Rogium.UserInterface.ModalWindows;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Allows the user to grab assets as input.
    /// </summary>
    public class AssetField : Selectable, IPointerClickHandler
    {
        public event Action<IAsset> OnValueChanged;

        [SerializeField] private AssetType type;
        [SerializeField] private UIInfo ui;

        private IAsset lastAsset;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable) return;
            ModalWindowBuilder.GetInstance().OpenAssetPickerWindow(type, WhenAssetGrabbed, lastAsset);
        }

        /// <summary>
        /// Explicitly set the type of asset this Asset Fields collects.
        /// </summary>
        /// <param name="type">The type of asset to collect.</param>
        public void SetType(AssetType type) => this.type = type;

        /// <summary>
        /// Update the UI based on the grabbed sprite.
        /// </summary>
        /// <param name="asset">The sprite to update with.</param>
        private void WhenAssetGrabbed(IAsset asset)
        {
            lastAsset = asset;
            ui.icon.sprite = asset.Icon;
            if (ui.title != null) ui.title.text = asset.Title;
            OnValueChanged?.Invoke(asset);
        }

        [Serializable]
        public struct UIInfo
        {
            public Image icon;
            public TextMeshProUGUI title;
        }
    }
}