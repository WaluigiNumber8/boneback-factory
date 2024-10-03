using System;
using Rogium.UserInterface.Interactables;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Represents an asset in the form of a card that has buttons for editing it.
    /// </summary>
    public class EditableAssetCardControllerV2 : AssetCardControllerV2
    {
        [SerializeField] private EditableUIInfo editUI;

        protected override void Awake()
        {
            base.Awake();
            toggle.onValueChanged.AddListener(ToggleDisplayedGroups);
        }

        public override void Construct(AssetCardData data)
        {
            base.Construct(data);
            editUI.editButton.onClick.AddListener(() => InteractableInput.Handle(data.whenAssetEdit, index));
            editUI.configButton.onClick.AddListener(() => InteractableInput.Handle(data.whenAssetConfig, index));
            editUI.deleteButton.onClick.AddListener(() => InteractableInput.Handle(data.whenAssetDelete, index));
        }
        
        /// <summary>
        /// Edit the asset.
        /// </summary>
        public void Edit() => editUI.editButton.onClick.Invoke();
        /// <summary>
        /// Configure the asset's properties.
        /// </summary>
        public void Config() => editUI.configButton.onClick.Invoke();
        /// <summary>
        /// Delete the asset.
        /// </summary>
        public void Delete() => editUI.deleteButton.onClick.Invoke();

        private void ToggleDisplayedGroups(bool value)
        {
            editUI.infoGroup.gameObject.SetActive(!value);
            editUI.buttonGroup.gameObject.SetActive(value);
        }
        
        public bool IsInfoGroupShown => editUI.infoGroup.gameObject.activeSelf;
        public bool IsButtonGroupShown => editUI.buttonGroup.gameObject.activeSelf;
        
        [Serializable]
        public struct EditableUIInfo
        {
            public Button editButton;
            public Button configButton;
            public Button deleteButton;
            public RectTransform infoGroup;
            public RectTransform buttonGroup;
        }
    }
}