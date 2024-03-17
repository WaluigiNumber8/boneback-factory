using System;
using RedRats.UI.Core;
using RedRats.UI.InputFields;
using RedRats.UI.Sliders;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Represents a color picker, that allows the user to pick a color using sliders and a HTML field.
    /// </summary>
    public class ColorPicker : MonoBehaviour
    {
        private const int ConversionValue = 255;

        public event Action<Color> OnValueChanged;
        
        [SerializeField] private Image colorGuide;
        [SerializeField] private SliderInfo sliders;
        [SerializeField] private HTMLColorField htmlField;
        [SerializeField] private ColorWheel colorWheel;
        
        private Image otherGuideImage;
        
        private Color currentColor;
        private bool cannotUpdateHTML;

        private void OnEnable()
        {
            sliders.r.OnValueChanged += UpdateColorR;
            sliders.g.OnValueChanged += UpdateColorG;
            sliders.b.OnValueChanged += UpdateColorB;
            htmlField.OnValueChanged += UpdateColorHTML;
            if (colorWheel != null) colorWheel.OnValueChanged += UpdateColor;
        }

        private void OnDisable()
        {
            sliders.r.OnValueChanged -= UpdateColorR;
            sliders.g.OnValueChanged -= UpdateColorG;
            sliders.b.OnValueChanged -= UpdateColorB;
            htmlField.OnValueChanged -= UpdateColorHTML;
            if (colorWheel != null) colorWheel.OnValueChanged -= UpdateColor;
        }

        /// <summary>
        /// Assigns a new color to the Color Picker.
        /// </summary>
        /// <param name="color">The new color to assign.</param>
        /// <param name="otherGuideImage">A secondary image, that will also get it's color changed.</param>
        public void Construct(Color color, Image otherGuideImage)
        {
            Construct(color);
            this.otherGuideImage = otherGuideImage;
        }
        /// <summary>
        /// Assigns a new color to the Color Picker.
        /// </summary>
        /// <param name="color">The new color to assign.</param>
        public void Construct(Color color)
        {
            this.currentColor = color;
            this.otherGuideImage = null;
            
            RefreshSliders();
            RefreshHTMLField();
            RefreshColorGuide();
        }

        /// <summary>
        /// Updates the theme of the Color Picker.
        /// </summary>
        /// <param name="inputFieldSpriteSet">Sprite set used for all input fields.</param>
        /// <param name="inputFont">Font used for the input text.</param>
        public void UpdateTheme(InteractableSpriteInfo inputFieldSpriteSet, FontInfo inputFont)
        {
            UIExtensions.ChangeInteractableSprites(sliders.r.InputField, sliders.r.InputField.image, inputFieldSpriteSet);
            UIExtensions.ChangeInteractableSprites(sliders.g.InputField, sliders.g.InputField.image, inputFieldSpriteSet);
            UIExtensions.ChangeInteractableSprites(sliders.b.InputField, sliders.b.InputField.image, inputFieldSpriteSet);
            UIExtensions.ChangeFont(sliders.r.InputField.textComponent, inputFont);
            UIExtensions.ChangeFont(sliders.g.InputField.textComponent, inputFont);
            UIExtensions.ChangeFont(sliders.b.InputField.textComponent, inputFont);
            htmlField.UpdateTheme(inputFieldSpriteSet, inputFont);
        }
        
        #region Refresh Methods

        /// <summary>
        /// Refreshes the Color Guide with the currentColor.
        /// </summary>
        private void RefreshColorGuide()
        {
            if (colorGuide != null) colorGuide.color = currentColor;
            if (otherGuideImage != null) otherGuideImage.color = currentColor;
        }
        
        /// <summary>
        /// Refreshes the HTML field.
        /// </summary>
        private void RefreshHTMLField()
        {
            if (!cannotUpdateHTML) htmlField.ChangeValue(currentColor);
            RefreshColorGuide();
        }

        /// <summary>
        /// Refreshes all the sliders.
        /// </summary>
        private void RefreshSliders()
        {
            sliders.r.SetValue(currentColor.r * ConversionValue);
            sliders.g.SetValue(currentColor.g * ConversionValue);
            sliders.b.SetValue(currentColor.b * ConversionValue);
            RefreshColorGuide();
        }

        #endregion

        #region Update Color

        private void UpdateColor(Color color)
        {
            currentColor = color;
            RefreshSliders();
            RefreshHTMLField();
            WhenValueChanged();
        }
        
        /// <summary>
        /// Updates the red color of currentColor. Should be in range 0-255.
        /// </summary>
        /// <param name="value">The new value (0-255) of the red component.</param>
        private void UpdateColorR(float value)
        {
            currentColor.r = value / ConversionValue;
            RefreshHTMLField();
            WhenValueChanged();
        }
        
        /// <summary>
        /// Updates the green color of currentColor. Should be in range 0-255.
        /// </summary>
        /// <param name="value">The new value (0-255) of the green component.</param>
        private void UpdateColorG(float value)
        {
            currentColor.g = value / ConversionValue;
            RefreshHTMLField();
            WhenValueChanged();
        }
        
        /// <summary>
        /// Updates the blue color of currentColor. Should be in range 0-255.
        /// </summary>
        /// <param name="value">The new value (0-255) of the blue component.</param>
        private void UpdateColorB(float value)
        {
            currentColor.b = value / ConversionValue;
            RefreshHTMLField();
            WhenValueChanged();
        }

        private void UpdateColorHTML(Color color)
        {
            currentColor = color;
            cannotUpdateHTML = true;
            RefreshSliders();
            cannotUpdateHTML = false;
            WhenValueChanged();
        }

        #endregion

        private void WhenValueChanged() => OnValueChanged?.Invoke(currentColor);

        public Color CurrentColor { get => currentColor; }
        
        [System.Serializable]
        public struct SliderInfo
        {
            public SliderWithInput r;
            public SliderWithInput g;
            public SliderWithInput b;
        }
    }
}