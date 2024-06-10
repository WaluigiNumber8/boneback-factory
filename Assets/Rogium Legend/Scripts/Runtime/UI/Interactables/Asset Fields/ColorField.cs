using System;
using Rogium.Editors.Core.Defaults;
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
        [SerializeField] private UIInfo ui;

        private Color value = EditorConstants.DefaultColor;
        private Color lastValue = EditorConstants.DefaultColor;
        private Action<Color> whenValueChanged;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            ModalWindowBuilder.GetInstance().OpenColorPickerWindow(WhenColorPicked, value);
        }

        /// <summary>
        /// Construct the ColorField with initial values.
        /// </summary>
        /// <param name="value">The <see cref="Color"/> value to hold.</param>
        /// <param name="whenValueChanged">Runs when the field's color changes.</param>
        public void Construct(Color value, Action<Color> whenValueChanged)
        {
            this.value = value;
            this.lastValue = value;
            this.whenValueChanged = whenValueChanged;
            ui.color.color = value;
        }

        public void UpdateValue(Color value)
        {
            this.lastValue = this.value;
            this.value = value;
            this.whenValueChanged?.Invoke(value);
            
            ui.color.color = value;
        }
        
        
        /// <summary>
        /// Update everything based on the grabbed color.
        /// </summary>
        private void WhenColorPicked(Color value)
        {
            ActionHistorySystem.AddAndExecute(new UpdateColorFieldAction(this, value, lastValue, whenValueChanged));
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