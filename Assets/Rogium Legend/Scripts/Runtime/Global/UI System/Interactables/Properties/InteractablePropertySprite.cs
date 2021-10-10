using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Rogium.Global.UISystem.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a sprite interactable property.
    /// </summary>
    public class InteractablePropertySprite : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image image;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="title">Property Title.</param>
        /// <param name="sprite">Sprite of property.</param>
        public void Set(string title, Sprite sprite)
        {
            this.title.text = title;
            this.image.sprite = sprite;
            
            //TODO: Add delegate registration for when the sprite changes.
        }

        public string Title { get => title.text; }
        public Sprite Property { get => image.sprite; }
    }
}