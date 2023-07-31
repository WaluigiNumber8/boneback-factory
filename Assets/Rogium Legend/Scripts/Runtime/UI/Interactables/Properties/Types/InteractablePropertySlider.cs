using System;
using RedRats.UI.Core;
using RedRats.UI.Sliders;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Prepares the Slider property for correct use.
    /// </summary>
    public class InteractablePropertySlider : InteractablePropertyBase
    {
        [SerializeField] private Slider slider;
        [SerializeField] private InteractablePropertyInputField inputField;

        [SerializeField] private DecimalInfo decimals;
        [SerializeField] private UIInfo ui;

        private int decimalMultiplier;

        private void OnEnable()
        {
            decimalMultiplier = 1;
            for (int i = 0; i < decimals.allowedDecimals; i++) decimalMultiplier *= 10;
        }

        public override void SetDisabled(bool isDisabled)
        {
            slider.interactable = !isDisabled;
            if (inputField != null) inputField.SetDisabled(isDisabled);
        }

        /// <summary>
        /// Sets the property title and state.
        /// </summary>
        /// <param name="titleText">The text of the property title.</param>
        /// <param name="minValue">Minimum allowed value on the slider.</param>
        /// <param name="maxValue">Maximum allowed value on the slider.</param>
        /// <param name="startingValue">Starting value of the slider.</param>
        /// <param name="whenValueChange">Method that will run when the slider changes value.</param>
        public void Construct(string titleText, float minValue, float maxValue, float startingValue, Action<float> whenValueChange)
        {
            title.text = titleText;
            title.gameObject.SetActive((titleText != ""));
            if (ui.emptySpace != null) ui.emptySpace.SetActive((titleText != ""));
            
            decimals.sliderWithInput.OverrideDecimalMultiplier(decimalMultiplier);
            inputField.UpdateContentType(TMP_InputField.ContentType.DecimalNumber);
            slider.maxValue = Mathf.RoundToInt(maxValue * decimalMultiplier);
            slider.minValue = Mathf.RoundToInt(minValue * decimalMultiplier);
            slider.value = Mathf.RoundToInt(startingValue * decimalMultiplier);
            slider.onValueChanged.AddListener(_ => whenValueChange(slider.value / decimalMultiplier));
        }
        
        /// <summary>
        /// Sets the property title and state.
        /// </summary>
        /// <param name="titleText">The text of the property title.</param>
        /// <param name="minValue">Minimum allowed value on the slider.</param>
        /// <param name="maxValue">Maximum allowed value on the slider.</param>
        /// <param name="startingValue">Starting value of the slider.</param>
        /// <param name="whenValueChange">Method that will run when the slider changes value.</param>
        public void Construct(string titleText, int minValue, int maxValue, int startingValue, Action<float> whenValueChange)
        {
            title.text = titleText;
            title.gameObject.SetActive((titleText != ""));
            if (ui.emptySpace != null) ui.emptySpace.SetActive((titleText != ""));
            
            decimals.sliderWithInput.ResetDecimalMultiplier();
            inputField.UpdateContentType(TMP_InputField.ContentType.IntegerNumber);
            slider.maxValue = maxValue;
            slider.minValue = minValue;
            slider.value = startingValue;
            slider.onValueChanged.AddListener(_ => whenValueChange(slider.value));
        }

        /// <summary>
        /// Updates the slider's UI elements.
        /// </summary>
        /// <param name="sliderSet">The Slider graphics.</param>
        /// <param name="backgroundSprite">Background of the slider.</param>
        /// <param name="handleSprite">Slider's handle.</param>
        /// <param name="titleFont">Font of the property title.</param>
        public void UpdateTheme(InteractableSpriteInfo sliderSet, Sprite backgroundSprite, Sprite handleSprite, FontInfo titleFont)
        {
            UIExtensions.ChangeInteractableSprites(slider, ui.fillImage, sliderSet);
            UIExtensions.ChangeFont(title, titleFont);
            ui.backgroundImage.sprite = backgroundSprite;
            ui.handleImage.sprite = handleSprite;
        }
        
        public InteractablePropertyInputField InputField { get => inputField; }

        [Serializable]
        public struct DecimalInfo
        {
            public int allowedDecimals;
            public SliderWithInput sliderWithInput;
        }
        
        [Serializable]
        public struct UIInfo
        {
            public Image fillImage;
            public Image backgroundImage;
            public Image handleImage;
            public GameObject emptySpace;
        }
    }
}