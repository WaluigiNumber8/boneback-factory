using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.Global.UISystem.UI
{
    /// <summary>
    /// Handles setup and button interactions for the Asset Card object.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class AssetCardController : MonoBehaviour, IAssetHolder
    {
        [SerializeField] private AssetType type;
        [SerializeField] private UIInfo ui;

        private IAsset asset; //Will be used for sorting cards
        private int id;
        private Button cardButton;

        private void Start()
        {
            cardButton = GetComponent<Button>();
            cardButton.onClick.AddListener(OnClick);
        }

        public void Construct(AssetType type, int id, IAsset asset, Image iconPos)
        {
            ui.icon = iconPos;
            Construct(type, id, asset);
        }
        public void Construct(AssetType type, int id, IAsset asset)
        {
            this.type = type;
            this.id = id;
            this.asset = asset;
            ui.title.text = asset.Title;
            ui.icon.sprite = asset.Icon;
            
            ui.infoGroup.gameObject.SetActive(true);
        }

        /// <summary>
        /// Turns on/off button elements on the card.
        /// </summary>
        private void OnClick()
        {
            ui.infoGroup.gameObject.SetActive(!ui.infoGroup.gameObject.activeSelf);
            ui.buttonGroup.SetActive(!ui.buttonGroup.activeSelf);
        }

        public AssetType Type { get => type; }
        public int ID { get => id; }

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