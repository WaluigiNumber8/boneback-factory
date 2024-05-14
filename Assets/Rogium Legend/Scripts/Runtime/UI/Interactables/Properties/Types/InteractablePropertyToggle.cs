using UnityEngine;
using UnityEngine.UI;
using System;
using RedRats.UI.Core;
using Rogium.Systems.ActionHistory;

namespace Rogium.UserInterface.Interactables.Properties
{
    /// <summary>
    /// Overseers everything happening in a toggle interactable property.
    /// </summary>
    public class InteractablePropertyToggle : InteractablePropertyBase<bool>
    {
        [SerializeField] private Toggle toggle;
        [SerializeField] private UIInfo ui;
        
        private Action<bool> whenChangeValue;

        public override void SetDisabled(bool isDisabled) => toggle.interactable = !isDisabled;

        /// <summary>
        /// Set the property title and state.
        /// </summary>
        /// <param name="titleText">Property Title.</param>
        /// <param name="toggleState">State of the toggle checkbox.</param>
        /// <param name="whenChangeValue">The method that will run when the toggle changes it's value.</param>
        public void Construct(string titleText, bool toggleState, Action<bool> whenChangeValue)
        {
            ConstructTitle(titleText);
            
            toggle.isOn = toggleState;
            this.whenChangeValue = whenChangeValue;
            toggle.onValueChanged.AddListener(WhenValueChange);
        }
        
        public void UpdateValueWithoutNotify(bool value)
        {
            toggle.SetIsOnWithoutNotify(value);
            whenChangeValue?.Invoke(value);
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
        
        private void WhenValueChange(bool value)
        {
            ActionHistorySystem.GetInstance().AddAndExecute(new UpdateToggleAction(this, value));
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