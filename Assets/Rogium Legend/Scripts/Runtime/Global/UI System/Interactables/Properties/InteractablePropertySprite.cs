using System;
using BoubakProductions.UI.Helpers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rogium.Global.UISystem.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a sprite interactable property.
    /// </summary>
    public class InteractablePropertySprite : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Image icon;
        [SerializeField] private Button spriteField;
        [SerializeField] private UIInfo ui;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="title">Property Title.</param>
        /// <param name="sprite">Sprite of property.</param>
        public void Construct(string title, Sprite sprite)
        {
            this.title.text = title;
            this.icon.sprite = sprite;
            
            //TODO: Add delegate registration for when the sprite changes.
        }

        /// <summary>
        /// Updates the UI elements
        /// </summary>
        /// <param name="fieldSpriteSet">A Set of Sprites for the button.</param>
        public void UpdateTheme(InteractableInfo fieldSpriteSet)
        {
            UIExtensions.ChangeInteractableSprites(spriteField, ui.borderImage, fieldSpriteSet);
        }

        public string Title { get => title.text; }
        public Sprite Property { get => icon.sprite; }

        [Serializable]
        public struct UIInfo
        {
            public Image borderImage;
        }
    }
}