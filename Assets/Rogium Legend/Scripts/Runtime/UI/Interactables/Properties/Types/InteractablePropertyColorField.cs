using System;
using RedRats.UI.Core;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    public class InteractablePropertyColorField : InteractablePropertyBase<Color>
    {
        [SerializeField] private ColorField colorField;
        
        private Action<Color> whenValueChange;

        public override void SetDisabled(bool isDisabled) => colorField.interactable = !isDisabled;
        
        /// <summary>
        /// Construct the property with initial values.
        /// </summary>
        /// <param name="titleText">The text of the property's title.</param>
        /// <param name="value">Initial color.</param>
        /// <param name="WhenChangeValue">Runs when a value is changed.</param>
        public void Construct(string titleText, Color value, Action<Color> WhenChangeValue)
        {
            ConstructTitle(titleText);
            colorField.Construct(value);
            
            if (whenValueChange != null) colorField.OnValueChanged -= whenValueChange;
            
            whenValueChange = WhenChangeValue;
            colorField.OnValueChanged += whenValueChange;
        }
        
        /// <summary>
        /// Updates the ColorField's theme.
        /// </summary>
        /// <param name="fieldSet">Sprite set of the ColorField.</param>
        /// <param name="titleFont">Font of the title.</param>
        public void UpdateTheme(InteractableSpriteInfo fieldSet, FontInfo titleFont)
        {
            UIExtensions.ChangeInteractableSprites(colorField, fieldSet);
            if (title != null) UIExtensions.ChangeFont(title, titleFont);
        }

        public override Color PropertyValue { get; }
    }
}