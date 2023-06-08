using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static TMPro.TMP_InputField;

namespace RedRats.UI.Sliders
{
    /// <summary>
    /// Allows a Slider to have an Input Field as an alternative method.
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class SliderWithInput : MonoBehaviour
    {
        public event Action<float> OnValueChanged;

        [SerializeField] private TMP_InputField inputField;

        private Slider slider;
        private bool changingValue;

        private void Awake()
        {
            if (inputField.characterValidation != CharacterValidation.Integer &&
                inputField.characterValidation != CharacterValidation.Decimal &&
                inputField.characterValidation != CharacterValidation.Digit)
                throw new InvalidOperationException("Cannot work with non-numeric input fields! Change validation to numbers only.");

            slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(SetToInputField);
            inputField.onValueChanged.AddListener(SetToSlider);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(SetToInputField);
            inputField.onValueChanged.RemoveListener(SetToSlider);
        }

        /// <summary>
        /// Sets a new value.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void SetValue(float value)
        {
            if (changingValue) return;

            changingValue = true;
            slider.value = value;
            inputField.text = value.ToString();
            changingValue = false;

            OnValueChanged?.Invoke(value);
        }

        /// <summary>
        /// Set a value into the attached input field only.
        /// </summary>
        /// <param name="value">The value to set.</param>
        private void SetToInputField(float value)
        {
            if (changingValue) return;

            changingValue = true;
            inputField.text = value.ToString();
            changingValue = false;

            OnValueChanged?.Invoke(value);
        }

        /// <summary>
        /// Set a value to the slider only.
        /// </summary>
        /// <param name="stringValue">The value to set.</param>
        private void SetToSlider(string stringValue)
        {
            if (changingValue) return;
            if (EqualsToSpecialSymbol(stringValue)) return;

            changingValue = true;
            float value = Mathf.Clamp(float.Parse(stringValue), slider.minValue, slider.maxValue);
            slider.value = value;
            inputField.text = value.ToString();
            changingValue = false;

            OnValueChanged?.Invoke(value);
        }

        /// <summary>
        /// Checks if a value is equal to special symbols or not.
        /// </summary>
        /// <param name="value">The value of the Input Field.</param>
        /// <returns>Is true if is equal to any special value.</returns>
        private bool EqualsToSpecialSymbol(string value) => value == "-";
    }
}