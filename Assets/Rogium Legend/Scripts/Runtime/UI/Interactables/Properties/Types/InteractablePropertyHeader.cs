using BoubakProductions.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Prepares the Header property for correct use.
    /// </summary>
    public class InteractablePropertyHeader : InteractablePropertyBase
    {
        [SerializeField] private Image line;
        
        public override void ChangeDisabledStatus(bool isDisabled) {}
        
        /// <summary>
        /// Sets the property title and state.
        /// </summary>
        /// <param name="headerText"></param>
        public void Construct(string headerText)
        {
            title.text = headerText;
        }

        /// <summary>
        /// Updates the header's UI elements.
        /// </summary>
        /// <param name="headerFont">The font of the header text.</param>
        /// <param name="lineSprite">The line drawn under the header.</param>
        public void UpdateTheme(FontInfo headerFont, Sprite lineSprite)
        {
            UIExtensions.ChangeFont(title, headerFont);
            line.sprite = lineSprite;
        }
    }
}