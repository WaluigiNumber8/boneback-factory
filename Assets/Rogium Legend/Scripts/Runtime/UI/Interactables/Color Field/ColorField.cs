using System;
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
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            
            // Open ColorWindow
        }

        /// <summary>
        /// Construct the ColorField with initial values.
        /// </summary>
        /// <param name="value">The <see cref="Color"/> value to hold.</param>
        public void Construct(Color value) => this.value = value;

        
        /// <summary>
        /// Update everything based on the grabbed color.
        /// </summary>
        private void WhenColorPicked(Color color)
        {
            value = color;
            ui.color.color = color;
            OnValueChanged?.Invoke(color);
        }
        
        public Color Value { get => value; }
        
        [Serializable]
        public struct UIInfo
        {
            public Image color;
        }

    }
}