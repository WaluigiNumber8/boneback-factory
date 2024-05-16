using System;
using Rogium.Systems.ActionHistory;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables
{
    /// <summary>
    /// Stores a single color, that can be edited via a modal window.
    /// </summary>
    public class ColorField : Selectable
    {
        public event Action<Color> OnValueChanged;
        
        [SerializeField] private UIInfo ui;

        private Color value;
        private Color lastValue;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            ModalWindowBuilder.GetInstance().OpenColorPickerWindow(WhenColorPicked, value);
        }

        /// <summary>
        /// Construct the ColorField with initial values.
        /// </summary>
        /// <param name="value">The <see cref="Color"/> value to hold.</param>
        public void Construct(Color value)
        {
            this.value = value;
            this.lastValue = value;
            ui.color.color = value;
        }

        public void UpdateValue(Color value)
        {
            this.lastValue = this.value;
            this.value = value;
            ui.color.color = value;
            OnValueChanged?.Invoke(value);
        }
        
        
        /// <summary>
        /// Update everything based on the grabbed color.
        /// </summary>
        private void WhenColorPicked(Color value)
        {
            ActionHistorySystem.GetInstance().AddAndExecute(new UpdateColorFieldAction(this, value, lastValue));
            lastValue = value;
        }
        
        public Color Value { get => value; }
        
        [Serializable]
        public struct UIInfo
        {
            public Image color;
        }

    }
}