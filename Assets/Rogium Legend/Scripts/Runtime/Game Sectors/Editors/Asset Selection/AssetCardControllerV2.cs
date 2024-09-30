using System;
using Rogium.UserInterface.Editors.AssetSelection;
using Rogium.UserInterface.Interactables;
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

        private int index;
        
        protected override void Awake()
        {
            base.Awake();
            toggle.onValueChanged.AddListener(ToggleDisplayedGroups);
        }

        public void Construct(AssetCardData data)
        {
            index = data.index;
            ui.title.text = data.title;
            ui.iconImage.sprite = data.icon;
            ui.editButton.onClick.AddListener(() => InteractableInput.Handle(data.whenAssetEdit, index));
            ui.configButton.onClick.AddListener(() => InteractableInput.Handle(data.whenAssetConfig, index));
            ui.deleteButton.onClick.AddListener(() => InteractableInput.Handle(data.whenAssetDelete, index));
        }
        
        /// <summary>
        /// Edit the asset.
        /// </summary>
        public void Edit() => ui.editButton.onClick.Invoke();
        /// <summary>
        /// Configure the asset's properties.
        /// </summary>
        public void Config() => ui.configButton.onClick.Invoke();
        /// <summary>
        /// Delete the asset.
        /// </summary>
        public void Delete() => ui.deleteButton.onClick.Invoke();

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
            public Button editButton;
            public Button configButton;
            public Button deleteButton;
            public RectTransform infoGroup;
            public RectTransform buttonGroup;
        }
    }
}