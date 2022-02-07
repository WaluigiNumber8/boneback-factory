using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.UserInterface.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// Handles setup and button interactions for the Asset Card object.
    /// </summary>
    [RequireComponent(typeof(Toggle))]
    public class AssetCardController : AssetHolderBase, IToggleable
    {
        [SerializeField] private UIInfo ui;

        private int id;
        private AssetType type;
        private AssetBase asset; //Will be used for sorting cards
        private Toggle toggle;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnClick);
        }

        public override void Construct(AssetType type, int index, AssetBase asset, Image iconPos)
        {
            ui.icon = iconPos;
            Construct(type, index, asset);
        }

        public override void Construct(AssetType type, int index, AssetBase asset)
        {
            this.type = type;
            this.id = index;
            this.asset = asset;
            ui.title.text = asset.Title;
            ui.icon.sprite = asset.Icon;
            
            ui.infoGroup.gameObject.SetActive(true);
            ui.buttonGroup.gameObject.SetActive(false);
        }

        public void SetToggle(bool value) => toggle.isOn = value;

        /// <summary>
        /// Register the toggle under a <see cref="ToggleGroup"/>.
        /// </summary>
        /// <param name="group">The <see cref="ToggleGroup"/> to register the toggle under.</param>
        public void RegisterToggleGroup(ToggleGroup group) => toggle.group = group;
        
        /// <summary>
        /// Turns on/off button elements on the card.
        /// </summary>
        private void OnClick(bool value)
        {
            ui.infoGroup.gameObject.SetActive(!value);
            ui.buttonGroup.SetActive(value);
        }

        public override int Index { get => id; }
        public override AssetType Type { get => type; }
        public override AssetBase Asset { get => asset; }

        [System.Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public Image icon;
            public GameObject infoGroup;
            public GameObject buttonGroup;
        }
    }
}