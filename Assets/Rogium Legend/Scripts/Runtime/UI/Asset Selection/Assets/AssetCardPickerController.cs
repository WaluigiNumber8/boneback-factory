using Rogium.Core;
using Rogium.Editors.Core;
using System;
using Rogium.UserInterface.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// A variant for the <see cref="AssetCardController"/>. That allows the asset to be selected.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class AssetCardPickerController : MonoBehaviour, IAssetHolder, IToggleable
    {
        public static event Action<AssetBase> OnSelected;
        public static event Action<AssetBase> OnDeselected;
        public static event Action<AssetBase> OnToggled;

        [SerializeField] private Toggle toggle;
        [SerializeField] private UIInfo ui;

        private int posIndex;
        private AssetType type;
        private AssetBase asset;

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(WhenToggled);
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(WhenToggled);
        }

        public void Construct(AssetType type, int id, AssetBase asset, Image iconPos)
        {
            ui.icon = iconPos;
            Construct(type, id, asset);
        }

        public void Construct(AssetType type, int id, AssetBase asset)
        {
            this.type = type;
            this.posIndex = id;
            this.asset = asset;

            ui.title.text = asset.Title;
            ui.icon.sprite = asset.Icon;
            toggle.isOn = false;
        }

        public void ChangeToggleState(bool value)
        {
            toggle.isOn = value;
        }
        
        private void WhenToggled(bool value)
        {
            if (value) OnSelected?.Invoke(asset);
            else OnDeselected?.Invoke(asset);
            OnToggled?.Invoke(asset);
        }

        public int ID { get => posIndex; }
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