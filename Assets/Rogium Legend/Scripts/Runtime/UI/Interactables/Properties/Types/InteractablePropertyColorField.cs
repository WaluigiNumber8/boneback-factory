using System;
using UnityEngine;

namespace Rogium.UserInterface.Interactables.Properties
{
    public class InteractablePropertyColorField : InteractablePropertyBase<Color>
    {
        [SerializeField] private ColorField colorField;
        
        private Action<Color> whenValueChange;

        public override void SetDisabled(bool isDisabled) => colorField.interactable = !isDisabled;
        
        public void Construct(string titleText, Color value, Action<Color> WhenChangeValue)
        {
            ConstructTitle(titleText);
            colorField.Construct(value);
            
            if (whenValueChange != null) colorField.OnValueChanged -= whenValueChange;
            
            whenValueChange = WhenChangeValue;
            colorField.OnValueChanged += whenValueChange;
        }

        public override Color PropertyValue { get; }
    }
}