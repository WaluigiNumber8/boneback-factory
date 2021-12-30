using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using BoubakProductions.UI.Core;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a toggle interactable property.
    /// </summary>
    public class InteractablePropertyToggle : MonoBehaviour, IInteractableProperty
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private Toggle toggle;
        [SerializeField] private UIInfo ui;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="title">Property Title.</param>
        /// <param name="toggleState">State of the toggle checkbox.</param>
        /// <param name="OnChangeValue">The method that will run when the toggle changes it's value.</param>
        public void Construct(string title, bool toggleState, Action<bool> OnChangeValue)
        {
            this.title.text = title;
            this.toggle.isOn = toggleState;
            this.toggle.onValueChanged.AddListener(delegate { OnChangeValue(toggle.isOn); });
        }

        /// <summary>
        /// Updates UI elements.
        /// </summary>
        /// <param name="background">The background of the toggle.</param>
        /// <param name="checkmark">The checkmark for activated toggle.</param>
        public void UpdateTheme(Sprite background, InteractableInfo toggleSpriteSet, Sprite checkmark)
        {
            UIExtensions.ChangeInteractableSprites(toggle, ui.backgroundImage, toggleSpriteSet);
            ui.backgroundImage.sprite = background;
            ui.checkmarkImage.sprite = checkmark;
        }
        
        public string Title { get => title.text; }
        public bool Property { get => toggle.isOn; }

        [Serializable]
        public struct UIInfo
        {
            public Image backgroundImage;
            public Image checkmarkImage;
        }
    }
}