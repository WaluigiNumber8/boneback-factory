using Rogium.Core;
using Rogium.Editors.Core;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.Editors.AssetSelection.PickerVariant
{
    /// <summary>
    /// A variant for the <see cref="AssetCardController"/>. That allows the asset to be selected.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class AssetPickerCardController : AssetHolderBase
    {
        public static event Action<IAsset> OnSelected;
        public static event Action<IAsset> OnDeselected;
        public static event Action<IAsset> OnToggled;

        [SerializeField] private UIInfo ui;

        private void OnEnable() => toggle.onValueChanged.AddListener(WhenToggled);
        private void OnDisable() => toggle.onValueChanged.RemoveListener(WhenToggled);

        public override void Construct(AssetType type, int index, IAsset asset, Image iconPos)
        {
            ui.icon = iconPos;
            Construct(type, index, asset);
        }

        public override void Construct(AssetType type, int index, IAsset asset)
        {
            this.type = type;
            this.index = index;
            this.asset = asset;

            ui.title.text = asset.Title;
            ui.icon.sprite = asset.Icon;
            SetToggle(false);
        }

        private void WhenToggled(bool value)
        {
            if (value) OnSelected?.Invoke(asset);
            else OnDeselected?.Invoke(asset);
            OnToggled?.Invoke(asset);
        }

        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public Image icon;
        }
    }
}