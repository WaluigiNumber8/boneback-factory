using System;
using RedRats.UI.Core;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Palettes;
using Rogium.Systems.ThemeSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.ModalWindows
{
    /// <summary>
    /// Represents a modal window that allows the user to pick a color.
    /// </summary>
    public class ColorPickerWindow : ModalWindowBase
    {
        [SerializeField] private UIInfo ui;
        [SerializeField] private ColorPicker colorPicker;
        
        private Color currentColor;
        private Action<Color> onChangeColor;

        public void Construct(Action<Color> onChangeColor, Color value)
        {
            currentColor = value;
            this.onChangeColor = onChangeColor;
            
            colorPicker.UpdateValue(currentColor);
            colorPicker.OnValueChanged += this.onChangeColor;
        }

        public void UpdateColor(Color value) => colorPicker.UpdateValue(value);

        public void UpdateTheme(InteractableSpriteInfo buttonSet, InteractableSpriteInfo inputFieldSet,
                                Sprite windowBackgroundSprite, Sprite colorGuideBorder, Sprite actionRowBackground, 
                                FontInfo inputFont)
        {
            UIExtensions.ChangeInteractableSprites(generalUI.closeButton, buttonSet);
            colorPicker.UpdateTheme(inputFieldSet, inputFont);
            generalUI.windowArea.sprite = windowBackgroundSprite;
            ui.colorGuideBorder.sprite = colorGuideBorder;
            ui.topActionRowBackground.sprite = actionRowBackground;
        }
        
        public override void Close()
        {
            base.Close();
            colorPicker.OnValueChanged -= onChangeColor;
        }

        protected override void UpdateTheme() => ThemeUpdaterRogium.UpdateColorPickerWindow(this);

        [Serializable]
        public struct UIInfo
        {
            public Image colorGuideBorder;
            public Image topActionRowBackground;
        }
    }
}