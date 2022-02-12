using BoubakProductions.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Prepares the Header property for correct use.
    /// </summary>
    public class InteractablePropertyHeader : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private Image line;
        
        /// <summary>
        /// Sets the property title and state.
        /// </summary>
        /// <param name="headerText"></param>
        public void Construct(string headerText)
        {
            header.text = headerText;
        }

        /// <summary>
        /// Updates the header's UI elements.
        /// </summary>
        /// <param name="headerFont">The font of the header text.</param>
        /// <param name="lineSprite">The line drawn under the header.</param>
        public void UpdateTheme(FontInfo headerFont, Sprite lineSprite)
        {
            UIExtensions.ChangeFont(header, headerFont);
            line.sprite = lineSprite;
        }
        
        public string Title { get => header.text; }
    }
}