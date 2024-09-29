using System;
using Rogium.Editors.Core;
using Rogium.UserInterface.Editors.AssetSelection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Represents an asset in the form of a card.
    /// </summary>
    public class AssetCardControllerV2 : ToggleableBase
    {
        [SerializeField] private UIInfo ui;

        protected override void Awake()
        {
            base.Awake();
            toggle.onValueChanged.AddListener(ToggleDisplayedGroups);
        }

        public void Construct(IAsset asset, AssetCardData data)
        {
            ui.title.text = asset.Title;
            ui.iconImage.sprite = asset.Icon;
        }
        
        private void ToggleDisplayedGroups(bool value)
        {
            ui.infoGroup.gameObject.SetActive(!value);
            ui.buttonGroup.gameObject.SetActive(value);
        }

        public bool IsInfoGroupShown => ui.infoGroup.gameObject.activeSelf;
        public bool IsButtonGroupShown => ui.buttonGroup.gameObject.activeSelf;
        
        public string Title { get => ui.title.text; }
        public Sprite Icon {get => ui.iconImage.sprite;}

        [Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public Image iconImage;
            public RectTransform infoGroup;
            public RectTransform buttonGroup;
        }
    }
}