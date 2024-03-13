using System;
using RedRats.UI.Core;
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

        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private bool addHash;
        
        private bool ignoreValueChange;

        private void Awake()
        {
            if (inputField == null) inputField = GetComponent<TMP_InputField>();
        }

        private void OnEnable() => inputField.onValueChanged.AddListener(ProcessValue);
        private void OnDisable() => inputField.onValueChanged.RemoveListener(ProcessValue);

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
            if (ignoreValueChange) { ignoreValueChange = false; return; }
            
            if (value.Length < 6)
            {
                string v = value + new string('F', 6 - value.Length);
                value = v;
            }
            if (addHash) value = "#" + value;
            if (ColorUtility.TryParseHtmlString(value, out Color color))
            {
                OnValueChanged?.Invoke(color);
            }
        }
        
        /// <summary>
        /// Updates the elements sprites to match the editor theme.
        /// </summary>
        /// <param name="htmlFieldSpriteSet">Sprite set for InputFields.</param>
        /// <param name="inputFont">Font used for input text.</param>
        public void UpdateTheme(InteractableSpriteInfo htmlFieldSpriteSet, FontInfo inputFont)
        {
            UIExtensions.ChangeInteractableSprites(inputField, htmlFieldSpriteSet);
            UIExtensions.ChangeFont(inputField.textComponent, inputFont);
        }
    }
}

