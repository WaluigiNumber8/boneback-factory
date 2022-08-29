using System;
using UnityEngine;
using TMPro;

namespace RedRats.UI.InputFields
{
    /// <summary>
    /// Turns an <see cref="TMP_InputField"/> into an HTML Color Input Field.
    /// </summary>
    [RequireComponent(typeof(TMP_InputField))]
    public class HTMLColorField : MonoBehaviour
    {
        public event Action<Color> OnValueChanged;

        [SerializeField] private bool addHash;
        
        private TMP_InputField inputField;
        private bool ignoreValueChange;

        private void Awake() => inputField = GetComponent<TMP_InputField>();
        private void OnEnable()
        {
            inputField.onSubmit.AddListener(ProcessValue);
        }

        private void OnDisable()
        {
            inputField.onSubmit.RemoveListener(ProcessValue);
        }

        /// <summary>
        /// Sets a new HTML code into the input field based on color.
        /// </summary>
        /// <param name="color">The color to set.</param>
        public void ChangeValue(Color color)
        {
            ignoreValueChange = true;
            inputField.text = ColorUtility.ToHtmlStringRGB(color);
        }
        
        /// <summary>
        /// Turns the value into a color.
        /// </summary>
        /// <param name="value">The value from the Input Field.</param>
        private void ProcessValue(string value)
        {
            if (ignoreValueChange)
            {
                ignoreValueChange = false; 
                return;
            }
            if (addHash) value = "#" + value;
            if (ColorUtility.TryParseHtmlString(value, out Color color))
                OnValueChanged?.Invoke(color);
        }
        
    }
}

