using System;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Palettes;
using UnityEngine;

namespace Rogium.UserInterface.ModalWindows
{
    /// <summary>
    /// Represents a modal window that allows the user to pick a color.
    /// </summary>
    public class ColorPickerWindow : ModalWindowBase
    {
        [SerializeField] private ColorPicker colorPicker;
        
        private Color currentColor;
        private Action<Color> onChangeColor;

        public void Construct(Action<Color> onChangeColor, Color value)
        {
            currentColor = value;
            this.onChangeColor = onChangeColor;
            
            colorPicker.Construct(currentColor);
            colorPicker.OnValueChanged += this.onChangeColor;
        }

        public override void Close()
        {
            base.Close();
            colorPicker.OnValueChanged -= onChangeColor;
        }

        protected override void UpdateTheme()
        {
            
        }
    }
}