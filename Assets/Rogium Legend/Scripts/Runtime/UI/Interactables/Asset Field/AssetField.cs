using System;
using Rogium.Core;
using Rogium.Editors.Core;
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

        private IAsset value;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable) return;
            ModalWindowBuilder.GetInstance().OpenAssetPickerWindow(type, WhenAssetPicked, value);
        }

        /// <summary>
        /// Constructs the asset field with initial values.
        /// </summary>
        /// <param name="type">The type of asset to collect.</param>
        public void Construct(AssetType type, IAsset value)
        {
            this.type = type;
            this.value = value;
            
            ui.icon.sprite = value.Icon;
            if (ui.title != null) ui.title.text = value.Title;
        }

        /// <summary>
        /// Update everything based on the grabbed sprite.
        /// </summary>
        /// <param name="asset">The sprite to update with.</param>
        private void WhenAssetPicked(IAsset asset)
        {
            value = asset;
            ui.icon.sprite = asset.Icon;
            if (ui.title != null) ui.title.text = asset.Title;
            OnValueChanged?.Invoke(asset);
        }

        public IAsset Value { get => value; }
        public Image Icon { get => ui.icon; }
        public TextMeshProUGUI Title { get => ui.title; }
        
        [Serializable]
        public struct UIInfo
        {
            public Image icon;
            public TextMeshProUGUI title;
        }
    }
}