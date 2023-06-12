using RedRats.UI.InputFields;
using RedRats.UI.Sliders;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.Palettes
{
    /// <summary>
    /// Manages the color picker.
    /// </summary>
    public class ColorPickerManager : MonoBehaviour
    {
        private const int ConversionValue = 255;
        
        [SerializeField] private Image colorGuide;
        [SerializeField] private SliderInfo sliders;
        [SerializeField] private HTMLColorField htmlField;
        
        private Color currentColor;
        private Image otherGuideImage;
        private bool cannotUpdateHTML;

        private void OnEnable()
        {
            sliders.r.OnValueChanged += UpdateColorR;
            sliders.g.OnValueChanged += UpdateColorG;
            sliders.b.OnValueChanged += UpdateColorB;
            htmlField.OnValueChanged += UpdateColorHTML;
        }

        private void OnDisable()
        {
            sliders.r.OnValueChanged -= UpdateColorR;
            sliders.g.OnValueChanged -= UpdateColorG;
            sliders.b.OnValueChanged -= UpdateColorB;
            htmlField.OnValueChanged -= UpdateColorHTML;
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

        #region Refresh Methods

        /// <summary>
        /// Refreshes the Color Guide with the currentColor.
        /// </summary>
        private void RefreshColorGuide()
        {
            colorGuide.color = currentColor;
            if (otherGuideImage != null)
                otherGuideImage.color = currentColor;
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

        /// <summary>
        /// Updates the red color of currentColor. Should be in range 0-255.
        /// </summary>
        /// <param name="value">The new value (0-255) of the red component.</param>
        private void UpdateColorR(float value)
        {
            currentColor.r = value / ConversionValue;
            RefreshHTMLField();
        }
        
        /// <summary>
        /// Updates the green color of currentColor. Should be in range 0-255.
        /// </summary>
        /// <param name="value">The new value (0-255) of the green component.</param>
        private void UpdateColorG(float value)
        {
            currentColor.g = value / ConversionValue;
            RefreshHTMLField();
        }
        
        /// <summary>
        /// Updates the blue color of currentColor. Should be in range 0-255.
        /// </summary>
        /// <param name="value">The new value (0-255) of the blue component.</param>
        private void UpdateColorB(float value)
        {
            currentColor.b = value / ConversionValue;
            RefreshHTMLField();
        }

        private void UpdateColorHTML(Color color)
        {
            currentColor = color;
            cannotUpdateHTML = true;
            RefreshSliders();
            cannotUpdateHTML = false;
        }

        #endregion

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