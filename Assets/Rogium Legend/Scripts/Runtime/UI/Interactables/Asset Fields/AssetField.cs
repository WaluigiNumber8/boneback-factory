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
        public event Action OnValueEmptied;

        [SerializeField] private AssetType type;
        [SerializeField] private bool canBeEmpty;
        [SerializeField] private UIInfo ui;

        private Action<IAsset> whenValueChanged;
        private IAsset value;
        private IAsset lastValue;
        
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
            
            OpenWindow();
        }

        public void OpenWindow() => ModalWindowBuilder.Instance.OpenAssetPickerWindow(type, WhenAssetPicked, value, canBeEmpty);

        /// <summary>
        /// Constructs the asset field with initial values.
        /// </summary>
        /// <param name="type">The type of asset to collect.</param>
        /// <param name="value">The starting value of the AssetField.</param>
        /// <param name="whenValueChanged">Runs when the value of the asset field was changed.</param>
        /// <param name="canBeEmpty">Allow the AssetField to contain a <see cref="EmptyAsset"/>. It gets added as an option to the Asset Picker Menu.</param>
        public void Construct(AssetType type, IAsset value, Action<IAsset> whenValueChanged, bool canBeEmpty = false)
        {
            this.type = type;
            this.lastValue = value;
            this.value = value;
            this.whenValueChanged = whenValueChanged;
            this.canBeEmpty = canBeEmpty;
            Refresh();
        }

        public void UpdateValue(IAsset value)
        {
            lastValue = value;
            this.value = value;
            this.whenValueChanged?.Invoke(value);
            Refresh();
        }
        
        /// <summary>
        /// Update everything based on the grabbed sprite.
        /// </summary>
        /// <param name="value">The sprite to update with.</param>
        private void WhenAssetPicked(IAsset value)
        {
            ActionHistorySystem.AddAndExecute(new UpdateAssetFieldAction(this, value, lastValue, whenValueChanged));
            lastValue = value;
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