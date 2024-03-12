using System;
using RedRats.UI.ModalWindows;
using UnityEngine;

namespace Rogium.UserInterface.ModalWindows
{
    /// <summary>
    /// Represents a modal window that allows the user to pick a color.
    /// </summary>
    public class ColorPickerWindow : ModalWindowBase
    {

        private Color currentColor;
        
        public void Construct(Action<Color> onChangeColor, Color value)
        {
            currentColor = value;
        }
      
        
        protected override void UpdateTheme()
        {
            
        }
    }
}