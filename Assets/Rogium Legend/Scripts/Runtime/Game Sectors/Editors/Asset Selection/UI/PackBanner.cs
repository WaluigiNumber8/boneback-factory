using System;
using Rogium.Editors.Packs;
using Rogium.UserInterface.Editors.ModalWindowBuilding;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.Editors.NewAssetSelection.UI
{
    /// <summary>
    /// Controls everything happening on the Navigation Bar. Everything from what buttons do what, to what shows up.
    /// </summary>
    public class PackBanner : MonoBehaviour
    {
        [SerializeField] private NavBarUIInfo ui;

        private void Awake() => ui.configButton.onClick.AddListener(Config);

        public void Construct(PackAsset asset)
        {
            ui.packTitleText.text = asset.Title;
            ui.packIcon.sprite = asset.Icon;
        }
        
        public string Title { get => ui.packTitleText.text; }
        public Sprite Icon { get => ui.packIcon.sprite; }
        
        [Serializable]
        public struct NavBarUIInfo
        {
            public TextMeshProUGUI packTitleText;
            public Image packIcon;
            public Button configButton;
        }

        public void Config()
        {
            new ModalWindowPropertyBuilderPack().OpenForUpdate();
        }
    }
}