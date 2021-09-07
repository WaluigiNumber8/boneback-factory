using RogiumLegend.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RogiumLegend.Editors;

namespace RogiumLegend.Global.MenuSystem.UI
{
    /// <summary>
    /// Handles setup and button interactions for the Asset Card object.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class AssetCardController : MonoBehaviour, IAssetHolder
    {
        [SerializeField] private AssetType type;
        [SerializeField] private UIInfo ui;

        private IAsset asset;
        private int id;
        private Button cardButton;

        private void Start()
        {
            cardButton = GetComponent<Button>();
            cardButton.onClick.AddListener(OnClick);
        }

        public void Construct(AssetType type, int id, IAsset asset)
        {
            this.type = type;
            this.id = id;
            this.asset = asset;
            ui.title.text = asset.Title;
            ui.icon.sprite = asset.Icon;
        }

        /// <summary>
        /// Turns on/off button elements on the card.
        /// </summary>
        private void OnClick()
        {
            ui.buttonGroup.SetActive(!ui.buttonGroup.activeSelf);
        }

        /// <summary>
        /// Calls the correct action when the Edit Button was pressed.
        /// </summary>
        private void OnEdit()
        {
            
        }

        /// <summary>
        /// Calls the correct action when the Config Button was pressed.
        /// </summary>
        private void OnConfig()
        {

        }

        /// <summary>
        /// Calls the correct action when the Remove Button was pressed.
        /// </summary>
        private void OnDelete()
        {

        }

        public AssetType Type { get => type; }
        public int ID { get => id; }

        [System.Serializable]
        public struct UIInfo
        {
            public TextMeshProUGUI title;
            public Image icon;
            public GameObject buttonGroup;
            public Button editButton;
            public Button configButton;
            public Button removeButton;
        }
    }
}