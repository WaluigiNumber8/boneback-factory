using Rogium.Core;
using Rogium.Editors.Core;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.Global.UISystem.AssetSelection
{
    /// <summary>
    /// A variant for the <see cref="AssetCardController"/>. That allows the asset to be selected.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class AssetCardPickerController : MonoBehaviour, IAssetHolder
    {
        public static event Action<AssetBase> OnSelected;
        public static event Action<AssetBase> OnDeselected;

        [SerializeField] private Toggle toggle;
        [SerializeField] private UIInfo ui;

        private int id;
        private AssetType type;
        private AssetBase asset;

        private void Start()
        {
            toggle.onValueChanged.AddListener(WhenToggled);
        }

        public void Construct(AssetType type, int id, AssetBase asset, Image iconPos)
        {
            ui.icon = iconPos;
            Construct(type, id, asset);
        }

        public void Construct(AssetType type, int id, AssetBase asset)
        {
            this.type = type;
            this.id = id;
            this.asset = asset;

            ui.title.text = asset.Title;
            ui.icon.sprite = asset.Icon;
            toggle.isOn = false;
        }

        /// <summary>
        /// Controls the toggle value.
        /// </summary>
        /// <param name="value">Value of the toggle.</param>
        public void Toggle(bool value)
        {
            toggle.isOn = value;
        }
        
        /// <summary>
        /// Calls an event based on if the toggle is On or Off.
        /// </summary>
        /// <param name="value"></param>
        private void WhenToggled(bool value)
        {
            if (value) OnSelected?.Invoke(asset);
            else OnDeselected?.Invoke(asset);
        }
        
        public int ID { get => id; }
        public AssetType Type { get => type; }
        public AssetBase Asset { get => asset; }
        
        [System.Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public Image icon;
        }
    }
}