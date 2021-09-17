using UnityEngine;
using TMPro;
using System;

namespace Rogium.Global.UISystem.Interactables.Properties
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
        /// <param name="title">Property Title.</param>
        /// <param name="text">Text in the property.</param>
        public void Set(string title, string text)
        {
            this.title.text = title;
            this.plainText.text = text;
        }

        public string Title { get => title.text; }
        public string Property { get => plainText.text; }
    }
}