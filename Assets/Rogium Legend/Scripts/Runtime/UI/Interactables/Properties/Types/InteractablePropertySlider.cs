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

        private Action<float> whenValueChange;
        private float oldValue;
        private bool usesFloatValues;
        private int decimalMultiplier;

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
            oldValue = Mathf.RoundToInt(startingValue);

            this.whenValueChange = whenValueChange;
            slider.onValueChanged.AddListener(WhenValueChanges);
            decimals.sliderWithInput.OnValueChanged += WhenValueChanges;
            usesFloatValues = true;
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
            ConstructTitle(titleText);
            
            decimals.sliderWithInput.ResetDecimalMultiplier();
            inputField.UpdateContentType(TMP_InputField.ContentType.IntegerNumber);
            slider.maxValue = maxValue;
            slider.minValue = minValue;
            decimals.sliderWithInput.SetValue(startingValue);
            oldValue = startingValue;
            
            this.whenValueChange = whenValueChange;
            slider.onValueChanged.AddListener(WhenValueChanges);
            decimals.sliderWithInput.OnValueChanged += WhenValueChanges;
            usesFloatValues = false;
        }

        /// <summary>
        /// Updates the dropdown value without invoking the value change event. Assigned <see cref="whenValueChange"/> action still runs.
        /// </summary>
        /// <param name="value">The new value for the dropdown.</param>
        public void UpdateValueWithoutNotify(float value)
        {
            oldValue = slider.value;
            decimals.sliderWithInput.SetValue(value);
            whenValueChange?.Invoke((usesFloatValues) ? value * decimalMultiplier : value);
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

        private void WhenValueChanges(float value)
        {
            value = (usesFloatValues) ? value / decimalMultiplier : value;
            ActionHistorySystem.AddAndExecute(new UpdateSliderAction(this, value, oldValue));
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