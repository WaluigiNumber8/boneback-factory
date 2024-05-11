using System;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Systems.ActionHistory;
using Rogium.UserInterface.ModalWindows;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Allows the user to grab assets as input.
    /// </summary>
    public class AssetField : Selectable, IAssetField<IAsset>, IPointerClickHandler
    {
        public event Action<IAsset> OnValueChanged;
        public event Action OnValueEmptied;

        [SerializeField] private AssetType type;
        [SerializeField] private bool canBeEmpty;
        [SerializeField] private UIInfo ui;

        private IAsset value;
        private IAsset oldValue;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable) return;
            
            //Right Click to remove asset when can be empty
            if (canBeEmpty && eventData.button == PointerEventData.InputButton.Right)
            {
                WhenAssetPicked(new EmptyAsset());
                OnValueEmptied?.Invoke();
                return;
            }
            
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            ModalWindowBuilder.GetInstance().OpenAssetPickerWindow(type, WhenAssetPicked, value, canBeEmpty);
        }

        /// <summary>
        /// Constructs the asset field with initial values.
        /// </summary>
        /// <param name="type">The type of asset to collect.</param>
        /// <param name="value">The starting value of the AssetField.</param>
        /// <param name="canBeEmpty">Allow the AssetField to contain a <see cref="EmptyAsset"/>. It gets added as an option to the Asset Picker Menu.</param>
        public void Construct(AssetType type, IAsset value, bool canBeEmpty = false)
        {
            this.type = type;
            this.oldValue = value;
            this.value = value;
            this.canBeEmpty = canBeEmpty;
            
            Refresh();
        }

        public void UpdateValue(IAsset value)
        {
            oldValue = value;
            this.value = value;
            Refresh();
            OnValueChanged?.Invoke(value);
        }
        
        /// <summary>
        /// Update everything based on the grabbed sprite.
        /// </summary>
        /// <param name="value">The sprite to update with.</param>
        private void WhenAssetPicked(IAsset value)
        {
            ActionHistorySystem.AddAndExecute(new UpdateAssetFieldAction(this, value, oldValue));
        }

        private void Refresh()
        {
            ui.icon.SetSpriteFromAsset(value);
            ui.title.SetTextValueFromAssetTitle(value);
        }

        public IAsset Value { get => value; }
        public Image Icon { get => ui.icon; }
        public TextMeshProUGUI Title { get => ui.title; }
        
        [Serializable]
        public struct UIInfo
        {
            public Image icon;
            public TextMeshProUGUI title;
        }
    }
}