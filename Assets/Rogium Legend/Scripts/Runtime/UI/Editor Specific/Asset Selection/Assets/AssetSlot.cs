using System;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// Holds information about a specific asset.
    /// </summary>
    public class AssetSlot : ToggleableIndexBase, IAssetHolder
    {
        public static event Action<int> OnSelectedAny;
        
        [SerializeField] private UIInfo ui;

        private AssetType type;
        private IAsset asset;
        
        private void OnEnable() => toggle.onValueChanged.AddListener(WhenSelected);
        private void OnDisable() => toggle.onValueChanged.RemoveListener(WhenSelected);
        
        public void Construct(AssetType type, int index, IAsset asset, Image iconPos)
        {
            ui.iconImg = iconPos;
            Construct(type, index, asset);
        }
        public void Construct(AssetType type, int index, IAsset asset)
        {
            this.type = type;
            this.index = index;
            this.asset = asset;
            ui.iconImg.sprite = asset.Icon;
        }

        /// <summary>
        /// Fires the select event when the toggle was clicked.
        /// </summary>
        private void WhenSelected(bool value)
        {
            if (!value) return;
            OnSelectedAny?.Invoke(index);
        }
        
        public string ID { get => asset.ID; }
        
        public AssetType Type { get => type; }
        public IAsset Asset { get => asset; }

        [System.Serializable]
        public struct UIInfo
        {
            public Image iconImg;
        }

    }
}