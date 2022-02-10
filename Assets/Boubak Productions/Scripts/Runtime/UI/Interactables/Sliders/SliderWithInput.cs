using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static TMPro.TMP_InputField;

namespace BoubakProductions.UI.Sliders
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
        private bool ignoreValueChange;
        
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
            slider.onValueChanged.AddListener(SetSameValue);
            inputField.onValueChanged.AddListener(UpdateValue);
            inputField.onEndEdit.AddListener(FixValue);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(SetSameValue);
            inputField.onValueChanged.RemoveListener(UpdateValue);
            inputField.onEndEdit.RemoveListener(FixValue);
        }

        /// <summary>
        /// Sets a new value.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void ChangeValue(float value)
        {
            ignoreValueChange = true;
            slider.value = value;
        }
        
        /// <summary>
        /// Updates the value of the Slider and Input Field based on the input value & limits. 
        /// </summary>
        /// <param name="value">The value to enter.</param>
        private void UpdateValue(string value)
        {
            if (EqualsToSpecialSymbol(value)) return;
            UpdateValue(float.Parse(value));
        }
        /// <summary>
        /// Updates the value of the Slider and Input Field based on the input value & limits. 
        /// </summary>
        /// <param name="value">The value to enter.</param>
        private void UpdateValue(float value)
        {
            value = Mathf.Min(value, slider.maxValue);
            value = Mathf.Max(slider.minValue, value);
            
            SetSameValue(value);
        }

        /// <summary>
        /// Sets the value to 0, if it is equal to a special symbol.
        /// </summary>
        /// <param name="value">The value to check.</param>
        private void FixValue(string value)
        {
            if (EqualsToSpecialSymbol(value))
                SetSameValue(0);
        }
        
        /// <summary>
        /// Checks if a value is equal to special symbols or not.
        /// </summary>
        /// <param name="value">The value of the Input Field.</param>
        /// <returns>Is true if is equal to any special value.</returns>
        private bool EqualsToSpecialSymbol(string value)
        {
            if (value == "-") return true;
            return false;
        }
        
        /// <summary>
        /// Sets a same value to both the Slider & the Input Field.
        /// </summary>
        /// <param name="value">The value to set.</param>
        private void SetSameValue(float value)
        {
            inputField.text = value.ToString();
            slider.value = value;

            if (ignoreValueChange)
            {
                ignoreValueChange = false;
                return;
            }
            OnValueChanged?.Invoke(value);
        }
    }
}