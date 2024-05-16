using System;
using RedRats.UI.Core;
using RedRats.UI.Sliders;
using Rogium.Systems.ActionHistory;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Prepares the Slider property for correct use.
    /// </summary>
    public class InteractablePropertySlider : InteractablePropertyBase<float>
    {
        [SerializeField] private Slider slider;
        [SerializeField] private InteractablePropertyInputField inputField;

        [SerializeField] private DecimalInfo decimals;
        [SerializeField] private UIInfo ui;

        private int decimalMultiplier;
        private float lastValue;
        private Action<float> whenValueChanged;

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
            decimalMultiplier = 1;
            for (int i = 0; i < decimals.allowedDecimals; i++) decimalMultiplier *= 10;
            decimals.sliderWithInput.OverrideDecimalMultiplier(decimalMultiplier);
            
            ConstructTitle(titleText);
            
            inputField.UpdateContentType(TMP_InputField.ContentType.DecimalNumber);
            slider.maxValue = Mathf.RoundToInt(maxValue * decimalMultiplier);
            slider.minValue = Mathf.RoundToInt(minValue * decimalMultiplier);
            decimals.sliderWithInput.SetValue(startingValue);
            lastValue = startingValue * decimalMultiplier;
            
            this.whenValueChanged = whenValueChange;
            slider.onValueChanged.AddListener(WhenValueChanged);
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
            decimalMultiplier = 1;
            decimals.sliderWithInput.ResetDecimalMultiplier();
            
            ConstructTitle(titleText);
            
            inputField.UpdateContentType(TMP_InputField.ContentType.IntegerNumber);
            slider.maxValue = maxValue;
            slider.minValue = minValue;
            UpdateValueWithoutNotify(startingValue);
            
            this.whenValueChanged = whenValueChange;
            slider.onValueChanged.AddListener(value => WhenValueChanged((int)value));
        }

        public void UpdateValueWithoutNotify(float value)
        {
            decimals.sliderWithInput.SetValue(value);
            whenValueChanged?.Invoke(value);
            lastValue = value * decimalMultiplier;
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

        private void WhenValueChanged(float value)
        {
            ActionHistorySystem.AddAndExecute(new UpdateSliderAction(this, value / decimalMultiplier, lastValue / decimalMultiplier));
            lastValue = value;
        }
        
        private void WhenValueChanged(int value)
        {
            ActionHistorySystem.AddAndExecute(new UpdateSliderAction(this, value, lastValue));
            lastValue = value;
        }
        
        public override float PropertyValue { get => slider.value / decimalMultiplier; }
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
        }
    }
}