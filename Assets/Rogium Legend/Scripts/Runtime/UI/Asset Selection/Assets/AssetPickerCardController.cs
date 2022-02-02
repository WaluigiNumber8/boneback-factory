using Rogium.Core;
using Rogium.Editors.Core;
using System;
using Rogium.UserInterface.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.AssetSelection.PickerVariant
{
    /// <summary>
    /// A variant for the <see cref="AssetCardController"/>. That allows the asset to be selected.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class AssetPickerCardController : AssetHolderBase, IToggleable
    {
        public static event Action<AssetBase> OnSelected;
        public static event Action<AssetBase> OnDeselected;
        public static event Action<AssetBase> OnToggled;

        [SerializeField] private Toggle toggle;
        [SerializeField] private UIInfo ui;

        private int posIndex;
        private AssetType type;
        private AssetBase asset;

        private void OnEnable() => toggle.onValueChanged.AddListener(WhenToggled);
        private void OnDisable() => toggle.onValueChanged.RemoveListener(WhenToggled);

        public override void Construct(AssetType type, int index, AssetBase asset, Image iconPos)
        {
            ui.icon = iconPos;
            Construct(type, index, asset);
        }

        public override void Construct(AssetType type, int index, AssetBase asset)
        {
            this.type = type;
            this.posIndex = index;
            this.asset = asset;

            ui.title.text = asset.Title;
            ui.icon.sprite = asset.Icon;
            toggle.isOn = false;
        }

        public void SetToggle(bool value) => toggle.isOn = value;

        /// <summary>
        /// Register the toggle under a <see cref="ToggleGroup"/>.
        /// </summary>
        /// <param name="group">The <see cref="ToggleGroup"/> to register the toggle under.</param>
        public void RegisterToggleGroup(ToggleGroup group) => toggle.group = group;

        private void WhenToggled(bool value)
        {
            if (value) OnSelected?.Invoke(asset);
            else OnDeselected?.Invoke(asset);
            OnToggled?.Invoke(asset);
        }

        public override int Index { get => posIndex; }
        public override AssetType Type { get => type; }
        public override AssetBase Asset { get => asset; }
        
        [System.Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public Image icon;
        }
    }
}