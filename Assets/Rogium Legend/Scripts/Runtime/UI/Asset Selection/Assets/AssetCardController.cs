using System;
using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// Handles setup and button interactions for the Asset Card object.
    /// </summary>
    public class AssetCardController : AssetHolderBase
    {
        public static event Action<IAsset> OnSelect; 

        [SerializeField] private UIInfo ui;

        protected override void Awake()
        {
            base.Awake();
            toggle.onValueChanged.AddListener(OnClick);
        }

        public override void Construct(AssetType type, int index, IAsset asset, Image iconPos)
        {
            // ui.iconImage = iconPos;
            Construct(type, index, asset);
        }

        public override void Construct(AssetType type, int index, IAsset asset)
        {
            this.type = type;
            this.index = index;
            this.asset = asset;
            ui.titleText.text = asset.Title;
            if (ui.iconImage != null) ui.iconImage.sprite = asset.Icon;
            
            ui.infoGroup.gameObject.SetActive(true);
            ui.buttonGroup.gameObject.SetActive(false);
        }

        /// <summary>
        /// Turns on/off button elements on the card.
        /// </summary>
        private void OnClick(bool value)
        {
            ui.infoGroup.gameObject.SetActive(!value);
            ui.buttonGroup.SetActive(value);
            OnSelect?.Invoke(asset);
        }

        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI titleText;
            public Image iconImage;
            public GameObject infoGroup;
            public GameObject buttonGroup;
        }
    }
}