using UnityEngine;
using UnityEngine.UI;
using System;
using RedRats.UI.Core;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a toggle interactable property.
    /// </summary>
    public class InteractablePropertyToggle : InteractablePropertyBase<bool>
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private UIInfo ui;

        public override void SetDisabled(bool isDisabled) => toggle.interactable = !isDisabled;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// <param name="toggleState">State of the toggle checkbox.</param>
        /// <param name="WhenChangeValue">The method that will run when the toggle changes it's value.</param>
        public void Construct(string titleText, bool toggleState, Action<bool> WhenChangeValue)
        {
            ConstructTitle(titleText);
            
            toggle.isOn = toggleState;
            toggle.onValueChanged.AddListener(_ => WhenChangeValue(toggle.isOn));
        }

        /// <summary>
        /// Updates UI elements.
        /// </summary>
        /// <param name="toggleSpriteSet">The toggle graphics.</param>
        /// <param name="checkmark">The checkmark for activated toggle.</param>
        /// <param name="titleFont">The font of the title text.</param>
        public void UpdateTheme(InteractableSpriteInfo toggleSpriteSet, Sprite checkmark, FontInfo titleFont)
        {
            UIExtensions.ChangeInteractableSprites(toggle, ui.backgroundImage, toggleSpriteSet);
            UIExtensions.ChangeFont(title, titleFont);
            ui.checkmarkImage.sprite = checkmark;
        }
        
        public override bool PropertyValue { get => toggle.isOn; }

        [Serializable]
        public struct UIInfo
        {
            public Image backgroundImage;
            public Image checkmarkImage;
        }
    }
}