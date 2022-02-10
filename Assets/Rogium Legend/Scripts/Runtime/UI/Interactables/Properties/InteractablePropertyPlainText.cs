using UnityEngine;
using TMPro;
using System;
using BoubakProductions.UI.Core;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a plain text interactable property.
    /// </summary>
    public class InteractablePropertyPlainText : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI plainText;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// <param name="text">Text in the property.</param>
        public void Construct(string titleText, string text)
        {
            title.text = titleText;
            title.gameObject.SetActive((titleText != ""));
            
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
        
        public string Title { get => title.text; }
        public string Property { get => plainText.text; }
    }
}