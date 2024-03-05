using RedRats.UI.Core;
using UnityEngine;
using TMPro;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a plain text interactable property.
    /// </summary>
    public class InteractablePropertyPlainText : InteractablePropertyBase
    {
        [SerializeField] private TextMeshProUGUI plainText;
        
        public override void SetDisabled(bool isDisabled) {}
        
        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// <param name="text">Text in the property.</param>
        public void Construct(string titleText, string text)
        {
            ConstructTitle(titleText);
            plainText.text = text;
        }

        /// <summary>
        /// Updates the UI elements.
        /// </summary>
        public void UpdateTheme(FontInfo titleFont, FontInfo textFont)
        {
            UIExtensions.ChangeFont(title, titleFont);
            UIExtensions.ChangeFont(plainText, textFont);
        }
        
        public string Property { get => plainText.text; }
    }
}