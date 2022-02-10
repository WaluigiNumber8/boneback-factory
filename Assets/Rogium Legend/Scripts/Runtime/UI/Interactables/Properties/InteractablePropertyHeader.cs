using BoubakProductions.UI.Core;
using TMPro;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Prepares the Header property for correct use.
    /// </summary>
    public class InteractablePropertyHeader : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI header;

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
        public void UpdateTheme(FontInfo headerFont)
        {
            UIExtensions.ChangeFont(header, headerFont);
        }
        
        public string Title { get => header.text; }
    }
}